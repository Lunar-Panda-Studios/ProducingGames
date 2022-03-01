using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VSync : MonoBehaviour
{
    void Start()
    {
        //need fps counter to test, also test with this vs in proj settings. drag onto empty game obj

        QualitySettings.vSyncCount = 1;
        //QualitySettings.vSyncCount = 2;
        //QualitySettings.vSyncCount = 3;
        //QualitySettings.vSyncCount = 4;
        //QualitySettings.vSyncCount = 0;
    }
}
