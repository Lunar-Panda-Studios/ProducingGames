using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPuzzle : MonoBehaviour
{
    [Tooltip("id that links with the event system")]
    public int id;
    [Tooltip("If ticked, only the boxLinked variable will be used, if not then the lightsLinked would be used")]
    public bool LinkedToBox;
    [Tooltip("GameObject that has all the lights in (gameobjects with light components")]
    public GameObject lightsLinked;
    public GameObject boxLinked;
    public float lightIntensity = 20000;

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.triggerOpenDoor += openDoor;
        GameEvents.current.triggerCloseDoor += closeDoor;

        if(!boxLinked)
        {
            GameEvents.current.triggerLightsOff += lightsOn;
            GameEvents.current.triggerLightsOn += lightsOff;
        }
        else
        {
            GameEvents.current.powerOff += powerOff;
            GameEvents.current.powerOn += powerOff;
        }
    }

    public void openDoor(int id)
    {
        if(id == this.id)
        {
            GetComponent<OpenClose>().openCloseObject();
        }
    }

    public void closeDoor(int id)
    {
        if (id == this.id)
        {
            GetComponent<OpenClose>().openCloseObject();
        }
    }

    public void lightsOff(int id)
    {
        if (id == this.id)
        {
            foreach(Transform child in lightsLinked.transform)
            {
                child.GetComponent<Light>().intensity = 0;
            }
        }
    }

    public void lightsOn(int id)
    {
        if (id == this.id)
        {
            foreach (Transform child in lightsLinked.transform)
            {
                child.GetComponent<Light>().intensity = lightIntensity;
            }
        }
    }

    public void powerOn(int id)
    {
        if (id == this.id)
        {
            boxLinked.GetComponent<SwitchPuzzle>().enabled = true;
        }
    }

    public void powerOff(int id)
    {
        if (id == this.id)
        {
            boxLinked.GetComponent<SwitchPuzzle>().enabled = false;
        }
    }

}
