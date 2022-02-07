using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WindowManager : MonoBehaviour {

    [SerializeField] TMP_Dropdown displayModeDropdown;

    void Awake() {
        if(PlayerPrefs.HasKey("DisplayMode")) {
            int mode = PlayerPrefs.GetInt("DisplayMode");
            switch(mode) {
                case 0:
                    Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                    displayModeDropdown.value = 0;
                    break;
                case 1:
                    Screen.fullScreenMode = FullScreenMode.Windowed;
                    displayModeDropdown.value = 1;
                    break;
                case 2:
                    Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                    displayModeDropdown.value = 2;
                    break;
                default:
                    break;
            }
        }
    }

    public void UpdateDisplayMode(int mode) {
        switch(mode) {
            case 0:
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                PlayerPrefs.SetInt("DisplayMode", 0);
                break;
            case 1:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                PlayerPrefs.SetInt("DisplayMode", 1);
                break;
            case 2:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                PlayerPrefs.SetInt("DisplayMode", 2);
                break;
            default:
                break;
        }
    }
}
