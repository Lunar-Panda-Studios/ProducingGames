using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class SubmitFeedback : MonoBehaviour
{
    public Button yourButton;
    public GameObject FeedbackMenu;
    public GameObject BarOfStamina;


    public FeedbackToggle EE;


    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);

        EE = FindObjectOfType<FeedbackToggle>();
    }

    void TaskOnClick()
    {
        if (EE.IsOnFeedbackMenu == true)
        {
            EE.IsOnFeedbackMenu = false;
            FeedbackMenu.SetActive(false);
            BarOfStamina.SetActive(true);
            if (Time.timeScale <= 0f)
            {
                Time.timeScale = 1f;
            }
        }
    }
}