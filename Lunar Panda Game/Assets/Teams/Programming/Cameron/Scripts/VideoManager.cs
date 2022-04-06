using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    VideoPlayer videoPlayer;
    LevelManager levelManager;
    [SerializeField] string nextScene;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        levelManager = FindObjectOfType<LevelManager>();
        videoPlayer.loopPointReached += LoadScene;
    }

    void LoadScene(VideoPlayer vp)
    {
        StartCoroutine(levelManager.FadeLoadingScreen(nextScene));
    }
}
