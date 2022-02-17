using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorToLights : MonoBehaviour
{
    public int id;
    bool switchState = false;

    private void OnMouseDown()
    {
        if(GetComponent<Interaction>().canInteract)
        {
            if (switchState)
            {
                GameEvents.current.onTriggerCloseDoor(id);
                GameEvents.current.onTriggerLightsOn(id);
            }
            else
            {
                GameEvents.current.onTriggerOpenDoor(id);
                GameEvents.current.onTriggerLightsOff(id);
            }

            switchState = !switchState;
        }
    }
}
