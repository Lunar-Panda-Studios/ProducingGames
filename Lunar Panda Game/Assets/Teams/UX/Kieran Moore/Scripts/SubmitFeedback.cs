using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class SubmitFeedback : MonoBehaviour
{
    public Button yourButton;
    public GameObject FeedbackMenu;
    string keyName;
    int feedbackNumber = 0;
    public Text textBody;

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
            keyName = "FeedbackV" + feedbackNumber.ToString();
            Analysis.current.parameters.Add(keyName, textBody.text);
            feedbackNumber++;

            EE.IsOnFeedbackMenu = false;
            FeedbackMenu.SetActive(false);
            if (Time.timeScale <= 0f)
            {
                Time.timeScale = 1f;
            }
        }
    }
}
