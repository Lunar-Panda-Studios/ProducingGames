using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSounds : MonoBehaviour
{
    [SerializeField] string audioClipName;

    public void playAnimationSound()
    {
        SoundEffectManager.GlobalSFXManager.PlaySFX(audioClipName);
    }
}
