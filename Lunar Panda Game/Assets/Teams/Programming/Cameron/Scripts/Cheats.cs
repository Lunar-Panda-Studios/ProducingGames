using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cheats : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] bool cheatsEnabled = false;
    [SerializeField] KeyCode cheatKey = KeyCode.Tilde;

    [Header("Scenes")]
    [Tooltip("Drag and drop the hospital scene here")]
    [SerializeField] Object hospitalScene;
    [Tooltip("Drag and drop the hotel scene here")]
    [SerializeField] Object hotelScene;
    [Tooltip("Drag and drop the train scene here")]
    [SerializeField] Object trainScene;

    [SerializeField] KeyCode hospitalKey = KeyCode.N;
    [SerializeField] KeyCode hotelKey = KeyCode.M;
    [SerializeField] KeyCode trainKey = KeyCode.B;



    //private void Awake()
    //{
    //    DontDestroyOnLoad(gameObject);
    //}

    void Update()
    {
        if (cheatsEnabled)
        {
            //put all cheat stuff here
            LoadSceneOnKeyPress();
        }
        if (Input.GetKeyDown(cheatKey))
        {
            cheatsEnabled = !cheatsEnabled;
        }
    }

    void LoadSceneOnKeyPress()
    {
        if (Input.GetKeyDown(trainKey))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Train v1.7.1");
        }
        else if (Input.GetKeyDown(hospitalKey))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Hospital v1.7.1");
        }
        else if (Input.GetKeyDown(hotelKey))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Hotel v1.7.1");
        }
    }
}
