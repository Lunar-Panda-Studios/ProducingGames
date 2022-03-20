using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


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
    //Need help with creating a Singleton
    public static GameManager Instance;

    public GameState gameStates;
    public int whichLevel = 0;
    public Room currentRoom;
    internal GameObject player;
    public Inventory inventory;
    public PuzzleData completion;
    internal bool canSave;
    internal int saveFile = 1;
    internal string currentScene;
    internal bool subtitles;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        completion = FindObjectOfType<PuzzleData>();
        player = FindObjectOfType<playerMovement>().gameObject;
    }

    public void save()
    {
        currentScene = SceneManager.GetActiveScene().name;
        print(whichLevel);
        Database.current.locationUpdate();
        SaveSystem.save(saveFile);
        print("Save");
    }

    IEnumerator load(int slot)
    {
        GameData data = SaveSystem.load(slot);

        if (data != null)
        {
            bool sceneJustLoad = false ;

            if (whichLevel != data.whichLevel)
            {
                whichLevel = data.whichLevel;
                AsyncOperation loadScene = SceneManager.LoadSceneAsync(data.sceneName);

                while(!loadScene.isDone)
                {
                    yield return null;
                }

                sceneJustLoad = true;
            }

            player.transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
            player.transform.eulerAngles = new Vector3(data.rotation[0], data.rotation[1], data.rotation[2]);

            print(data.position[0]);

            //inventory.itemInventory = data.itemInven;
            int index = 0;

            for (int i = 0; i < inventory.itemInventory.Count; i++)
            {
                inventory.itemInventory[i] = null;
            }

            foreach (string itemId in data.itemInven)
            {
                if (itemId != null)
                {
                    inventory.itemInventory[index] = (Database.current.allItems[int.Parse(itemId)]);
                }

                index += 1;
            }

            index = 0;

            foreach (HoldableItem item in Database.current.itemsInScene)
            {
                if (!inventory.itemInventory.Contains(item.data))
                {
                    if (data.itemsInScene[index, 0] != null)
                    {
                        item.gameObject.SetActive(true);
                        item.transform.position = new Vector3((float)data.itemsInScene[index, 0], (float)data.itemsInScene[index, 1], (float)data.itemsInScene[index, 2]);
                        item.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                    }
                }
                else
                {
                    item.gameObject.SetActive(false);
                }

                index++;
            }

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
            completion.eventsID = data.puzzlesEvents;
            completion.isCompleted = data.puzzleCompleted;

            for (int i = 0; i < data.puzzleCompleted.Count; i++)
            {
                if (completion.isCompleted[i])
                {
                    GameEvents.current.onPuzzleComplete(i + 1);
                }
                else
                {
                    if (!sceneJustLoad)
                    {
                        GameEvents.current.onPuzzleReset(i + 1);
                    }
                }
            }

            print("Load");
        }
        else
        {
            print("No load data");
        }

        yield return null;
    }

    public void loadButton(int slot = 0)
    {
        StartCoroutine(load(slot));
    }

    public void deleteButton(int slot)
    {
        SaveSystem.delete(slot);
    }

    public void LoadCurrentScene(GameState state) //Scene Loader. Not doing anything as of right now as we don't have any scenes to load.
    {
        gameStates = state;
        switch (gameStates)
        {
            case GameState.MENU:
                break;
            case GameState.GAME:
                {
                    switch (whichLevel)
                    {
                        case 0:
                            {
                                SceneManager.LoadScene("Train v1.8");
                                break;
                            }
                        case 1:
                            {
                                SceneManager.LoadScene("Hospital");
                                break;
                            }                        
                        default:
                            {
                                SceneManager.LoadScene("Train");
                                break;
                            }
                    }
                    break;
                }
            case GameState.PAUSE:
                break;
            case GameState.QUIT:
                Application.Quit();
                break;
            default:
                break;
        }

        currentScene = SceneManager.GetActiveScene().name;
    }



    public void currentLevel(int currLevel)
    {
        //Just returning the value of which game scene the player is in
        whichLevel = currLevel;
        return;
    }

    public void ChangeRoom()
    {
        switch (whichLevel)
        {
            case 0:
                {
                    currentRoom = Room.TRAIN;
                    break;
                }
            case 1:
                {
                    currentRoom = Room.HOSPITAL;
                    break;
                }
            case 2:
                {
                    currentRoom = Room.CABIN;
                    break;
                }
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level < 3)
        {
            player = FindObjectOfType<playerMovement>().gameObject;
            inventory = FindObjectOfType<Inventory>();
            completion = FindObjectOfType<PuzzleData>();
        }
        else
        {
            Destroy(this);
        }
    }
}
