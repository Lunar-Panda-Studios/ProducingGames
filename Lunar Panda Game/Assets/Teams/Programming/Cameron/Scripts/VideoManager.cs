using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    VideoPlayer videoPlayer;
    LevelManager levelManager;
    GameManager gameManager;
    [SerializeField] string nextScene;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        levelManager = FindObjectOfType<LevelManager>();
        gameManager = FindObjectOfType<GameManager>();
        videoPlayer.loopPointReached += LoadScene;
    }

    void LoadScene(VideoPlayer vp)
    {
        StartCoroutine(levelManager.FadeLoadingScreen(nextScene));
    }
}
