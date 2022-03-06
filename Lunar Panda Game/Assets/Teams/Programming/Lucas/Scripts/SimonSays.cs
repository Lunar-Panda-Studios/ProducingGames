using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SimonSays : MonoBehaviour
{
    lockMouse lockMouse;
    public GameObject puzzle;

    public GameObject First;
    public GameObject Second;
    public GameObject Third;
    public GameObject Fourth;

    private float timer = 0f;
    private bool reset = false;
    private void Start()
    {
        
    }

    private void Update()
    {
        if (reset == true)
        {
            timer += Time.deltaTime;
        }
        
        //Flashes first sequence button colour after a timer
        if(timer >= 1.5)
        {
            First.GetComponent<Image>().color = Color.white;
        }
        if(timer <= 1.5 && timer > 0.5)
        {
            First.GetComponent<Image>().color = Color.red;
        }

        //Flashes second sequence button colour after a timer
        if(timer >= 3)
        {
            Second.GetComponent<Image>().color = Color.white;
        }

        if (timer <= 3 && timer > 2)
        {
            Second.GetComponent<Image>().color = Color.red;
        }

        //Flashes third sequence button colour after a timer
        if (timer >= 4.5)
        {
            Third.GetComponent<Image>().color = Color.white;
        }
        if (timer <= 4.5 && timer > 3.5)
        {
            Third.GetComponent<Image>().color = Color.red;
        }

        //Flashes fourth sequence button colour after a timer
        if (timer >= 6)
        {
            Fourth.GetComponent<Image>().color = Color.white;
        }

        if (timer <= 6 && timer > 5)
        {
            Fourth.GetComponent<Image>().color = Color.red;
        }


        //timer = 0f; resets puzzle, can do this when player gets puzzle wrong or a restart button 

    }
    void OnMouseDown()
    {

        //Starts puzzle and timer when clicked on the puzzle object 
        reset = true;
        puzzle.SetActive(true);

        //Cursor.lockState = CursorLockMode.None;
        
    }
    
}

