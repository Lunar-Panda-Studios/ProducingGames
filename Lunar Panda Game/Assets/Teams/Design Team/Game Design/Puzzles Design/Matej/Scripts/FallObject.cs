using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallObject : MonoBehaviour
{
    public string clipName;
    private void OnCollisionEnter(Collision collision)
    {
        SoundEffectManager.GlobalSFXManager.PlaySFX(clipName);
        Destroy(this);
    }

}
