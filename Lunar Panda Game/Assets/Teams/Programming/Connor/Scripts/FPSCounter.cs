using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] private int FPS;

    // Update is called once per frame
    void Update()
    {
        FPS = (int)(1f / Time.unscaledDeltaTime);
    }
}
