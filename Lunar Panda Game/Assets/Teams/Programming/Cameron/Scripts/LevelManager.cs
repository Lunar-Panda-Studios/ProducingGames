using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] CanvasGroup loadingScreen;
    [SerializeField] float fadeTime;
/*    private static LevelManager _instance;
    public static LevelManager Instance { get { return _instance; } }*/

    void Awake()
    {
        /*        if (_instance != null && _instance != this)
                {
                    Destroy(this.gameObject);
                }
                else
                {
                    _instance = this;
                }*/
        //StartCoroutine(LoadConnectorScene());
        transform.parent = null;
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(this);
        
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
        loadingScreen.alpha = 1; //without this, the value will end at something like 0.9994512
    }

    IEnumerator FadeOut() //i dont care this this doesnt need to exist and the fade in could just be optimised. Shut up
    {
        yield return new WaitForSeconds(1);
        for (float t = 0f; t < fadeTime; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeTime;
            loadingScreen.alpha = Mathf.Lerp(1, 0, normalizedTime);
            yield return null;
        }
        loadingScreen.alpha = 0;
        Destroy(gameObject);
    }

    public IEnumerator FadeLoadingScreen(string sceneName)
    {
        loadingScreen.gameObject.SetActive(true);
        StartCoroutine(FadeIn());
        yield return new WaitForSeconds(2f);
        if (Analysis.current != null)
        {
            if (Analysis.current.consent)
            {
                Analysis.current.saveAnalytics();
            }
        }
        //if the current level is not the cabin, increase the level, if it is the cabin, we're going back to the main menu so set it back to 0
        //not a very elegant solution, but it works for now i guess
        if(GameManager.Instance.currentRoom != Room.CABIN)
            GameManager.Instance.currentLevel(GameManager.Instance.whichLevel + 1);
        else
            GameManager.Instance.currentLevel(0);

        AsyncOperation load = SceneManager.LoadSceneAsync(sceneName);
        while (load.progress < 0.9f)
        {
            yield return null;
        }
        StartCoroutine(FadeOut());
    }
}
