using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class MiscMenuButton : MonoBehaviour
{
    public Button yourButton;
    public GameObject CurrentMenu;
    public GameObject MenuToShow;
    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        CurrentMenu.SetActive(false);
        MenuToShow.SetActive(true);
    }
}