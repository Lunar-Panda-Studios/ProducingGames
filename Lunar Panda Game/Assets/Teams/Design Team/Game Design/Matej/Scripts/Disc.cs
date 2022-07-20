using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Disc : MonoBehaviour
{
    //Author : Matej Gajdos - Gameplay Design
    //When editing please use comments to mark the changes
    //This script is used in logic and animation of the food chain puzzle

    [Header("Random Numbers")]
    //Basic Logic
    int minNumber = 0;
    int maxNumber = 9;
    int numberOne;
    int numberTwo;
    int numberThree;
    int numberFour;

    //Differentiating the discs themselves. Each Disc registers different animal as the main number.
    //We need to keep track of this to know what the code for the safe will be 
    public enum Animal
    {
        Tiger,
        Wolf,
        Fox,
        Rabbit
    }
    public Animal type;
    public int theNumber;

    //In-game objects / TextObjects

    public TextMeshPro tigerNumber;
    public TextMeshPro wolfNumber;
    public TextMeshPro foxNumber;
    public TextMeshPro rabbitNumber;

    //Animation script
    [Header("Animation")]
    public FoodChainBrain brain;
    public char size;
    public Animator animator;
    public int beginPhase;

    //Audio settings
    AudioSource audioSource;

    void Awake()
    {
        //Decide the numbers
        numberOne = Randomize(minNumber, maxNumber);
        numberTwo = Randomize(minNumber, maxNumber);
        numberThree = Randomize(minNumber, maxNumber);
        numberFour = Randomize(minNumber, maxNumber);
        //Show the numbers
        tigerNumber.text = numberOne.ToString();
        wolfNumber.text = numberTwo.ToString();
        foxNumber.text = numberThree.ToString();
        rabbitNumber.text = numberFour.ToString();
        switch (type)
        {
            case Animal.Tiger:
                theNumber = numberOne;
                break;
            case Animal.Wolf:
                theNumber = numberTwo;
                break;
            case Animal.Fox:
                theNumber = numberThree;
                break;
            case Animal.Rabbit:
                theNumber = numberFour;
                break;
            default:
                break;
        }
    }
    void Start()
    {
        brain = FindObjectOfType<FoodChainBrain>(); //Getting all the needed components
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        beginPhase = Randomize(0, 3);
        switch (beginPhase)
        {
            case 0:
                animator.SetBool("Tiger", true);
                break;
            case 1:
                animator.SetBool("Wolf", true);
                break;
            case 2:
                animator.SetBool("Fox", true);
                break;
            case 3:
                animator.SetBool("Rabbit", true);
                break;
            default:
                break;
        }

    }
    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            Interact();
        }
    }
    public int Randomize(int min, int max)
    {
        int number;
        return number = Random.Range(min, max+1);
    }//Basic Randomizing function
    public int GetNumber()
    {
        return theNumber;
    }//Function for the "brain" of the puzzle
    public void Interact()
    {
        if (InteractRaycasting.Instance.raycastInteract(out RaycastHit hit))
        {
            if (hit.transform.gameObject == gameObject)
            {
                print(size + " has been hit");
                brain.TurnDiscs(size);
            }
        }
    }
    public void Rotate()
    {
        animator.SetTrigger("Next");
        if (!audioSource.isPlaying)
        { 
            audioSource.Play();
        }
    }


}
