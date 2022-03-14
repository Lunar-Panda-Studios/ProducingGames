using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorisBabbles : MonoBehaviour
{
    [SerializeField] GameObject[] buttons;
    [SerializeField] bool randomSequence = false;
    [Tooltip("The order that the buttons should be pressed in. Top left is 0, left of that is 1 etc")]
    [SerializeField] List<int> buttonOrder;
    Material baseMat;
    [SerializeField] Material greenMat;
    [SerializeField] Material redMat;
    List<int> buttonInput = new List<int>();

    [SerializeField] bool playerCanInput = false;
    bool showingOrder = false;

    [SerializeField] float timeBetweenDisplayingOrder = 2f;
    [Tooltip("This is temporary. Just used to activate the animation of the lid opening")]
    [SerializeField] GameObject lid;
    public Room inRoom;

    void Awake()
    {
        baseMat = buttons[0].GetComponent<MeshRenderer>().material;
        //Generates a random sequence
        if (randomSequence)
        {
            for (int i = 0; i < buttonOrder.Count; i++)
            {
                buttonOrder[i] = Random.Range(0, 9);
            }
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if (InteractRaycasting.Instance.raycastInteract(out RaycastHit hit))
            {
                if(!showingOrder && !playerCanInput)
                {
                    CheckForInitialInteraction(hit);
                }
                else if (!showingOrder && playerCanInput) //the playerCanInput check may be irrelevent, but i dont care
                {
                    CheckForInput(hit);
                }
            }
        }
    }

    void CheckForInitialInteraction(RaycastHit hit)
    {
        //if the player presses E on the main briefcase, or the base of the buttons
        if(hit.transform.CompareTag("BorisBox"))
        {
            StartCoroutine(DisplayOrder());
        }
        /*else
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if(hit.transform.gameObject == buttons[i])
                {
                    StartCoroutine(DisplayOrder());
                }
            }
        }*/
    }

    IEnumerator DisplayOrder()
    {
        showingOrder = true;
        for (int i = 0; i < buttonOrder.Count; i++)
        {
            buttons[buttonOrder[i]].GetComponent<MeshRenderer>().material = greenMat;
            yield return new WaitForSeconds(timeBetweenDisplayingOrder);
            buttons[buttonOrder[i]].GetComponent<MeshRenderer>().material = baseMat;
        }
        showingOrder = false;
        playerCanInput = true;
    }

    void CheckForInput(RaycastHit hit)
    {
        if (hit.transform.gameObject == gameObject)
        {

        }
        else
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (hit.transform.gameObject == buttons[i])
                {
                    buttonInput.Add(i);
                    if(buttonInput[buttonInput.Count - 1] != buttonOrder[buttonInput.Count - 1])
                    {
                        StartCoroutine(IncorrectInput());
                    }
                    else if (buttonInput.Count >= buttonOrder.Count)
                    {
                        //these nested if's are getting outta hand....
                        if (isInputCorrect())
                        {
                            //run IEnumerator that changes all the buttons to green and then opens the briefcase
                            StartCoroutine(CorrectInput());

                            if (FindObjectOfType<Analysis>() != null)
                            {
                                if (Analysis.current.consent)
                                {
                                    string name = "SimonSays" + inRoom.ToString();
                                    Analysis.current.resetTimer(name);
                                }
                            }
                        }
                        else
                        {
                            //run IEnumerator that displays the buttons as all red and then resets the puzzle
                            StartCoroutine(IncorrectInput());
                        }
                    }
                    else
                    {
                        StartCoroutine(ButtonPress(buttons[i]));
                    }
                }
            }
        }
    }

    IEnumerator ButtonPress(GameObject btn)
    {
        btn.GetComponent<MeshRenderer>().material = greenMat;
        yield return new WaitForSeconds(0.2f);
        btn.GetComponent<MeshRenderer>().material = baseMat;
    }

    IEnumerator CorrectInput()
    {
        playerCanInput = false;
        showingOrder = true;
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<MeshRenderer>().material = greenMat;
        }
        yield return new WaitForSeconds(0.2f);
        lid.GetComponent<Animation>().Play();
    }

    IEnumerator IncorrectInput()
    {
        playerCanInput = false;
        showingOrder = true;
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<MeshRenderer>().material = redMat;
        }
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<MeshRenderer>().material = baseMat;
        }
        showingOrder = false;
        buttonInput.Clear();
    }

    bool isInputCorrect()
    {
        for (int i = 0; i < buttonInput.Count; i++)
        {
            if (buttonInput[i] != buttonOrder[i]) return false;
        }
        return true;
    }
}
