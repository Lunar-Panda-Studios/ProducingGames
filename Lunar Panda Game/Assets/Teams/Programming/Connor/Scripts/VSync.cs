using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VSync : MonoBehaviour
{
    public Toggle vsyncToggle;

    private void Awake()
    {
        
    }

    void Start()
    {
        //need fps counter to test, also test with this vs in proj settings. drag onto empty game obj

        QualitySettings.vSyncCount = 1;
        //QualitySettings.vSyncCount = 2;
        //QualitySettings.vSyncCount = 3;
        //QualitySettings.vSyncCount = 4;
        //QualitySettings.vSyncCount = 0;

        if(QualitySettings.vSyncCount == 0)
        {
            vsyncToggle.isOn = false;
        }
        else
        {
            vsyncToggle.isOn = true;
        }
    }

    public void toggleVSync(bool enable) 
    {
        QualitySettings.vSyncCount = enable ? 1 : 0;
    }
}
