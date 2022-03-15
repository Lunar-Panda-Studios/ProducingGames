using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsPower : MonoBehaviour
{
    public switchChanger powerSwitch;
    public GameObject lightsToTurnOn;
    public GameObject lightsToTurnOn2;
    
    void Update()
    {

        if (powerSwitch.isPowerOn == true)
        {
            lightsToTurnOn.SetActive(true);
            lightsToTurnOn2.SetActive(true);

        }

        if (powerSwitch.isPowerOn == false)
        {
            lightsToTurnOn.SetActive(false);
            lightsToTurnOn2.SetActive(false);

        }
    }
}
