using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script by Connor - if designers need any assistance feel free to dm or @ me on Discord

public class Flashlight : MonoBehaviour
{
    private Light lightSource; //creating lightsource to assign at runtime

    const float maxBatteryLife = 60; //total battery life 
    float batteryLife = maxBatteryLife; //current battery life will change this later after designers say what they want

    internal bool powerOn = false; //Flashlight starts disabled this manages battery consumption 

    void Start()
    {
        lightSource = this.gameObject.GetComponent<Light>(); //assigning the spotlight to access it
    }

    void Update()
    {
        toggleLight(); //runs the toggleLight function.
    }

    void toggleLight()
    {
        if (Input.GetButtonDown("Flashlight"))
        {
            powerOn = !powerOn;
            lightSource.enabled = powerOn;
        }
    }


}
