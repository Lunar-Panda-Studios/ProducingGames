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
        
        if(timer >= 1.5)
        {
            First.GetComponent<Image>().color = Color.white;
            //timer = 0f;
        }
        if(timer <= 1.5 && timer > 0.5)
        {
            First.GetComponent<Image>().color = Color.red;
        }


        if(timer >= 3)
        {
            Second.GetComponent<Image>().color = Color.white;
        }

        if (timer <= 3 && timer > 2)
        {
            Second.GetComponent<Image>().color = Color.red;
        }


        if (timer >= 4.5)
        {
            Third.GetComponent<Image>().color = Color.white;
        }
        if (timer <= 4.5 && timer > 3.5)
        {
            Third.GetComponent<Image>().color = Color.red;
        }


        if (timer >= 6)
        {
            Fourth.GetComponent<Image>().color = Color.white;
        }

        if (timer <= 6 && timer > 5)
        {
            Fourth.GetComponent<Image>().color = Color.red;
        }

    }
    void OnMouseDown()
    {
        reset = true;
        puzzle.SetActive(true);
        //Cursor.lockState = CursorLockMode.None;
        
    }
}

