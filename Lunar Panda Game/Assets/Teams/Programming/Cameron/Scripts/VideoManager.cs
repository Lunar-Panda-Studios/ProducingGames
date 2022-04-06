using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoManager : MonoBehaviour
{
    VideoPlayer videoPlayer;
    LevelManager levelManager;
    GameManager gameManager;
    [SerializeField] string nextScene;

    float time;
    public float maxTime = 5f;
    bool hasLoaded = false;
    [SerializeField] Slider skipSlider;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        levelManager = FindObjectOfType<LevelManager>();
        gameManager = FindObjectOfType<GameManager>();
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
                skipSlider.value = time / maxTime;
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
                skipSlider.value = 0;
            }
        }
    }

    void LoadScene(VideoPlayer vp)
    {
        StartCoroutine(levelManager.FadeLoadingScreen(nextScene));
    }
}
