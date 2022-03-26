using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class NewGameClick : MonoBehaviour
{
    public Button yourButton;
    public string SceneToLoad;
    public GameObject LoadScreenOBJ;
    public float DelayForLoading;
    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }


    IEnumerator LoadSceneAsync()
    {
        yield return new WaitForSeconds(DelayForLoading);
        GameManager.Instance.currentLevel(1);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneToLoad);

        while (!asyncLoad.isDone)
        {
            print("Loading");
            yield return null;
        }
    }


    void TaskOnClick()
    {
        LoadScreenOBJ.SetActive(true);
        StartCoroutine("LoadSceneAsync");
    }
}