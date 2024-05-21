using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] TMP_Dropdown resolutionDropdown;
    [SerializeField] Toggle fullscreenToggle;
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider sfxSlider;

    [SerializeField] AudioMixer masterMixer;

    List<Resolution> allResolutions;
    Resolution currentResolution;
    
    // Start is called before the first frame update
    void Start()
    {
        allResolutions = new List<Resolution>();

        allResolutions.Add(new Resolution(640, 480));
        allResolutions.Add(new Resolution(1024, 1024));
        allResolutions.Add(new Resolution(1280, 720));
        allResolutions.Add(new Resolution(1366, 768));
        allResolutions.Add(new Resolution(1920, 1080));

        FillResolutions();

        RefreshUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RefreshUI()
    {
        // set resolution dropdown based on current resolution
        currentResolution = new Resolution( Screen.width, Screen.height);

        var resIndex = allResolutions.IndexOf(currentResolution);
        if (resIndex != -1)
        {
            resolutionDropdown.value = resIndex;
        }

        // set fullscreen checkbox
        bool isFullscreen = Screen.fullScreen;
        fullscreenToggle.isOn = isFullscreen;

        // set BGM slider based on music volume
        float bgmVol = 0;
        masterMixer.GetFloat("musicVol", out bgmVol);
        bgmSlider.value = bgmVol / bgmSlider.maxValue;

        // set SFX slider based on music volume
        float sfxVol = 0;
        masterMixer.GetFloat("sfxVol", out sfxVol);
        sfxSlider.value = sfxVol / sfxSlider.maxValue;
    }

    public void FillResolutions()
    {
        List<string> resText = new List<string>();

        foreach(Resolution r in allResolutions)
        {
            resText.Add(r.ToString());
        }

        resolutionDropdown.AddOptions(resText);


    }

    public void UpdateWindow()
    {
        List<string> resText = new List<string>();

        foreach (Resolution r in allResolutions)
        {
            resText.Add(r.ToString());
        }

        int index = resolutionDropdown.value;

        currentResolution = allResolutions[index];

        Screen.SetResolution(currentResolution.x, currentResolution.y, fullscreenToggle.isOn);
    }

    public void UpdateSFXVolume(float val)
    {
        masterMixer.SetFloat("sfxVol", val);
    }

    public void UpdateBGMVolume(float val)
    {
        masterMixer.SetFloat("musicVol", val);
    }

    public void Cancel()
    {
        gameObject.SetActive(false);
    }
}

class Resolution
{
    public int x;
    public int y;

    public Resolution(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public override string ToString()
    {
        return x + " x " + y;
    }
}