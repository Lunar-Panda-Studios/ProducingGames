using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DoorAndBox
{
    public OpenClose linkedDoor;
    public PowerChanging linkedBox;
}

public class BoxToDoor : MonoBehaviour
{
    public int id;
    public DoorAndBox LinkedObjects;
    bool switchState = true;

    private void Start()
    {
        LinkedObjects.linkedBox.id = id;
        LinkedObjects.linkedDoor.id = id;
    }

    private void OnMouseDown()
    {
        if(GetComponent<Interaction>().canInteract)
        {
            if (switchState)
            {
                GameEvents.current.onTriggerOpenDoor(id);
                GameEvents.current.onPowerTurnedOff(id);
            }
            else
            {
                GameEvents.current.onTriggerCloseDoor(id);
                GameEvents.current.onPowerTurnedOn(id);
            }
        }

        switchState = !switchState;
    }
}
