using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Image fadePanel;
    [SerializeField] float fadeTime;
    float currentFadeTime;

    void Awake()
    {
        StartCoroutine(LoadConnectorScene());
        DontDestroyOnLoad(gameObject);
        
    }

    IEnumerator LoadConnectorScene()
    {
        AsyncOperation load = SceneManager.LoadSceneAsync("Connect", LoadSceneMode.Additive);
        while (!load.isDone)
        {
            yield return null;
        }
    }

    IEnumerator FadeLoadingScreen()
    {
        fadePanel.color = new Color(0, 0, 0, currentFadeTime);
        while (currentFadeTime < fadeTime)
        {
            fadeTime += Time.deltaTime;
            yield return null;
        }
        //load shit here

        //if shit loaded
        //fade loading screen out
        //idk if that should all be in this single function, but i dont care
    }
}
