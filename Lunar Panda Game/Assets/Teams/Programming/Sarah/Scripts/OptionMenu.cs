using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using TMPro;

public class OptionMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    Resolution[] resolutions;
    public Dropdown resolutionDropdown;
    public Volume postProcessV;
    public VSync vSync;
    float timer = 0;
    int timerMax = 5;
    bool changeResolution = false;
    int currentResolutionIndex;
    int oldResolutionIndex;
    public TextMeshProUGUI resolutionText;
    public GameObject resolutionWindow;
    public Dropdown fullscreenDropdown;
    bool initialing = true;


    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        currentResolutionIndex = 0;
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].height == Screen.currentResolution.height && resolutions[i].width == Screen.currentResolution.width)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        if(Screen.fullScreen)
        {
            fullscreenDropdown.value = 1;
        }
        else
        {
            fullscreenDropdown.value = 0;
        }

        fullscreenDropdown.RefreshShownValue();
        initialing = false;
    }

    private void Update()
    {
        if(changeResolution)
        {
            timer += Time.deltaTime;
            resolutionText.text = Mathf.Ceil(timerMax - timer).ToString();
             if(timer >= timerMax)
            {
                resolution(oldResolutionIndex);
                changeResolution = false;
                timer = 0;
                resolutionWindow.SetActive(false);
            }
        }
    }

    public void masterVolume(float volume)
    {
        audioMixer.SetFloat("MastVol", volume);
    }

    public void SFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVol", volume);
    }

    public void dialogVolume(float volume)
    {
        audioMixer.SetFloat("Dialog", volume);
    }

    public void musicVolume(float volume)
    {
        audioMixer.SetFloat("Music", volume);
    }

    public void enableSubtitles(bool enable)
    {
        GameManager.Instance.subtitles = enable;
    }

    public void resolution(int index)
    {
        if (!resolutionWindow.activeInHierarchy)
        {
            print("Change Resolution " + index);
            Resolution resolution = resolutions[index];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

            if (!changeResolution)
            {
                oldResolutionIndex = currentResolutionIndex;
                currentResolutionIndex = index;
                if (!initialing)
                {
                    changeResolution = true;
                    resolutionWindow.SetActive(true);
                }
            }

            resolutionDropdown.value = index;
            resolutionDropdown.RefreshShownValue();
        }

    }

    public void resolutionConfirm(bool confirm)
    {
        timer = 0;
        resolutionWindow.SetActive(false);

        if (!confirm)
        {
            resolution(oldResolutionIndex);
            currentResolutionIndex = oldResolutionIndex;
        }
        changeResolution = false;

    }

    public void fullscreen(int isFullscreen)
    {
        if (isFullscreen == 0)
        {
            Screen.fullScreen = false;
        }
        else
        {
            Screen.fullScreen = true;
        }
    }

    public void qualitySettings(int index)
    {
        print("Change Quality " + index);
        QualitySettings.SetQualityLevel(index);
    }    

    public void motionBlur(bool enable)
    {
        VolumeProfile profile = postProcessV.sharedProfile;

        profile.TryGet(out MotionBlur motionBlur);
        motionBlur.active = enable;
    }

    public void brightness(float value)
    {
        VolumeProfile profile = postProcessV.sharedProfile;
        
        profile.TryGet(out Exposure exposure);
        exposure.compensation.value = value;
    }
}
