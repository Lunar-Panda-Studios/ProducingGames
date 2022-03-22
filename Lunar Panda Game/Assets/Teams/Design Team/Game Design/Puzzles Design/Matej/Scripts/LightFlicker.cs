using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    Light light;

    public float minWaitTime;
    public float maxWaitTime;

    public float stopMinWaitTime;
    public float stopMaxWaitTime;

    public float minFlicks;
    public float maxFlicks;


    private bool isOn;
    void Start()
    {
        isOn = true;
        light = GetComponent<Light>();
        StartCoroutine(Flashing());
    }
    IEnumerator Flashing()
    {
        while(true)
        {
            if (isOn)
            {
                yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
                light.enabled = !light.enabled;
                isOn = false;
            }
            if (!isOn)
            {
                for(int i = 0; i <= Random.Range(minFlicks, maxFlicks) * 2; i++)
                {
                    yield return new WaitForSeconds(Random.Range(stopMinWaitTime, stopMaxWaitTime));
                    light.enabled = !light.enabled;
                    print(".");
                    isOn = true;
                }
                
            }
        }
        
    }
}
