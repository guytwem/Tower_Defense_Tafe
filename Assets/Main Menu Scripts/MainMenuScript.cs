using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    #region Variables

    [SerializeField, Tooltip("fullscreen toggle")]
    private Toggle fullscreenToggle;
    [SerializeField, Tooltip("Game Quailty Dropdown")]
    private Dropdown qualityDropdown;
    [SerializeField, Tooltip("")]
    private Dropdown resolution;
    private Resolution[] resolutions;

    #endregion

    private void Start()
    {
        GetResolutions();

        LoadPlayerPrefs();

        if (!PlayerPrefs.HasKey("fullscreen"))
        {
            PlayerPrefs.SetInt("fullscreen", 0);
            Screen.fullScreen = false;
        }
        else
        {
            if (PlayerPrefs.GetInt("fullscreen") == 0)
            {
                Screen.fullScreen = false;
            }
            else
            {
                Screen.fullScreen = true;
            }
        }

        if (!PlayerPrefs.HasKey("quality"))
        {
            PlayerPrefs.SetInt("quality", 6);// the highest quality setting
            QualitySettings.SetQualityLevel(6);
        }
        else
        {
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("quality"));
        }
        PlayerPrefs.Save();
    }

    #region QuitMethod
    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
        Application.Quit();
    }
    #endregion

    #region Change Settings

    public void ChangeQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution res = resolutions[resolutionIndex];
        Screen.SetResolution(res.width, res.height, false);
    }

    #endregion

    #region Save and Load PlayerPrefs
    public void LoadPlayerPrefs()
    {
        //load quality
        if (PlayerPrefs.HasKey("quality"))
        {
            int quality = PlayerPrefs.GetInt("quality");
            qualityDropdown.value = quality;
            if (QualitySettings.GetQualityLevel() != quality)
            {
                ChangeQuality(quality);
            }
        }

        //load fullscreen
        if (PlayerPrefs.GetInt("fullscreen") == 0)
        {
            //set GUI toggle off
            fullscreenToggle.isOn = false;
        }
        else
        {
            //set GUI toggle on
            fullscreenToggle.isOn = true;
        }
    }

    public void SavePlayerPrefs()
    {
        //save quality
        PlayerPrefs.SetInt("quality", QualitySettings.GetQualityLevel());

        // save full screen
        if (fullscreenToggle.isOn)
        {
            PlayerPrefs.SetInt("fullscreen", 1);
        }
        else
        {
            PlayerPrefs.SetInt("fullscreen", 0);
        }
    }
    #endregion

    /// <summary>
    /// find all of the resolutions for the current screen
    /// </summary>
    private void GetResolutions()
    {
        resolutions = Screen.resolutions;
        resolution.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)// go through every resolution
        {
            //build a string for displaying the resolutions
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && 
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        // set up the dropdown
        resolution.AddOptions(options);
        resolution.value = currentResolutionIndex;
        resolution.RefreshShownValue();
    }


}
