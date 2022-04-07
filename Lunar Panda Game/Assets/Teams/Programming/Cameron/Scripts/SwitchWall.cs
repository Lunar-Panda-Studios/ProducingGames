using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWall : MonoBehaviour
{
    internal int id;
    Transform cam;
    GameObject player;
    [SerializeField] switchChanger amogusComplete;
    [SerializeField] ConnectInputsAndOutputs amogusPuzzle;
    [SerializeField] List<switchChanger> buttons;
    [SerializeField] switchChanger submitButton;
    //I would make a list of a bool array for the combinations to make it more modifiable, but they dont show in inspector without doing 
    //annoying stuff and i cba for that so yeah. 3 bool arrays it is
    [SerializeField] bool[] combination1 = new bool[4];
    [SerializeField] bool[] combination2 = new bool[4];
    [SerializeField] bool[] combination3 = new bool[4];
    [SerializeField] LabMachine labMachine;
    [SerializeField] bool[] completedCombinations = new bool[3];
    List<bool[]> combinations = new List<bool[]>();
    [SerializeField] bool[] currentCombination = new bool[4];

    void Awake()
    {
        combinations.Add(combination1);
        combinations.Add(combination2);
        combinations.Add(combination3);
        cam = Camera.main.transform;
        player = GameObject.FindGameObjectWithTag("Player");
        for (int i = 0; i < currentCombination.Length; i++)
        {
            currentCombination[i] = false;
        }
    }

    private void Start()
    {
        id = amogusPuzzle.id;
        GameEvents.current.puzzleReset += resetPuzzle;
    }

    void Update()
    {
        if (amogusComplete.isPowerOn)
        {
            CheckForButtonPress();
        }
    }

    public bool[] CheckCombination()
    {
        //this bool keeps track to see if the player inputted a correct guess
        bool madeCorrectGuess = false;
        //gets the current combination
        for(int i = 0; i < currentCombination.Length; i++)
        {
            currentCombination[i] = buttons[i].GetComponent<switchChanger>().getSwitchState();
        }
        //for each combination, check if the current combo is the same. If it is, set the completed combo to true
        for(int i = 0; i < combinations.Count; i++)
        {
            if (CompareBoolArray(combinations[i], currentCombination))
            {
                completedCombinations[i] = true;
                madeCorrectGuess = true;
            }
        }


        //if the player didnt make a correct guess, reset the lab machine and the completed combo array
        if (!madeCorrectGuess)
        {
            print("didnt make correct guess");
            for (int i = 0; i < completedCombinations.Length; i++)
            {
                completedCombinations[i] = false;
            }
            labMachine.ResetMachine(amogusPuzzle.id);
            amogusPuzzle.resetPuzzle(amogusPuzzle.id);
            amogusPuzzle.TurnOffLights();

            if (Analysis.current != null)
            {
                if (Analysis.current.consent)
                {
                    Analysis.current.failCounterAntidote++;
                }
            }
        }
        return completedCombinations;
    }

    public bool CheckAllCombos()
    {
        foreach (bool x in completedCombinations)
        {
            if (!x)
                return false;
        }
        return true;
    }

    bool CompareBoolArray(bool[] x, bool[] y)
    {
        for (int i = 0; i < x.Length; i++)
        {
            if (x[i] != y[i])
                return false;
        }
        return true;
    }

    void CheckForButtonPress()
    {
        //if the player hits E, check if the player is looking at a button related to this object. Change the switch state of that button
        RaycastHit hit;
        if (Input.GetButtonDown("Interact"))
        {
            if (Physics.Raycast(cam.position, cam.TransformDirection(Vector3.forward), out hit, player.GetComponent<PlayerPickup>().pickupDist))
            {
                foreach(switchChanger button in buttons)
                {
                    if (hit.transform.GetComponent<switchChanger>() && hit.transform.gameObject == button.gameObject)
                    {
                        button.changeSwitchState(false);
                    }
                }
                //if the submit button is pressed, Check the current combination. Turn on the tubes whos combos have been completed
                if (hit.transform.gameObject == submitButton.gameObject)
                {
                    bool[] x = CheckCombination();
                    //for each button, if its true, set it to false (reset the buttons)
                    foreach(switchChanger button in buttons)
                    {
                        if (button.getSwitchState())
                        {
                            button.changeSwitchState(true);
                        }
                    }
                    labMachine.TurnTubeOn(x);
                }
            }
        }
    }
    
     public void resetPuzzle(int id)
    {
        if (id == this.id)
        {

            amogusPuzzle.TurnOffLights();

            for (int i = 0; i < completedCombinations.Length; i++)
            {
                completedCombinations[i] = false;
            }

            for(int i = 0; i < currentCombination.Length; i++)
            {
                currentCombination[i] = false;
            }

        }
    }
}
