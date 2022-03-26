using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HorrorTriggerv2 : MonoBehaviour
{
    //The logic behind the list
    [System.Serializable]
    public struct HorrorData//data for the event
    {
        public HorrorEvent state;
        public bool disableAtStart;
        public float delayBeforeMovingAgain;
    }
    [SerializeField]
    public List<HorrorData> Events = new List<HorrorData>();

    public enum HorrorEvent//Type of the event
    {

    }
    public HorrorEvent state;
    private int check;//This is the main logic for this all to work. !FRAGILE!

    private void OnTriggerEnter(Collider other)//Trigger duh
    {
        if (other.gameObject.tag == "Player")
        {
            foreach (HorrorData data in Events)//Every event in the list of 
            {
                StartTrigger(data);
            }
            if (check == Events.Count) Destroy(this.gameObject);//Checks if all of the events are completed, then deletes the trigger
        }
    }
    public void StartTrigger(HorrorData data)// This activates the trigger depending on the type of the trigger
    {
        switch (data.state)
        {
            default: break;
        }
    }

}
