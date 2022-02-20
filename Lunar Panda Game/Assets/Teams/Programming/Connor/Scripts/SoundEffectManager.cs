using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundEffectManager : MonoBehaviour
{
    public List<string> ClipNames = new List<string>();
    public List<AudioClip> ClipList = new List<AudioClip>();
    private Dictionary<string, AudioClip> SFX_Library = new Dictionary<string, AudioClip>();

    public GameObject SFX_Prefab;
    AudioSource TheSFX;
    public AudioSource stopSFX;


    public static SoundEffectManager GlobalSFXManager;
    void Start()
    {
        GlobalSFXManager = this;

        for (int i = 0; i <ClipNames.Count; i++)
        {
            SFX_Library.Add(ClipNames[i], ClipList[i]);
        }
    }

    public void PlaySFX(string ClipName)
    {
        if (SFX_Library.ContainsKey(ClipName))
        {
            TheSFX = Instantiate(SFX_Prefab).GetComponent<AudioSource>();
            TheSFX.PlayOneShot(SFX_Library[ClipName]); // Sets clip and plays it
            Destroy(TheSFX.gameObject, SFX_Library[ClipName].length);
        }
    }

}
