using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SFXTriggerNew : MonoBehaviour
{
    public AudioClip stopThisSound;
    public GameObject disableThis;

    public string audioClipName;
    public bool destroyWhenPlayed;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "MrCapsule")
        {
            SoundEffectManager.GlobalSFXManager.PlaySFX(audioClipName);
        }

        if (destroyWhenPlayed)
        {
            disableThis.SetActive(false);
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.name == "MrCapsule")
    //    {
    //        SoundEffectManager.GlobalSFXManager.StopSFX("boing");
    //    }
    //}
}