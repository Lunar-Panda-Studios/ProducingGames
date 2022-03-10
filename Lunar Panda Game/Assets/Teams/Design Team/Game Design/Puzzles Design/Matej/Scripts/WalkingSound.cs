using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingSound : MonoBehaviour
{
    private Rigidbody p_rigidbody;
    private bool playing;
    public AudioSource soundSource;
    void Start()
    {
        playing = true;
        p_rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (p_rigidbody.velocity.x != 0 && !playing)
        {
            soundSource.UnPause();
            playing = true;
        }
        else if(p_rigidbody.velocity.x == 0 && playing)
        {
            soundSource.Pause();
            playing = false;
        }
    }
}
