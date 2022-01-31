using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script by Connor - if designers need any assistance feel free to dm or @ me on Discord

public class Flashlight : MonoBehaviour
{
    public KeyCode toggleLightButton; //assign this in the Unity Editor
    private Light lightSource; //creating lightsource to assign at runtime

    const float maxBatteryLife = 60; //total battery life 
    public float powerUsage = 1f;
    float batteryLife = maxBatteryLife; //current battery life will change this later after designers say what they want

    bool powerOn = false; //Flashlight starts disabled this manages battery consumption 

    void Start()
    {
        lightSource = this.gameObject.GetComponent<Light>(); //assigning the spotlight to access it
        lightSource.intensity = 0;
    }

    void Update()
    {
        toggleLight(); //runs the toggleLight function.
    }

    void toggleLight()
    {
        if (Input.GetKeyDown(toggleLightButton) && batteryLife > 0) //press key assigned in unity editor to run this code.
        {
            switch (powerOn)
            {
                case false: //if flashlight off, turn on
                    powerOn = true;
                    lightSource.intensity = 10;
                    break;

                case true: //if flashlight on turn off
                    powerOn = false;
                    lightSource.intensity = 0;
                    break;
            }
        }

        if (powerOn && batteryLife > 0) //uses battery when flashlight is on.
        {
            batteryLife -= (powerUsage * Time.deltaTime); //set usage to 1 in Unity inspector for it to lose 1 batteryLife/second. batteryLife is set to 60.

            if (batteryLife <= 0)
            {
                powerOn = false;
                lightSource.intensity = 0; // if battery hits 0 turn off flashlight
            }
        }
    }


}
