using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;


//SINGLETON
//Manager of Managers
//Scene Loaders
//Tracking Player:
//Current Level
//Others variables that could be useful

public enum GameState //Only Basic states
{
    MENU, GAME, PAUSE, QUIT
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState gameStates;
    public int whichLevel = 0;
    public Room currentRoom;
    internal GameObject player;
    internal Vector3 location;
    internal Quaternion rotation;
    public Inventory inventory;
    public PuzzleData completion;
    internal bool canSave;
    internal int saveFile = 1;
    internal string currentScene;
    internal bool subtitles;
    internal List<DocumentData> docInventory;

    private void Awake()
    {
        docInventory = new List<DocumentData>();
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        completion = FindObjectOfType<PuzzleData>();
        player = FindObjectOfType<playerMovement>().gameObject;
    }

    public void save(bool mainSave)
    {
        currentScene = SceneManager.GetActiveScene().name;
        Database.current.locationUpdate();
        player = FindObjectOfType<playerMovement>().gameObject;
        inventory = FindObjectOfType<Inventory>();
        completion = FindObjectOfType<PuzzleData>();
        location = player.transform.position;
        rotation = player.transform.rotation;
        if(mainSave)
        {
            SaveSystem.asignPath(saveFile);
        }
        else
        {
            SaveSystem.asignPath(0);
        }
        SaveSystem.asignPath(saveFile);
        saveAsync();
    }

    public async void saveAsync()
    {
        var result = await Task.Run(() =>
        {
            SaveSystem.save(saveFile);

            return true;
        }
        );

        UIManager.Instance.autoSavingPromptHide();
    }

    IEnumerator loadScene(GameData data)
    {
        bool sceneJustLoad = false;

        if(whichLevel != data.whichLevel)
        {
            whichLevel = data.whichLevel;
            AsyncOperation loadScene = SceneManager.LoadSceneAsync(data.sceneName);

            while (!loadScene.isDone)
            {
                yield return null;
            }

            yield return new WaitForEndOfFrame();

            sceneJustLoad = true;

            player = FindObjectOfType<playerMovement>().gameObject;
            inventory = FindObjectOfType<Inventory>();
            completion = FindObjectOfType<PuzzleData>();
        }

        player.transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
        player.transform.eulerAngles = new Vector3(data.rotation[0], data.rotation[1], data.rotation[2]);
        player.transform.rotation = new Quaternion(data.rotation[0], data.rotation[1], data.rotation[2],0);

        //inventory.itemInventory = data.itemInven;
        int index = 0;

        //for (int i = 0; i < inventory.itemInventory.Count; i++)
        //{
        //    inventory.itemInventory[i] = null;
        //}

        //foreach (string itemId in data.itemInven)
        //{
        //    if (itemId != null)
        //    {
        //        inventory.itemInventory[index] = (Database.current.allItems[int.Parse(itemId)]);
        //    }

        //    index += 1;
        //}

        //index = 0;

        //foreach (HoldableItem item in Database.current.itemsInScene)
        //{
        //    if (!inventory.itemInventory.Contains(item.data))
        //    {
        //        if (data.itemsInScene[index, 0] != null)
        //        {
        //            item.gameObject.SetActive(true);
        //            item.transform.position = new Vector3((float)data.itemsInScene[index, 0], (float)data.itemsInScene[index, 1], (float)data.itemsInScene[index, 2]);
        //            item.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        //        }
        //    }
        //    else
        //    {
        //        item.gameObject.SetActive(false);
        //    }

        //    index++;
        //}

        //Document aka clues or red hering or stuff with images

        index = 0;

        inventory.documentInventory.Clear();

        foreach (string docId in data.docInven)
        {
            if (docId != null)
            {
                inventory.documentInventory.Add(Database.current.allDocs[int.Parse(docId)]);

                GameObject document = GameObject.Find(inventory.documentInventory[index].prefab.name);

                if (document != null)
                {
                    document.SetActive(false);
                }
            }

            index += 1;
        }

        index = 0;
        inventory.storyNotesInventory.Clear();

        //Story documents inventory 
        foreach (string storyID in data.docInven)
        {
            if (storyID != null)
            {
                inventory.storyNotesInventory.Add(Database.current.allStoryNotes[int.Parse(storyID)]);
                GameEvents.current.onTriggerStoryNotes(Database.current.allStoryNotes[int.Parse(storyID)].id);
            }

            index += 1;
        }

        //Puzzle status
        //completion.eventsID = data.puzzlesEvents;
        //completion.isCompleted = data.puzzleCompleted;

        //for (int i = 0; i < data.puzzleCompleted.Count; i++)
        //{
        //    if (completion.isCompleted[i])
        //    {
        //        GameEvents.current.onPuzzleComplete(i + 1);
        //    }
        //    else
        //    {
        //        if (!sceneJustLoad)
        //        {
        //            GameEvents.current.onPuzzleReset(i + 1);
        //        }
        //    }
        //}

        print("Load");
    }

    public void load(int slot = 4)
    {
        SaveSystem.asignPath(saveFile);
        GameData data = SaveSystem.load();

        if(data != null)
        {
            StartCoroutine(loadScene(data));
        }
        else
        {
            print("No data");
        }
    }

    public void deleteButton(int slot)
    {
        SaveSystem.delete(slot);
    }



    public void currentLevel(int currLevel)
    {
        //Just returning the value of which game scene the player is in
        whichLevel = currLevel;
        switch (whichLevel)
        {
            case 0:
                {
                    currentRoom = Room.NONE;
                    break;
                }
            case 1:
                {
                    currentRoom = Room.CRASHEDTRAIN;
                    break;
                }
            case 2:
                {
                    currentRoom = Room.TRAIN;
                    break;
                }
            case 3:
                {
                    currentRoom = Room.HOSPITAL;
                    break;
                }
            case 4:
                {
                    currentRoom = Room.HOTEL;
                    break;
                }
            case 5:
                {
                    currentRoom = Room.CABIN;
                    break;
                }
            default:
                {
                    currentRoom = Room.NONE;
                    whichLevel = 0;
                    break;
                }
        }
        return;
    }
}
