using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingSound : MonoBehaviour
{
    private Rigidbody p_rigidbody;
    private bool playing;
    public AudioSource soundSource;
    public enum TypeOfFloor
    {
        Wood,
        Concrete,
        Puddle,
        Carpet,
        Grass,
        Chips,
        Tiles
    }
    public TypeOfFloor typeOfFloor;

    public SoundEffectManager soundEffectManager;
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
        else if (p_rigidbody.velocity.x == 0 && playing)
        {
            soundSource.Pause();
            playing = false;
        }
    }
}
