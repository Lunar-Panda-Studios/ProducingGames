using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShowFrames : MonoBehaviour
{
    public string textValue;
    public Text textElement;
    public FPSCounter counter;
    void Start()
    {
    }

    void Update()
    {
        textValue = counter.FPS.ToString();
        textElement.text = textValue;
    }
}