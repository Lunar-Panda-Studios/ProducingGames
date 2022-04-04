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
    float timer = 0;
    int timerMax = 5;
    bool changeResolution = false;
    int currentResolutionIndex;
    int oldResolutionIndex;

    [Header("Audio Settings")]
    public Slider masterVolSlider;
    public Slider SFXVolSlider;
    public Slider dialogueVolSlider;
    public Slider musicVolSlider;
    public Toggle subtitleToggle;

    [Header("Video Settings")]
    public TextMeshProUGUI resolutionText;
    public GameObject resolutionWindow;
    public Dropdown fullscreenDropdown;
    public Dropdown qualitySettingsDropdown;
    public Dropdown FPSDropdown;
    [SerializeField] Text fpsText;
    public Toggle motionBlurToggle;
    public Slider brightnessSlider;
    [SerializeField] float brightnessMultiplier = 1f;
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

        qualitySettingsDropdown.value = QualitySettings.GetQualityLevel();
        qualitySettingsDropdown.RefreshShownValue();

        audioMixer.GetFloat("MastVol", out float volume);
        masterVolSlider.value = volume;

        audioMixer.GetFloat("SFXVol", out volume);
        SFXVolSlider.value = volume;

        audioMixer.GetFloat("DialogueVol", out volume);
        dialogueVolSlider.value = volume;

        audioMixer.GetFloat("MusVol", out volume);
        musicVolSlider.value = volume;

        VolumeProfile profile = postProcessV.sharedProfile;

        if(profile.TryGet(out MotionBlur motionBlur))
        {
            motionBlurToggle.isOn = motionBlur.active;
        }

        profile.TryGet(out Exposure exposure);
        brightnessSlider.value = exposure.compensation.value;

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
        audioMixer.SetFloat("DialogueVol", volume);
    }

    public void musicVolume(float volume)
    {
        audioMixer.SetFloat("MusVol", volume);
    }

    public void enableSubtitles(bool enable)
    {
        GameManager.Instance.subtitles = enable;
    }

    public void resolution(int index)
    {
        if (!resolutionWindow.activeInHierarchy)
        {
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
        exposure.compensation.value = value * brightnessMultiplier;
    }

    public void ToggleFPSText()
    {
        fpsText.gameObject.SetActive(!fpsText.gameObject.activeInHierarchy);
    }
}
