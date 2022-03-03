using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class Resume : MonoBehaviour
{
    public Button yourButton;
    public GameObject PauseMenu;


    public PauseButtonToggle EE;


    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);

        EE = FindObjectOfType<PauseButtonToggle>();
    }

    void TaskOnClick()
    {
        if (EE.IsPaused == true && EE.IsOnRegularMenu == true)
        {
            EE.IsPaused = false;
            EE.IsOnRegularMenu = false;
            PauseMenu.SetActive(false);
            if (Time.timeScale <= 0f)
            {
                Time.timeScale = 1f;
            }
        }
    }
}