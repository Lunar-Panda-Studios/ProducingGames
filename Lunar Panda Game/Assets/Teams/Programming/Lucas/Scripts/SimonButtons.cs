using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonButtons : MonoBehaviour
{
    private SimonSays simonScript;
    public GameObject simonPuzzle;

    private int sequenceNum = 0;

    void Start()
    {
        simonScript = simonPuzzle.GetComponent<SimonSays>();
    }

    void Update()
    {
        
    }

    void checkAnswer()
    {
        int currentCorrectAnswers = simonScript.getCurrentCorrectAnswers();
        if (gameObject == simonScript.sequenceOrder[currentCorrectAnswers])
        {
            currentCorrectAnswers++;
            simonScript.changeCorrectAnswers(currentCorrectAnswers);
            //gameObject.GetComponent<Image>().color = Color.green;
        }
        else
        {
            currentCorrectAnswers = 0;
            simonScript.resetPuzzle();
        }
    }

}
