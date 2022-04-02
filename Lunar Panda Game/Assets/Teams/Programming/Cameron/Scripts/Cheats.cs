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
    [SerializeField] string trainSceneName;
    [SerializeField] string hospitalSceneName;
    [SerializeField] string hotelSceneName;
    [SerializeField] string cabinSceneName;

    [SerializeField] KeyCode trainKey = KeyCode.B;
    [SerializeField] KeyCode hospitalKey = KeyCode.N;
    [SerializeField] KeyCode hotelKey = KeyCode.M;
    [SerializeField] KeyCode cabinKey = KeyCode.Comma;
    



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
            GameManager.Instance.currentLevel(2);
            SceneManager.LoadScene(trainSceneName);
        }
        else if (Input.GetKeyDown(hospitalKey))
        {
            Time.timeScale = 1f;
            GameManager.Instance.currentLevel(3);
            SceneManager.LoadScene(hospitalSceneName);
        }
        else if (Input.GetKeyDown(hotelKey))
        {
            Time.timeScale = 1f;
            GameManager.Instance.currentLevel(4);
            SceneManager.LoadScene(hotelSceneName);
        }
        else if (Input.GetKeyDown(cabinKey))
        {
            Time.timeScale = 1f;
            GameManager.Instance.currentLevel(5);
            SceneManager.LoadScene(cabinSceneName);
        }
    }
}
