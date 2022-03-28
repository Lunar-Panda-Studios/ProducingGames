using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class OptionsMenuButton : MonoBehaviour
{
    public Button yourButton;
    public GameObject MenuOne;
    public GameObject MenuTwo;
    public GameObject MenuThree;
    public GameObject MenuToShow;
    [SerializeField] GameObject firstSelectedButton;
    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        EventSystem.current.SetSelectedGameObject(null);
        if (firstSelectedButton != null)
            EventSystem.current.SetSelectedGameObject(firstSelectedButton);
    }

    void TaskOnClick()
    {
        MenuOne.SetActive(false);
        MenuTwo.SetActive(false);
        MenuThree.SetActive(false);
        MenuToShow.SetActive(true);
        
    }
}