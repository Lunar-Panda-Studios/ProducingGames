using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class LevelManager : MonoBehaviour
{
    void Awake()
    {
        LoadConnectorScene();
        DontDestroyOnLoad(gameObject);
        
    }

    async void LoadConnectorScene()
    {
        AsyncOperation load = SceneManager.LoadSceneAsync("Connect", LoadSceneMode.Additive);
        while (!load.isDone)
        {
            await Task.Yield();
        }
    }
}