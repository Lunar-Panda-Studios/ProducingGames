using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingSave : MonoBehaviour
{
    internal GameObject player;
    public Inventory inventory;
    public PuzzleData completion;
    internal bool canSave;

    private void Awake()
    {
        player = FindObjectOfType<playerMovement>().gameObject;
    }

    public void save()
    {
        SaveSystem.save(this);
        print("Save");
    }

    public void load()
    {
        GameData data = SaveSystem.load();
        if (data != null)
        {
            player.transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
            player.transform.eulerAngles = new Vector3(data.rotation[0], data.rotation[1], data.rotation[2]);

            //inventory.itemInventory = data.itemInven;
            int index = 0;

            foreach(string itemId in data.itemInven )
            {
                if(itemId != null)
                {
                    inventory.itemInventory[index] = (Database.current.allItems[int.Parse(itemId)]);
                }

                index += 1;
            }

            //Document aka clues or red hering or stuff with images

            index = 0;

            foreach (string docId in data.docInven)
            {
                if (docId != null)
                {
                    inventory.documentInventory.Add(Database.current.allDocs[int.Parse(docId)]);
                }

                index += 1;
            }

            index = 0;

            //Story documents inventory 
            foreach (string storyID in data.docInven)
            {
                if (storyID != null)
                {
                    inventory.storyNotesInventory.Add(Database.current.allStoryNotes[int.Parse(storyID)]);
                }

                index += 1;
            }

            //Puzzle status
            completion.eventsID = data.puzzlesEvents;
            completion.isCompleted = data.puzzleCompleted;

            for (int i = 0; i < data.puzzleCompleted.Count; i++)
            {
                print(completion.isCompleted[i]);
                if (completion.isCompleted[i])
                {
                    print("Completed");
                    GameEvents.current.onPuzzleComplete(i + 1);
                }
            }

            print("Load");
        }
        else
        {
            print("No load data");
        }

    }
}
