using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class Analysis : MonoBehaviour
{
    internal static Analysis current;
    internal bool consent = false;
    private const string Name = "Game Analystics";
    internal Dictionary<string, object> parameters = new Dictionary<string, object>();
    internal float menuTime = 0;
    internal float sprintingTime = 0;
    internal bool menuOpen;
    float timer = 0;
    float levelTimer = 0;
    float flashlightTime = 0;
    float gameTimer = 0;
    playerMovement player;
    Flashlight light;
    internal int failCounterSimon = 0;
    internal int failCounterPiano = 0;
    internal int failCounterCodeLock = 0;
    internal int failCounterBikeLock = 0;
    internal int failCounterAntidote = 0;

    private void Start()
    {
        if (current == null)
        {
            current = this;
        }
        else
        {
            Destroy(this);
        }

        player = FindObjectOfType<playerMovement>();
        light = FindObjectOfType<Flashlight>();

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        timer += Time.unscaledDeltaTime;
        levelTimer += Time.unscaledDeltaTime;
        gameTimer += Time.unscaledDeltaTime;

        if (menuOpen)
        {
            menuTime += Time.unscaledDeltaTime;
        }
        if(player.isSprinting)
        {
            sprintingTime += Time.deltaTime;
        }
        if(light.powerOn)
        {
            flashlightTime += Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.K))
        {
            completed();
        }
    }

    public bool completed()
    {
        AnalyticsResult result = AnalyticsEvent.Custom(Name, parameters);
        print(result.ToString());
        if (result == AnalyticsResult.Ok)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void resetTimer(string puzzleName)
    {
        parameters.Add(puzzleName, timer);
        timer = 0;
    }

    public void resetlevelTimer(string level)
    {
        level = "Time to Complete " + level;
        parameters.Add(level, levelTimer);
        levelTimer = 0;
    }

    public void missedItems(string level)
    {
        List<ItemData> items = Database.current.allItems;
        List <ItemData> notPickedUp = new List<ItemData>();

        for(int i = 0; i < items.Count; i++)
        {
            if(!items[i].beenPickedUp)
            {
                notPickedUp.Add(items[i]);
            }
        }

        level = "Items Missed in " + level;
        parameters.Add(level, notPickedUp);
    }

    public void missedClues(string level)
    {
        List<DocumentData> allDocs = Database.current.allDocs;
        List<DocumentData> notPickedUp = new List<DocumentData>();

        for (int i = 0; i < allDocs.Count; i++)
        {
            if (!allDocs[i].beenPickedUp)
            {
                notPickedUp.Add(allDocs[i]);
            }
        }

        level = "Documents Missed in " + level;
        parameters.Add(level, notPickedUp);
    }

    public void missedStoryNotes(string level)
    {
        List<StoryData> storyNotes = Database.current.allStoryNotes;
        List<StoryData> notPickedUp = new List<StoryData>();


        for (int i = 0; i < storyNotes.Count; i++)
        {
            if (!storyNotes[i].beenPickedUp)
            {
                notPickedUp.Add(storyNotes[i]);
            }
        }

        level = "Story Notes Missed in " + level;
        parameters.Add(level, notPickedUp);
    }

    public void timesChecked(string level)
    {
        List<string> itemTimes = new List<string>();
        List<string> usedTimes = new List<string>();
        string data;

        for (int i = 0; i < Database.current.itemsInScene.Count; i++)
        {
            data = Database.current.itemsInScene[i].data.itemName + " " + Database.current.itemsInScene[i].data.timesChecked.ToString();
            itemTimes.Add(data);
            data = Database.current.itemsInScene[i].data.itemName + " " + Database.current.itemsInScene[i].data.timesUses.ToString();
            usedTimes.Add(data);
        }

        parameters.Add("Times Items Were Checked in " + level, itemTimes);
        parameters.Add("Times Items Were Used in " + level, usedTimes);
    }

    public void addTimers()
    {
        parameters.Add("Time using flashlight", flashlightTime);
        parameters.Add("Time in Menus", menuTime);
        parameters.Add("Time Running", sprintingTime);
        parameters.Add("Time of Gameplay", gameTimer);
    }

    public void puzzleFails()
    {
        List<string> failCounter = new List<string>();

        if (failCounterSimon != 0)
        {
            failCounter.Add("Simon Says: " + failCounterSimon);
            failCounterSimon = 0;
        }

        if (failCounterPiano != 0)
        {
            failCounter.Add("Piano: " + failCounterPiano);
            failCounterPiano = 0;
        }

        if (failCounterCodeLock != 0)
        {
            failCounter.Add("Code Lock: " + failCounterCodeLock);
            failCounterCodeLock = 0;
        }

        if (failCounterBikeLock != 0)
        {
            failCounter.Add("Bike Lock: " + failCounterBikeLock);
            failCounterBikeLock = 0;
        }

        if (failCounterAntidote != 0)
        {
            failCounter.Add( "Antidote: " + failCounterAntidote);
            failCounterAntidote = 0;
        }

        parameters.Add(GameManager.Instance.currentRoom.ToString() + " Fail Counters", failCounter);

    }

    public void OnLevelWasLoaded(int level)
    {
        if (level != GameManager.Instance.whichLevel)
        {
            timesChecked(GameManager.Instance.currentRoom.ToString());
            missedClues(GameManager.Instance.currentRoom.ToString());
            missedItems(GameManager.Instance.currentRoom.ToString());
            missedStoryNotes(GameManager.Instance.currentRoom.ToString());
            resetlevelTimer(GameManager.Instance.currentRoom.ToString());
            puzzleFails();
            print("Level Loaded");
            GameManager.Instance.currentLevel(level);
            GameManager.Instance.ChangeRoom();
        }
    }
}
