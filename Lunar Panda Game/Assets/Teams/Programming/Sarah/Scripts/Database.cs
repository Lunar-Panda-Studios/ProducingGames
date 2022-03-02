using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database: MonoBehaviour
{
    public static Database current;
    public List<ItemData> allItems;
    public List<DocumentData> allDocs;
    public List<StoryData> allStoryNotes;
    public List<HoldableItem> itemsInScene;
    public static List<Vector3> itemLocation;
    public static Dictionary<HoldableItem, Vector3> getLocation;
    public static Dictionary<ItemData, string> getItemID;
    public static Dictionary<DocumentData, string> getDocID;
    public static Dictionary<StoryData, string> getStoryID;

    private void Awake()
    {
        //setting up singleton
        if (current != null && current != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            current = this;
        }
    }

    private void Start()
    {
        getLocation = new Dictionary<HoldableItem, Vector3>();
        getDocID = new Dictionary<DocumentData, string>();
        getItemID = new Dictionary<ItemData, string>();
        getStoryID = new Dictionary<StoryData, string>();

        for (int i = 0; i < allItems.Count; i++)
        {
            getItemID.Add(allItems[i], i.ToString());
        }

        for (int i = 0; i < allDocs.Count; i++)
        {
            getDocID.Add(allDocs[i], i.ToString());
        }

        for (int i = 0; i < allStoryNotes.Count; i++)
        {
            getStoryID.Add(allStoryNotes[i], i.ToString());
        }

        itemUpdate();
        locationDicUpdate();
    }

    public void itemUpdate()
    {
        HoldableItem[] temp = FindObjectsOfType<HoldableItem>();

        itemLocation = new List<Vector3>();
        itemsInScene = new List<HoldableItem>();

        for (int i = 0; i < temp.Length; i++)
        {
            temp[i].data.id = i;
            itemsInScene.Add(temp[i]);
            itemLocation.Add(itemsInScene[i].transform.position);
        }
    }

    public void locationDicUpdate()
    {
        for(int i = 0; i < itemsInScene.Count; i++)
        {
            getLocation.Add(itemsInScene[i].GetComponent<HoldableItem>(), itemLocation[i]);
        }
    }

    public void locationUpdate()
    {
        foreach(HoldableItem item in FindObjectsOfType<HoldableItem>())
        {
            for(int i = 0; i < itemsInScene.Count; i++)
            {
                if (itemsInScene[i].GetComponent<HoldableItem>().data == item.data)
                {
                    itemLocation[i] = item.gameObject.transform.position;
                }
            }
        }
    }

    public void addToItemsInScene(HoldableItem newItem)
    {
        newItem.data.id = itemsInScene.Count;
        itemsInScene.Add(newItem);
    }

}
