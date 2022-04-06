using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    VideoPlayer videoPlayer;
    LevelManager levelManager;
    [SerializeField] string nextScene;

    float time;
    public float maxTime = 5f;
    bool hasLoaded = false;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        levelManager = FindObjectOfType<LevelManager>();
        videoPlayer.loopPointReached += LoadScene;
    }

    void Update()
    {
        Skip();
    }

    void Skip()
    {
        if (!hasLoaded)
        {
            if (Input.GetButton("Skip"))
            {                
                time += Time.deltaTime;
                if (time > maxTime)
                {
                    hasLoaded = true;
                    LoadScene(videoPlayer);
                    Debug.Log("Skipped");
                }
            }
            else if (Input.GetButtonUp("Skip"))
            {
                time = 0f;                
            }
        }
    }

    void LoadScene(VideoPlayer vp)
    {
        StartCoroutine(levelManager.FadeLoadingScreen(nextScene));
    }
}
