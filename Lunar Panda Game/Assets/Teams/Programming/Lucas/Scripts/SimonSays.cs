using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SimonSays : MonoBehaviour
{
    private GameObject playerCam;
    private lockMouse lockMouseScript;

    public GameObject puzzle;

    public List<GameObject> sequenceOrder;
    public int playbackFlashNumber;
    public float playbackFlashLength;
    private float _playbackFlashLength;
    private bool playback = true;
    private bool playableState = false;

    private int correctValues = 0;

    public float flashLength;
    private float _flashLength = 10000;

    public GameObject First;
    public GameObject Second;
    public GameObject Third;
    public GameObject Fourth;

    private float timer = 0f;
    private bool reset = false;
    private void Start()
    {
        playerCam = GameObject.FindGameObjectWithTag("MainCamera");
        lockMouseScript = playerCam.GetComponent<lockMouse>();
    }

    private void Update()
    {
        if (reset)
        {
            _playbackFlashLength += Time.deltaTime;
        }
        if (_playbackFlashLength>=playbackFlashLength)
        {
            if (playback)
            {
                _playbackFlashLength = 0;
                sequenceOrder[playbackFlashNumber].GetComponent<Image>().color = Color.blue;
                playbackFlashNumber++;
                _flashLength = 0;
                if (playbackFlashNumber>=sequenceOrder.Count)
                {
                    lockMouseScript.enabled = false;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    playback = false;
                    playableState = true;
                    Debug.Log("Working");

                }
            }
        }

        if (_flashLength<flashLength)
        {
            _flashLength += Time.deltaTime;
            if (_flashLength>=flashLength)
            {
                sequenceOrder[playbackFlashNumber-1].GetComponent<Image>().color = Color.white;
            }
        }





        //timer = 0f; resets puzzle, can do this when player gets puzzle wrong or a restart button 

    }

    public bool getPlayableState()
    {
        return playableState;
    }

    public void resetPuzzle()
    {
        correctValues = 0;
        playbackFlashNumber = 0;
    }

    public int changeCorrectAnswers(int newValue)
    {
        correctValues = newValue;
        return newValue;
    }

    public int getCurrentCorrectAnswers()
    {
        return correctValues;
    }

    void OnMouseDown()
    {

        //Starts puzzle and timer when clicked on the puzzle object 
        reset = true;
        puzzle.SetActive(true);

        //Cursor.lockState = CursorLockMode.None;
        
    }
    
}

