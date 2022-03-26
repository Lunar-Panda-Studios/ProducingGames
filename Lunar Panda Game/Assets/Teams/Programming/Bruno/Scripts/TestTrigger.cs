using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTrigger : MonoBehaviour
{
    //The logic behind the list
    [System.Serializable]
    public struct HorrorData
    {
        public HorrorEvent state;
        public bool disableAtStart;
        public float delayBeforeMovingAgain;
    }
    [SerializeField]
    public List<HorrorData> DataList = new List<HorrorData>();

    public enum HorrorEvent
    {

    }
    public HorrorEvent state;
    private int check;//This is the main logic for this all to work. !FRAGILE!

    private void OnTriggerEnter(Collider other)//Trigger duh
    {
        if (other.gameObject.tag == "Player")
        {
            foreach (HorrorData data in DataList)//Every event in the list of 
            {
                StartTrigger(data);
            }
            if (check == DataList.Count) Destroy(this.gameObject);//Checks if all of the events are completed, then delete the trigger
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
