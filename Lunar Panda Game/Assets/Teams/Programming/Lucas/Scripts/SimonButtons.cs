using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonButtons : MonoBehaviour
{
    private int sequenceNum = 0;
    void Update()
    {
        
    }

    void OnMouseDown()
    { 
        if (sequenceNum == 0)
        {
            //Checking if button pressed is right sequence stuff, then moves on to next one
            sequenceNum += 1;
        }

        if (sequenceNum == 1)
        {

            sequenceNum += 1; 
        }

        if (sequenceNum == 2)
        {

            sequenceNum += 1;
        }

        if (sequenceNum == 3)
        {

            sequenceNum += 1;
        }
    }
}
