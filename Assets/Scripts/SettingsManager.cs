using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

struct Settings
{
    public float volume;
    public float fov;
    public bool fullscreen;
    public QualitySettings quality;
}

public class SettingsManager : MonoBehaviour {

    Settings settings;

	void Start () {
		
	}
	
	void Update () {
		
	}

    public void LoadSettings()
    {

    }

    public void SaveSettings()
    {

    }

    void SetFullScreen(bool full)
    {
        settings.fullscreen = full;
        Screen.fullScreen = settings.fullscreen;
    }

    void SetScreenResolution(int i)
    {
        Screen.SetResolution(Screen.resolutions[i].width, Screen.resolutions[i].height, settings.fullscreen);
    }

    void SetQuality()
    {

    }

    void setFOV(int fov)
    {

    }
}
