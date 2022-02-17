using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomSoundScript : MonoBehaviour
{

    public KeyCode testSound1;
    public KeyCode testSound2;

    void Update()
    {
        if (Input.GetKeyDown(testSound1))
        {
            SoundEffectManager.GlobalSFXManager.PlaySFX("SFX 1");
        }

        if (Input.GetKeyDown(testSound2))
        {
            SoundEffectManager.GlobalSFXManager.PlaySFX("SFX 2");
        }
    }
}
