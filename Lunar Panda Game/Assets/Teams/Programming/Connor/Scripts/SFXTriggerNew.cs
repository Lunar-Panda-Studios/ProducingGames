using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXTriggerNew : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "MrCapsule")
        {
            SoundEffectManager.GlobalSFXManager.PlaySFX("Boing");
        }
    }
}
