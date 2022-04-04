using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pressurePlatePuzzle : MonoBehaviour
{
    [Header("Lists")]
    [Tooltip("Put in the numbers that stand in for each symbol in order here")]
    public List<int> correctOrder;
    [Tooltip("Drag every child pressure plate in here in order of their symbol number")]
    public List<GameObject> allPlates;
    [Tooltip("The symbol number where an item should go")]
    public List<int> pedestalLocations;
    private pressurePlate plateScript;

    private int currentCorrect = 0;

    [Header("Game Objects")]
    [Tooltip("Prefab of the pedestal goes here")]
    public GameObject prefabPedestal;
    private GameObject instPedestal;
    [Tooltip("The door that opens when the puzzle is solved")]
    public GameObject door;

    [Header("Puzzle")]
    [Tooltip("Checks if the puzzle is complete or not")]
    public bool puzzleComplete;

    public string completeClip;//Matej changes
    // Start is called before the first frame update
    void Start()
    {
        plateScript = FindObjectOfType<pressurePlate>();
        createPedestals();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void createPedestals()
    {
        for(int i = 0; i < pedestalLocations.Count; i++)
        {
            instPedestal = (GameObject)Instantiate(prefabPedestal, transform.position, Quaternion.Euler(0,0,0));
            instPedestal.transform.parent = gameObject.transform;
            instPedestal.transform.localPosition = new Vector3(0, 0, 4 - (2 * pedestalLocations[i]));
            instPedestal.GetComponent<platePedestal>().changePlate(allPlates[pedestalLocations[i]]);
            allPlates[pedestalLocations[i]].SetActive(false);
        }
    }

    public bool checkPuzzleCompletion()
    {
        if (currentCorrect==correctOrder.Count)
        {
            door.GetComponent<OpenDoor>().canOpen = true;
            SoundEffectManager.GlobalSFXManager.PlaySFX(completeClip);
            if (Analysis.current != null)
            {
                if (Analysis.current.consent)
                {
                    Analysis.current.resetTimer("Pressure Plates");
                }
            }
            return true;
        }
        return false;
    }

    public void resetPuzzle()
    {
        currentCorrect = 0;
        foreach(GameObject plate in allPlates)
        {
            plate.GetComponent<pressurePlate>().moving = true;
            plate.GetComponent<pressurePlate>().pressing = false;
        }
    }

    public void checkIfCorrect(int pressurePlateSymbol)
    {
        if (correctOrder[currentCorrect] == pressurePlateSymbol)
        {
            currentCorrect++;
            puzzleComplete = checkPuzzleCompletion();
        }
        else
        {
            resetPuzzle();
        }
    }

    public bool getCompletion()
    {
        
        return puzzleComplete;
    }
}
