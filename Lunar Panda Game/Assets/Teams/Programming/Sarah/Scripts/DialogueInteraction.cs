using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteraction : MonoBehaviour
{
    public int cutsceneID;
    bool canPlay = false;
    public float delay = 30;
    float timer = 0;
    public bool playOnlyOnce = false;
    bool beenPlayed = false;

    // Update is called once per frame
    void Update()
    {
        if(!canPlay)
        {
            timer += Time.deltaTime;

            if(timer >= delay)
            {
                canPlay = true;
                timer = 0;
            }
        }


        if ((!playOnlyOnce || !beenPlayed))
        {
            if (Input.GetButtonDown("Interact"))
            {
                if (canPlay)
                {
                    RaycastHit hit;
                    if (InteractRaycasting.Instance.raycastInteract(out hit))
                    {
                        if (hit.transform.gameObject == gameObject)
                        {
                            beenPlayed = true;
                            canPlay = false;
                            DialogueSystem.Instance.updateDialogue(cutsceneID);
                            DialogueSystem.Instance.playVoiceOver();
                        }
                    }
                }
            }
        }
    }
}
