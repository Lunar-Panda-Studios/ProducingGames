using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script by Connor - if designers need any assistance feel free to dm or @ me on Discord

public class Flashlight : MonoBehaviour
{
    private Light lightSource; //creating lightsource to assign at runtime

    const float maxBatteryLife = 60; //total battery life 
    float batteryLife = maxBatteryLife; //current battery life will change this later after designers say what they want

    internal bool powerOn = true; //Flashlight starts disabled this manages battery consumption 


    //Matej changes - just flickering :)
    public float minWaitTime;
    public float maxWaitTime;

    public float stopMinWaitTime;
    public float stopMaxWaitTime;

    public float minFlicks;
    public float maxFlicks;


    private bool isOn;

    void Start()
    {
        lightSource = this.gameObject.GetComponent<Light>(); //assigning the spotlight to access it
        isOn = true;
        StartCoroutine(Flashing());
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

    IEnumerator Flashing()
    {
        while (true)
        {
            if (isOn)
            {
                yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
                lightSource.enabled = !lightSource.enabled;
                isOn = false;
            }
            if (!isOn)
            {
                for (int i = 0; i <= Random.Range(minFlicks, maxFlicks) * 2; i++)
                {
                    yield return new WaitForSeconds(Random.Range(stopMinWaitTime, stopMaxWaitTime));
                    lightSource.enabled = !lightSource.enabled;
                    print(".");
                    isOn = true;
                }

            }
        }

    }


}
