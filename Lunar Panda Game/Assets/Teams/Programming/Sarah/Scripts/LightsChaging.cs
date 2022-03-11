using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsChaging : MonoBehaviour
{
    public int id;
    public int lightIntensity;

    // Start is called before the first frame update
    private void Start()
    {
        GameEvents.current.triggerLightsOff += lightsOn;
        GameEvents.current.triggerLightsOn += lightsOff;
    }

    public void lightsOff(int id)
    {
        if (id == this.id)
        {
            GetComponent<Light>().enabled = false;
        }
    }

    public void lightsOn(int id)
    {
        if (id == this.id)
        {
            GetComponent<Light>().enabled = false;
        }
    }
}
