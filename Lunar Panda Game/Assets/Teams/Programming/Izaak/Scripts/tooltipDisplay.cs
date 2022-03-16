using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tooltipDisplay : MonoBehaviour
{
    [Header("Text")]
    [Tooltip("Drag itself into this slot if it isn't already in there")]
    public Text tooltipText;

    public void changeText(string tooltip)
    {
        tooltipText.text = tooltip;
    }
}
