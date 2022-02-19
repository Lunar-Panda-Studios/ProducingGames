using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWall : MonoBehaviour
{
    Transform cam;
    GameObject player;
    [SerializeField] List<switchChanger> buttons;
    [SerializeField] switchChanger submitButton;
    //I would make a list of a bool array for the combinations to make it more modifiable, but they dont show in inspector without doing 
    //annoying stuff and i cba for that so yeah. 3 bool arrays it is
    [SerializeField] bool[] combination1 = new bool[4];
    [SerializeField] bool[] combination2 = new bool[4];
    [SerializeField] bool[] combination3 = new bool[4];
    bool[] completedCombinations = new bool[3];
    List<bool[]> combinations;
    bool[] currentCombination = new bool[4];

    void Awake()
    {
        combinations.Add(combination1);
        combinations.Add(combination2);
        combinations.Add(combination3);
        cam = Camera.main.transform;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public bool[] CheckCombination()
    {
        for(int i = 0; i < currentCombination.Length; i++)
        {
            currentCombination[i] = buttons[i].GetComponent<switchChanger>().getSwitchState();
        }
        for(int i = 0; i < combinations.Count; i++)
        {
            if (CompareBoolArray(combinations[i], currentCombination))
            {
                completedCombinations[i] = true;
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
        for (int i = 0; i <= x.Length; i++)
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
                        button.changeSwitchState();
                    }
                    else if (hit.transform.gameObject == submitButton.gameObject)
                    {
                        bool[] x = CheckCombination();

                    }
                }
            }
        }
    }
    
        
}
