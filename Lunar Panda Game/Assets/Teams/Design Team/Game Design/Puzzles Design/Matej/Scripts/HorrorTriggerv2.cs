using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class HorrorTriggerv2 : MonoBehaviour
{
    public List<EventData> Events = new List<EventData>();

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            foreach(EventData data in Events)
            {
                StartTrigger(data);
            }
        }
    }
    public void StartTrigger(EventData data)
    {
        if (data.sort == EventSort.Destroy) DestroyFunction();
    }
    public void DestroyFunction()
    {

    }
}
[System.Serializable]
public enum EventSort
{
    Destroy,
    Instantiate
}
[System.Serializable]
public class EventData
{
    public EventSort sort;
    public GameObject obj;
    public Vector3 position;
    public Quaternion rotation;
    public float waitTime;
}
[System.Serializable]
public class LightsOut : EventData
{
    public GameObject light;
}
