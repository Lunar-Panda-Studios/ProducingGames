using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingSound : MonoBehaviour
{
    private Rigidbody p_rigidbody;
    private bool playing;
    public AudioSource soundSource;
    [SerializeField] float buffer = 0.1f;
    internal bool canMakeSound = true;

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
        //playing = true;
        p_rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (canMakeSound && Time.timeScale > 0.1f)
        {
            Walking();
        }
        else
        {
            soundSource.enabled = false;
            playing = false;
        }
    }

    void Walking()
    {
        Vector2 velocityV2 = new Vector2(p_rigidbody.velocity.x, p_rigidbody.velocity.z);
        float velocityMag = velocityV2.magnitude;

        if (!soundSource.isActiveAndEnabled)
        {
            soundSource.enabled = true;
        }

        if ((velocityMag > buffer || velocityMag < -buffer) && !playing && Time.timeScale > 0)
        {
            if (!soundSource.isPlaying)
            {
                soundSource.Play();
                soundSource.UnPause();
                playing = true;
            }
        }

        else if ((velocityMag > -buffer && velocityMag < buffer) && playing)
        {
            soundSource.Pause();
            playing = false;
        }
    }
}