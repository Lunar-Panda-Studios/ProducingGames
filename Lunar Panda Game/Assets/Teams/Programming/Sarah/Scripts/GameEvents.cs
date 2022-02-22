using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    // Start is called before the first frame update

    private void Awake()
    {
        current = this;
    }

    public event Action<int> powerOn;
    public event Action<int> powerOff;
    public event Action<int> puzzleCompleted;
    public event Action<int> puzzleReset;
    public event Action<int> triggerSound;
    public event Action<int> triggerDialog;
    public event Action<int> triggerOpenDoor;
    public event Action<int> triggerCloseDoor;
    public event Action<int> triggerLightsOn;
    public event Action<int> triggerLightsOff;
    public event Action<int> triggerStoryNotes;

    public void onPowerTurnedOn(int id)
    {
        if (powerOn != null)
        {   
            powerOn(id);
        }
    }

    public void onPowerTurnedOff(int id)
    {
        if (powerOff != null)
        {
            powerOff(id);
        }
    }

    public void onPuzzleComplete(int id)
    {
        if(puzzleCompleted != null)
        {
            puzzleCompleted(id);
        }
    }

    public void onPuzzleReset(int id)
    {
        if (puzzleReset != null)
        {
            puzzleReset(id);
        }
    }

    public void onSoundTrigger(int id)
    {
        if(triggerSound != null)
        {
            triggerSound(id);
        }
    }

    public void onTriggerDialog(int id)
    {
        if(triggerDialog != null)
        {
            triggerDialog(id);
        }
    }

    public void onTriggerOpenDoor(int id)
    {
        if(triggerOpenDoor != null)
        {
            triggerOpenDoor(id);
        }
    }

    public void onTriggerCloseDoor(int id)
    {
        if (triggerCloseDoor != null)
        {
            triggerCloseDoor(id);
        }
    }

    public void onTriggerLightsOn(int id)
    {
        if (triggerLightsOn != null)
        {
            triggerLightsOn(id);
        }
    }

    public void onTriggerLightsOff(int id)
    {
        if (triggerLightsOff != null)
        {
            triggerLightsOff(id);
        }
    }

    public void onTriggerStoryNotes(int id)
    {
        if (triggerStoryNotes != null)
        {
            triggerStoryNotes(id);
        }
    }
}
