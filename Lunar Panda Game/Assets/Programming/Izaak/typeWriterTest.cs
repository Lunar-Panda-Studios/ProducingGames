using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class typeWriterTest : MonoBehaviour
{
    [Header("UI Settings")]
    [Tooltip("Reference to the text object that will contain the typewriting text")]
    public Text dialogueText;

    [Header("Dialogue Settings")]
    [Tooltip("The final dialogue when the typing animation has finished")]
    public string dialogue;
    //What the dialogue in the animation looks like thus far
    private string currentDialogue;
    //Amount of letters revealed by the animation thus far
    private int revealedLetters;

    [Tooltip("The time in seconds between every letter that is typed. Generally very small number.")]
    public float timeBetweenLetters;
    private float timeBetweenLettersCounter;

    bool playText = false;

    // Start is called before the first frame update
    void Start()
    {
        setupText();
    }

    // Update is called once per frame
    void Update()
    {
        if (playText)
        {
            timeBetweenLettersCounter -= Time.deltaTime;
            //If timer runs out and there are still more letters to print, print the next letter
            if ((timeBetweenLettersCounter <= 0) && (currentDialogue.Length < dialogue.Length))
            {
                revealedLetters++;
                currentDialogue = dialogue.Substring(0, revealedLetters);
                timeBetweenLettersCounter = timeBetweenLetters;
                dialogueText.text = currentDialogue;
            }
        }
    }

    public void setupText()
    {
        //Sets up a timer that acts when it hits 0
        timeBetweenLettersCounter = timeBetweenLetters;
        //Empties the current dialogue ready for use
        currentDialogue = ("");
    }
}
