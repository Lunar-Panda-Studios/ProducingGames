using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingSound : MonoBehaviour
{
    private Rigidbody p_rigidbody;
    private bool playing;
    public AudioSource soundSource;

    public SoundEffectManager soundEffectManager;
    void Start()
    {
        //soundEffectManager = GetComponent<SoundEffectManager>();
        playing = true;
        p_rigidbody = gameObject.GetComponent<Rigidbody>();
        //soundEffectManager.PlaySFX("walk");
    }

    void Update()
    {
        if (p_rigidbody.velocity.x != 0 && !playing)
        {
            soundSource.UnPause();
            playing = true;
        }
        else if (p_rigidbody.velocity.x == 0 && playing)
        {
            soundSource.Pause();
            playing = false;
        }
    }
}
