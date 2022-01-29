using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script by Connor - if designers need any assistance feel free to dm or @ me on Discord

public class Flashlight : MonoBehaviour
{
    public GameObject lightSource;

    const float maxBatteryLife = 60; //total battery life 
    float batteryLife = maxBatteryLife; //current battery life will change this later after designers say what they want
    bool powerOn = false; //Flashlight starts disabled

    void Start()
    {
        
    }

    void Update()
    {
        toggleLight();
    }

    void toggleLight()
    {
        if (Input.GetKeyDown("")) //Insert key here (I didn't want to mess with project settings on my branch)
        {
            switch (powerOn)
            {
                case false:
                    powerOn = true;
                    break;

                case true:
                    powerOn = false;
                    break;
            }
        }

        if (!powerOn)
        {
            lightSource.SetActive(false);
        }

        else
        {
            lightSource.SetActive(true);
        }
    }

}
