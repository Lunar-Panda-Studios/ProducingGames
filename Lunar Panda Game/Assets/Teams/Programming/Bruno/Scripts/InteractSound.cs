using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractSound : MonoBehaviour
{

    public string audioClipName;    

    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            playSound();
        }
    }

    void playSound()
    {
        //Currently using Connor's raycast script

        if (InteractRaycasting.Instance.raycastInteract(out RaycastHit hit))
        {
            if (hit.transform.gameObject == gameObject)
            {
                print("I played sound");
                SoundEffectManager.GlobalSFXManager.PlaySFX(audioClipName);
            }
        }
    }
}
