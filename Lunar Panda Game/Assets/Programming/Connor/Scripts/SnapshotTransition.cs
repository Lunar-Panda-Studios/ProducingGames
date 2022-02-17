using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SnapshotTransition : MonoBehaviour
{

    public AudioMixer AM;
    
    public enum TransitionType
    {
        Snapshot,
        ExposedVar
    }
    public TransitionType MyTransition;

    public AudioMixerSnapshot Amb_On;
    public AudioMixerSnapshot Amb_Off;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "MrCapsule")
        {
            switch (MyTransition)
            {
                case TransitionType.Snapshot:
                    Amb_On.TransitionTo(1);
                    break;
                case TransitionType.ExposedVar:
                    break;
                default:
                    break;
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "MrCapsule")
        {
            switch (MyTransition)
            {
                case TransitionType.Snapshot:
                    Amb_Off.TransitionTo(1);
                    break;
                case TransitionType.ExposedVar:
                    AM.SetFloat("AmbVol", -80);
                    break;
                default:
                    break;
            }
        }
    }
}
