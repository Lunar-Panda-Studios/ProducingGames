using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class OptionsMenuButton : MonoBehaviour
{
    public Button yourButton;
    public GameObject MenuOne;
    public GameObject MenuTwo;
    public GameObject MenuToShow;
    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        MenuOne.SetActive(false);
        MenuTwo.SetActive(false);
        MenuToShow.SetActive(true);
    }
}