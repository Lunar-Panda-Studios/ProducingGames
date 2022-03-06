using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsOutTrigger : MonoBehaviour
{
    public List<Light> Lights = new List<Light>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") TurnOffLights();
    }
    public void TurnOffLights()
    {
     foreach(Light light in Lights)
        {
            light.enabled = false;
        }
    }
}
