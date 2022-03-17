using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int level;
    public float[] position;
    public float[] rotation;
    public List<string> itemInven;
    public List<string> docInven;
    public List<string> storyInven;
    public List<int> puzzlesEvents;
    public List<bool> puzzleCompleted;
    public float?[,] itemsInScene;
    public string sceneName;
    public int whichLevel;

    public GameData(GameManager data)
    {
        whichLevel = data.whichLevel;

        position = new float[3];
        position[0] = data.player.transform.position.x;
        position[1] = data.player.transform.position.y;
        position[2] = data.player.transform.position.z;

        rotation = new float[3];
        rotation[0] = data.player.transform.rotation.x;
        rotation[1] = data.player.transform.rotation.y;
        rotation[2] = data.player.transform.rotation.z;

        itemsInScene = new float?[Database.getLocation.Count, 3];

        for(int i = 0; i < Database.getLocation.Count; i++)
        {
            if (Database.current.itemsInScene[i] == null)
            {
                if (!data.inventory.itemInventory.Contains(Database.current.itemsInScene[i].GetComponent<HoldableItem>().data))
                {
                    itemsInScene[i, 0] = Database.itemLocation[i].x;
                    itemsInScene[i, 1] = Database.itemLocation[i].y;
                    itemsInScene[i, 2] = Database.itemLocation[i].z;
                }
                else
                {
                    itemsInScene[i, 0] = null;
                    itemsInScene[i, 1] = null;
                    itemsInScene[i, 2] = null;
                }
            }
            else
            {
                break;
            }
        }

        //itemInven = data.inventory.itemInventory;

        itemInven = new List<string>();

        for(int i = 0; i < data.inventory.itemInventory.Count; i++)
        {
            if (data.inventory.itemInventory[i] != null)
            {
                itemInven.Add(Database.getItemID[data.inventory.itemInventory[i]]);
            }
            else
            {
                itemInven.Add(null);
            }
        }

        //docInven = data.inventory.documentInventory;

        docInven = new List<string>();

        for (int i = 0; i < data.inventory.documentInventory.Count; i++)
        {
            if (data.inventory.documentInventory[i] != null)
            {
                docInven.Add(Database.getDocID[data.inventory.documentInventory[i]]);
            }
            else
            {
                docInven.Add(null);
            }
        }

        storyInven = new List<string>();

        for (int i = 0; i < data.inventory.storyNotesInventory.Count; i++)
        {
            if (data.inventory.storyNotesInventory[i] != null)
            {
                storyInven.Add(Database.getStoryID[data.inventory.storyNotesInventory[i]]);
            }
            else
            {
                storyInven.Add(null);
            }
        }

        puzzlesEvents = data.completion.eventsID;
        puzzleCompleted = data.completion.isCompleted;
    }

}
