using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField, Tooltip("fullscreen toggle")]
    private Toggle fullscreenToggle;


    #region QuitMethod
    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
        Application.Quit();
    }
    #endregion


    #region Save and Load PlayerPrefs
    public void LoadPlayerPrefs()
    {
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

}
