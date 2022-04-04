using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingSound : MonoBehaviour
{
    private Rigidbody p_rigidbody;
    private bool playing;
    public AudioSource soundSource;
    [SerializeField] float buffer = 0.1f;
    public bool canMakeSound = true;

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
        if (canMakeSound && Time.timeScale > 0)
        {
            if (!soundSource.isActiveAndEnabled)
                soundSource.enabled = true;
            Vector2 velocityV2 = new Vector2(p_rigidbody.velocity.x, p_rigidbody.velocity.z);
            float velocityMag = velocityV2.magnitude;
            
            if ((velocityMag > buffer || velocityMag < -buffer) && !playing && Time.timeScale > 0)
            {
                if (!soundSource.isPlaying)
                    soundSource.Play();
                soundSource.UnPause();
                playing = true;
            }
            else if ((velocityMag > -buffer && velocityMag < buffer) && playing)
            {
                soundSource.Pause();
                playing = false;
            }
        }
        else
        {
            soundSource.enabled = false;
            playing = false;
        }
    }
}