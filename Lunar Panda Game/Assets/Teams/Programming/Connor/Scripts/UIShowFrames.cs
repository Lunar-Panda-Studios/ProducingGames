using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShowFrames : MonoBehaviour
{
    public string textValue;
    public Text textElement;
    public FPSCounter counter;

    private int updates = 0;
    private int totalFrames = 0;
    private float averageFrames = 0f;

    float updateTimer = 0.00f;
    [SerializeField] public float FPSUpdateRate;
    void Start()
    {
    }

    void Update()
    {
        updateTimer += Time.deltaTime;
        totalFrames += counter.FPS;
        updates++;

        if (updateTimer > FPSUpdateRate)
        {
            averageFrames = (totalFrames / updates);
            textValue = averageFrames.ToString();
            textElement.text = textValue;
            updateTimer = 0f;
            updates = 0;
            totalFrames = 0;
        }
    }
}