using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayOnTrigger : MonoBehaviour
{

    public bool AlreadyPlayed;
    public AudioSource Clip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (AlreadyPlayed == false)
            {
                AlreadyPlayed = true;
                Clip.Play();
            }
        }
    }
}
