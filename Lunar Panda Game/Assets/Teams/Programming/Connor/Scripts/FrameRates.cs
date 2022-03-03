using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRates : MonoBehaviour
{
    const int peasantFrameRate = 30;
    const int defaultFrameRate = 60;
    const int higherFrames = 144;

    public int setGameFPS = defaultFrameRate;
    private int chosenFPS;

    void Start()
    {
        Application.targetFrameRate = setGameFPS;
        chosenFPS = setGameFPS;
    }

    void Update()
    {
        if (setGameFPS != chosenFPS)
        {
            Application.targetFrameRate = setGameFPS;
        }
    }
}
