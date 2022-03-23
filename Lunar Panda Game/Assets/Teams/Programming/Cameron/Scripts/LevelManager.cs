using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] CanvasGroup loadingScreen;
    [SerializeField] float fadeTime;
    float currentFadeTime;
    private static LevelManager _instance;
    public static LevelManager Instance { get { return _instance; } }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        //StartCoroutine(LoadConnectorScene());
        //DontDestroyOnLoad(gameObject);
        
    }

    /*IEnumerator LoadConnectorScene()
    {
        AsyncOperation load = SceneManager.LoadSceneAsync("Connect", LoadSceneMode.Additive);
        while (!load.isDone)
        {
            yield return null;
        }
    }*/

    IEnumerator FadeIn()
    {
        for (float t = 0f; t < fadeTime; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeTime;
            loadingScreen.alpha = Mathf.Lerp(0, 1, normalizedTime);
            yield return null;
        }
        loadingScreen.alpha = 1; //without this, the value will end at something like 0.9992367
    }

    IEnumerator FadeOut() //i dont care this this doesnt need to exist. Shut up
    {
        for (float t = 0f; t < fadeTime; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeTime;
            loadingScreen.alpha = Mathf.Lerp(1, 0, normalizedTime);
            yield return null;
        }
        loadingScreen.alpha = 0;
    }

    public IEnumerator FadeLoadingScreen(string sceneName)
    {
        loadingScreen.gameObject.SetActive(true);
        
        StartCoroutine(FadeIn());
        yield return new WaitForSeconds(2f);
        AsyncOperation load = SceneManager.LoadSceneAsync(sceneName);
        while (!load.isDone)
        {
            yield return null;
        }
        currentFadeTime = fadeTime;
        StartCoroutine(FadeOut());
    }
}
