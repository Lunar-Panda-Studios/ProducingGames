using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database: MonoBehaviour
{
    public static Database current;
    public List<ItemData> allItems;
    public List<DocumentData> allDocs;
    public List<StoryData> allStoryNotes;
    //public List<int> id;
    public static Dictionary<ItemData, string> getItemID;
    public static Dictionary<DocumentData, string> getDocID;
    public static Dictionary<StoryData, string> getStoryID;

    private void Awake()
    {
        current = this;
    }

    private void Start()
    {
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
    }

}
