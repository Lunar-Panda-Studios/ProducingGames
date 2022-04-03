using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowUIText : MonoBehaviour
{
    public string textValue;
    public Text textElement;
    public autoCombineScript combine;

    void Start()
    {
    }

    void Update()
    {
        textValue = combine.fullPrompt;
        textElement.text = textValue;
    }
}
