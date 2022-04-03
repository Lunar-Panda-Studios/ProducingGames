using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsChaging : MonoBehaviour
{
    public int id;
    float lightIntensity;

    // Start is called before the first frame update
    private void Start()
    {
        lightIntensity = GetComponent<Light>().intensity;
        GameEvents.current.triggerLightsOff += lightsOn;
        GameEvents.current.triggerLightsOn += lightsOff;
    }

    public void lightsOff(int id)
    {
        if (id == this.id)
        {
            GetComponent<Light>().intensity = 0;
        }
    }

    public void lightsOn(int id)
    {
        if (id == this.id)
        {
            GetComponent<Light>().intensity = lightIntensity;
        }
    }
}
