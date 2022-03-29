using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class MiscMenuButton : MonoBehaviour
{
    public Button yourButton;
    public GameObject CurrentMenu;
    public GameObject MenuToShow;
    [SerializeField] GameObject firstButtonSelected;

    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        CurrentMenu.SetActive(false);
        MenuToShow.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        if (firstButtonSelected != null)
            EventSystem.current.SetSelectedGameObject(firstButtonSelected);
    }
}