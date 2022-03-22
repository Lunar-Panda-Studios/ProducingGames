using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadTrigger : MonoBehaviour
{
    [SerializeField] GameObject openedDoor;
    [SerializeField] GameObject closedDoor;
    bool loadingScene = false;

    private void Start()
    {
        DontDestroyOnLoad(transform.parent.gameObject);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            openedDoor.SetActive(true);
            if (!loadingScene)
            {
                StartCoroutine(LoadScene());
            }
        }
    }

    IEnumerator LoadScene()
    {
        AsyncOperation sceneUnload = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        while (!sceneUnload.isDone)
        {
            yield return null;
        }

        AsyncOperation sceneLoad = SceneManager.LoadSceneAsync("CamsHotel", LoadSceneMode.Additive);
        while (!sceneLoad.isDone)
        {
            yield return null;
        }
        closedDoor.SetActive(false);
    }
}
