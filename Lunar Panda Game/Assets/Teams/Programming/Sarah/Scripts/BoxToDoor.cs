using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxToDoor : MonoBehaviour
{
    public int id;
    bool switchState = true;

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
