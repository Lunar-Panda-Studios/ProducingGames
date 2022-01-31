using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewDocument : MonoBehaviour
{
    private bool showText = false;
    public GameObject text;
    void OnMouseDown()
    {

        if (!showText)
        {
            showText = true;
            text.gameObject.SetActive(true);
            //Show text when pressed
        }

        else
            if (showText)
        {
            showText = false;
            text.gameObject.SetActive(false);
            //Hide text when pressed
        }
    }

}
