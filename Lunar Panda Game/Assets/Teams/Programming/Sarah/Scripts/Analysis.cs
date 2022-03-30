using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class Analysis : MonoBehaviour
{
    internal static Analysis current;
    internal bool consent = true;

    internal Dictionary<string, object> parameters = new Dictionary<string, object>();
    internal Dictionary<string, object> timers = new Dictionary<string, object>();
    internal Dictionary<string, object> timersLevels = new Dictionary<string, object>();
    internal Dictionary<string, object> timersPuzzlesp1 = new Dictionary<string, object>();
    internal Dictionary<string, object> timersPuzzlesp2 = new Dictionary<string, object>();
    internal Dictionary<string, object> failCounter = new Dictionary<string, object>();

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
    internal int failCounterWires = 0;
    internal int failCounterPiano = 0;
    internal int failCounterChess = 0;
    internal int failCounterCodeLock = 0;
    internal int failCounterBikeLock = 0;
    internal int failCounterAntidote = 0;
    internal int failCounterPressurePlates = 0;

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
        if (player == null)
        {
            refind();
        }

        timer += Time.unscaledDeltaTime;
        levelTimer += Time.unscaledDeltaTime;
        gameTimer += Time.unscaledDeltaTime;

        if (menuOpen)
        {
            menuTime += Time.unscaledDeltaTime;
        }
        if (player.isSprinting)
        {
            sprintingTime += Time.deltaTime;
        }
        if (light.powerOn)
        {
            flashlightTime += Time.deltaTime;
        }

        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    completed("Test");
        //}
    }

    public void askConcent(bool hasConsent)
    {
        consent = hasConsent;
    }

    public bool completed(string name, Dictionary<string, object> para)
    {
        AnalyticsResult result = AnalyticsEvent.Custom(name, para);
        print(result.ToString());
        if (result == AnalyticsResult.Ok)
        {
            print("TRUE");
            return true;
        }
        else
        {
            print("FALSE");
            return false;
        }
    }

    public void resetTimer(string puzzleName)
    {
        if(timersPuzzlesp1.Count < 9)
        {
            timersPuzzlesp1.Add(puzzleName, timer);
        }
        else
        {
            timersPuzzlesp2.Add(puzzleName, timer);
        }

        timer = 0;
    }

    public void resetlevelTimer(string level)
    {
        level = "Time to Complete " + level;
        timersLevels.Add(level, levelTimer);
        levelTimer = 0;
    }

    public void missedItems(string level)
    {
        List<ItemData> items = Database.current.allItems;
        List<ItemData> notPickedUp = new List<ItemData>();
        parameters = new Dictionary<string, object>();
        Dictionary<string, object> parameters2 = new Dictionary<string, object>();

        for (int i = 0; i < items.Count; i++)
        {
            if (!items[i].beenPickedUp)
            {
                if(parameters.Count < 9)
                {
                    parameters.Add(level + " item missed no" + i, items[i].itemName);
                }
                else
                {
                    parameters2.Add(level + " item missed no" + i, items[i].itemName);
                }

            }
        }

        completed("Missed Items in " + level, parameters);

    }

    public void missedClues(string level)
    {
        List<DocumentData> allDocs = Database.current.allDocs;
        List<DocumentData> notPickedUp = new List<DocumentData>();
        parameters = new Dictionary<string, object>();

        for (int i = 0; i < allDocs.Count; i++)
        {
            if (!allDocs[i].beenPickedUp)
            {
                parameters.Add(level + " clue missed no" + i, allDocs[i]);
            }
        }

        completed("Clues Missed in " + level, parameters);
    }

    public void missedStoryNotes(string level)
    {
        List<StoryData> storyNotes = Database.current.allStoryNotes;
        List<StoryData> notPickedUp = new List<StoryData>();
        parameters = new Dictionary<string, object>();
        int number = 0;

        for (int i = 0; i < storyNotes.Count; i++)
        {
            if (!storyNotes[i].beenPickedUp)
            {
                parameters.Add(level + " story Notes no" + number, storyNotes[i]);
                number++;
            }
        }

        completed("Story Notes Missed in " + level, parameters);
    }

    public void timesChecked(string level)
    {
        List<string> itemTimes = new List<string>();
        List<string> usedTimes = new List<string>();

        parameters = new Dictionary<string, object>();
        Dictionary<string, object> parameters2 = new Dictionary<string, object>();
        Dictionary<string, object> parameters3 = new Dictionary<string, object>();
        Dictionary<string, object> parameters4 = new Dictionary<string, object>();

        for (int i = 0; i < Database.current.itemsInScene.Count; i++)
        {
            if (parameters.Count < 9)
            {
                parameters.Add(Database.current.itemsInScene[i].data.name, Database.current.itemsInScene[i].data.timesChecked.ToString());
                parameters2.Add(Database.current.itemsInScene[i].data.name, Database.current.itemsInScene[i].data.timesUses.ToString());
            }
            else
            {
                parameters3.Add(Database.current.itemsInScene[i].data.name, Database.current.itemsInScene[i].data.timesChecked.ToString());
                parameters4.Add(Database.current.itemsInScene[i].data.name, Database.current.itemsInScene[i].data.timesUses.ToString());
            }
        }

        completed("Times Items Were Checked in " + level, parameters);
        completed("Times Items Were Used in " + level, parameters2);
    }

    public void addTimers()
    {
        timers.Add("Time using flashlight", flashlightTime);
        timers.Add("Time in Menus", menuTime);
        timers.Add("Time Running", sprintingTime);
        timers.Add("Time of Gameplay", gameTimer);

        completed("Stat Timers", timers);
    }

    public void puzzleFails(string level)
    {
        if (failCounterSimon != 0)
        {
            failCounter.Add("Simon Says " + level, failCounterSimon);
            failCounterSimon = 0;
        }

        if (failCounterPiano != 0)
        {
            failCounter.Add("Piano " + level,  failCounterPiano);
            failCounterPiano = 0;
        }

        if (failCounterCodeLock != 0)
        {
            failCounter.Add("Code Lock " + level, failCounterCodeLock);
            failCounterCodeLock = 0;
        }

        if (failCounterBikeLock != 0)
        {
            failCounter.Add("Bike Lock " + level, failCounterBikeLock);
            failCounterBikeLock = 0;
        }

        if (failCounterAntidote != 0)
        {
            failCounter.Add("Antidote " + level, failCounterAntidote);
            failCounterAntidote = 0;
        }

        if (failCounterPressurePlates != 0)
        {
            failCounter.Add("Pressure Plates " + level, failCounterPressurePlates);
            failCounterPressurePlates = 0;
        }

        if (failCounterWires != 0)
        {
            failCounter.Add("Wires " + level, failCounterWires);
            failCounterWires = 0;
        }

        if (failCounterChess != 0)
        {
            failCounter.Add("Chess " + level, failCounterChess);
            failCounterChess = 0;
        }
    }

    public void saveAnalytics()
    {
        if (GameManager.Instance.whichLevel != 0)
        {
            timesChecked(GameManager.Instance.currentRoom.ToString());
            missedClues(GameManager.Instance.currentRoom.ToString());
            missedItems(GameManager.Instance.currentRoom.ToString());
            missedStoryNotes(GameManager.Instance.currentRoom.ToString());
            resetlevelTimer(GameManager.Instance.currentRoom.ToString());
            puzzleFails(GameManager.Instance.currentRoom.ToString());

            timer = 0;
            print("SaveAnalytics");
        }
    }

    public void sendFinal()
    {
        addTimers();
        completed("Level Timers", timersLevels);
        completed("Puzzle Timers Part 1", timersPuzzlesp1);
        completed("Puzzle Timers Part 2", timersPuzzlesp2);
        completed("Puzzle Fail Counters", failCounter);
    }

    public void refind()
    {
        player = FindObjectOfType<playerMovement>();
        light = FindObjectOfType<Flashlight>();
    }
}
