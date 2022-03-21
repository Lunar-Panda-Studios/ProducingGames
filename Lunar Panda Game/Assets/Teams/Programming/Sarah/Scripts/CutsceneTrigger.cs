using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneTrigger : MonoBehaviour
{
    public int cutsceneID;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            DialogueSystem.Instance.updateDialogue(cutsceneID);
            DialogueSystem.Instance.playVoiceOver();
        }
    }
}
