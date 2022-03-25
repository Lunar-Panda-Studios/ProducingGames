using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class FogMechanic : MonoBehaviour
{
    Volume volume;
    Fog fog;

    float maxFog = 10;
    float minFog = 1.5f;
    float currentFog = 1;

    public float startTimer = 20;
    public float killTimer;

    private void Awake()
    {
        volume = gameObject.GetComponentInChildren<Volume>();
        Fog tempFog;
        if (volume.profile.TryGet<Fog>(out tempFog))
        {
            fog = tempFog;
        }
    }

    private void Start()
    {
        killTimer = startTimer;
        fog.meanFreePath.value = maxFog;
        currentFog = fog.meanFreePath.value;
    }

    private void Update()
    {
        killTimer -= 1 * Time.deltaTime;
        if (killTimer <= 0)
        {
            currentFog -= ((1 * Time.deltaTime) / 3);
            fog.meanFreePath.value = currentFog;
        }
    }
}
