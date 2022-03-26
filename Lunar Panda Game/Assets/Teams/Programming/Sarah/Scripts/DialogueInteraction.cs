using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteraction : MonoBehaviour
{
    public int cutsceneID;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            RaycastHit hit;
            if (InteractRaycasting.Instance.raycastInteract(out hit))
            {
                if (hit.transform.gameObject == gameObject)
                {
                    DialogueSystem.Instance.updateDialogue(cutsceneID);
                    DialogueSystem.Instance.playVoiceOver();
                }
            }
        }
    }
}
