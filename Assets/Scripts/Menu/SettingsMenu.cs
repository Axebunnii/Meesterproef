using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : Menu {
    [SerializeField] private Dropdown resolutionDropdown;
    private Resolution[] resolutionOptions;
    private List<Resolution> filteredResolutions;
    private List<string> resolutionToString;

    private float currentRefreshRate;
    private int currentIndex = 0;

    private void Start() {
        SetResolutions();
    }

    public void BackToMainMenu() {
        mainmenu.SetActive(true);
        settingsmenu.SetActive(false);
    }

    private void SetResolutions() {
        resolutionOptions = Screen.resolutions;
        filteredResolutions = new List<Resolution>();

        resolutionDropdown.ClearOptions();
        currentRefreshRate = Screen.currentResolution.refreshRate;

        // Put all available resolutions in a list
        for (int i = resolutionOptions.Length - 1; i >= 0; i--) {
            if (resolutionOptions[i].refreshRate == currentRefreshRate)
                filteredResolutions.Add(resolutionOptions[i]);
        }

        resolutionToString = new List<string>();

        int index;
        string sub = "";

        // Display all the available resolutions
        for (int i = 0; i < filteredResolutions.Count; i++) {
            index = filteredResolutions[i].ToString().LastIndexOf("@");
            if (index >= 0)
                sub = filteredResolutions[i].ToString().Substring(0, index);

            resolutionToString.Add(sub);
        }

        // Add all the available options to the dropdown
        resolutionDropdown.AddOptions(resolutionToString);
    }

    public void SelectResolution() {
        int width;
        int height;

        string[] resolution = resolutionToString[resolutionDropdown.value].Split('x');

        width = int.Parse(resolution[0]);
        height = int.Parse(resolution[1]);

        Screen.SetResolution(width, height, true);
    }
}
