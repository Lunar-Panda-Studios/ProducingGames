using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorToLights : MonoBehaviour
{
    public int id;
    public GameObject lightsLinked;
    public int lightIntensity;

    private void Start()
    {
        GameEvents.current.triggerLightsOff += lightsOn;
        GameEvents.current.triggerLightsOn += lightsOff;
        GameEvents.current.triggerOpenDoor += openDoor;
        GameEvents.current.triggerCloseDoor += closeDoor;
    }


    public void openDoor(int id)
    {
        if (id == this.id)
        {
            GetComponent<OpenClose>().openCloseObject(id);
        }
    }

    public void closeDoor(int id)
    {
        if (id == this.id)
        {
            GetComponent<OpenClose>().openCloseObject(id);
        }
    }

    public void lightsOff(int id)
    {
        if (id == this.id)
        {
            foreach (Transform child in lightsLinked.transform)
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
}
