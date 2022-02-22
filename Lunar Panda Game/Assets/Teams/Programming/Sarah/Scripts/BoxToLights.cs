using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxToLights : MonoBehaviour
{
    public int id;
    bool switchState = false;
    public GameObject LinkedDoor;
    public GameObject LinkedLights;
    InteractRaycasting ray;

    private void Start()
    {
        ray = FindObjectOfType<InteractRaycasting>();
        LinkedDoor.GetComponent<OpenClose>().id = id;
        LinkedLights.GetComponent<LightsChaging>().id = id;
    }

    private void Update()
    {
        RaycastHit hit;

        if (ray.raycastInteract(out hit))
        {
                if (Input.GetButtonDown("Interact"))
                {
                    interact();
                }
        }
    }

    private void interact()
    {
        if(GetComponent<Interaction>().canInteract)
        {
            if (switchState)
            {
                GameEvents.current.onTriggerLightsOff(id);
                GameEvents.current.onPowerTurnedOn(id);
            }
            else
            {
                GameEvents.current.onTriggerLightsOn(id);
                GameEvents.current.onPowerTurnedOff(id);
            }

            switchState = !switchState;
        }
    }
}
