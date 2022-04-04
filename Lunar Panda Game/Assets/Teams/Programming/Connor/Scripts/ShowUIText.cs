using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowUIText : MonoBehaviour
{
    public string textValue;
    public Text textElement;
    autoCombineScript combine;

    void Start()
    {
        combine = FindObjectOfType<autoCombineScript>();
    }

    void Update()
    {
        textValue = combine.fullPrompt;
        textElement.text = textValue;
    }
}
