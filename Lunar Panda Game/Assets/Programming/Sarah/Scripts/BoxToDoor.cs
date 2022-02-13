using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxToDoor : MonoBehaviour
{
    public int id;
    public GameObject boxLinked;

    private void Start()
    {
        GameEvents.current.triggerOpenDoor += openDoor;
        GameEvents.current.triggerCloseDoor += closeDoor;
        GameEvents.current.powerOff += powerOff;
        GameEvents.current.powerOn += powerOff;
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

    public void powerOn(int id)
    {
        if (id == this.id)
        {
            boxLinked.GetComponent<DoorToLights>().enabled = true;
        }
    }

    public void powerOff(int id)
    {
        if (id == this.id)
        {
            boxLinked.GetComponent<DoorToLights>().enabled = false;
        }
    }
}
