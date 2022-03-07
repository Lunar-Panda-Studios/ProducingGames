using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxToLights : MonoBehaviour
{
    public int id;
    bool switchState = false;
    public GameObject LinkedBox;
    public List<GameObject> LinkedLights;
    InteractRaycasting ray;

    private void Start()
    {
        ray = FindObjectOfType<InteractRaycasting>();
        LinkedBox.GetComponent<PowerChanging>().id = id;

        for(int i = 0; i < LinkedLights.Count; i++)
        {
            foreach (Transform child in LinkedLights[i].transform)
            {
                child.GetComponent<LightsChaging>().id = id;
            }
        }

    }

    private void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            RaycastHit hit;
            if (InteractRaycasting.Instance.raycastInteract(out hit))
            {
                if (hit.transform.gameObject == gameObject)
                {
                    interact();
                }
            }
        }
    }

    private void interact()
    {
        if(GetComponent<Interaction>().canInteract)
        {
            if (switchState)
            {
                GameEvents.current.onTriggerLightsOn(id);
                GameEvents.current.onPowerTurnedOff(id);
            }
            else
            {
                GameEvents.current.onTriggerLightsOff(id);
                GameEvents.current.onPowerTurnedOn(id);
            }

            switchState = !switchState;
        }
    }
}
