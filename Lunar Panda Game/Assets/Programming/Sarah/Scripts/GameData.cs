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
    public List<int> puzzlesEvents;
    public List<bool> puzzleCompleted;

    public GameData(TestingSave data)
    {
        position = new float[3];
        position[0] = data.player.transform.position.x;
        position[1] = data.player.transform.position.y;
        position[2] = data.player.transform.position.z;

        rotation = new float[3];
        rotation[0] = data.player.transform.rotation.x;
        rotation[1] = data.player.transform.rotation.y;
        rotation[2] = data.player.transform.rotation.z;

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

        puzzlesEvents = data.completion.eventsID;
        puzzleCompleted = data.completion.isCompleted;
    }

}
