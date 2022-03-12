using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsPower : MonoBehaviour
{
    public switchChanger powerSwitch;
    public GameObject lightsToTurnOn;
    
    void Update()
    {

        if (powerSwitch.isPowerOn == true)
        {
            lightsToTurnOn.SetActive(true);
        }

        if (powerSwitch.isPowerOn == false)
        {
            lightsToTurnOn.SetActive(false);
        }
    }
}
