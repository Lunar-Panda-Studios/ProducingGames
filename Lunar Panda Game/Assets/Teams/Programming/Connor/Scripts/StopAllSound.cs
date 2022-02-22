using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopAllSound : MonoBehaviour
{

    public void stopAllSound()
    {
        AudioSource[] audio = FindObjectsOfType<AudioSource>();

        for(int i = 0; i < audio.Length; i++)
        {
            audio[i].Stop();
        }
    }

    //Thank you Sarah
}
