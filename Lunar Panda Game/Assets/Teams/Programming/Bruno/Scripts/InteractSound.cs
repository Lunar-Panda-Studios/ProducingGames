using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractSound : MonoBehaviour
{
    InteractRaycasting raycast;

    public string audioClipName;    

    private void Start()
    {
        raycast = FindObjectOfType<InteractRaycasting>(); // Needed to stop the object rotating from activating the script as well        
    }

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
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Currently using Connor's raycast script

        if (raycast.raycastInteract(out hit))
        {
            if (hit.transform.gameObject == gameObject)
            {
                SoundEffectManager.GlobalSFXManager.PlaySFX(audioClipName);
            }
        }
    }
}
