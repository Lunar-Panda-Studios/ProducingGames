using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SFXTriggerNew : MonoBehaviour
{
    public AudioClip stopThisSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "MrCapsule")
        {
            SoundEffectManager.GlobalSFXManager.PlaySFX("boing");
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
