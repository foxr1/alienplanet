using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Some code adapted from https://www.youtube.com/watch?v=yeaELkoxD9w&t=215s
public class ChangeResolution : MonoBehaviour
{
    TMP_Dropdown dropdown;
    public string resolutionText;
    int dropdownValue;
    public Toggle fullscreen;
    public Toggle vsync;

    void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();

        fullscreen.isOn = Screen.fullScreen;

        _ = (QualitySettings.vSyncCount == 1) ? vsync.isOn == true : vsync.isOn == false;

        // Create a list of resolutions based off the user's native resolution
        List<string> newRes = new List<string> { $"{Screen.width}x{Screen.height}" };
        dropdown.AddOptions(newRes);
        List<string> halfRes = new List<string> { $"{Screen.width / 2}x{Screen.height / 2}" };
        dropdown.AddOptions(halfRes);
        List<string> thirdRes = new List<string> { $"{Screen.width / 3}x{Screen.height / 3}" };
        dropdown.AddOptions(thirdRes);
        List<string> quarterRes = new List<string> { $"{Screen.width / 4}x{Screen.height / 4}" };
        dropdown.AddOptions(quarterRes);

        dropdown.SetValueWithoutNotify(0);
        Screen.SetResolution(Screen.width, Screen.height, fullscreen.isOn);
    }

    // Update is called once per frame
    void Update()
    {
        //Keep the current index of the Dropdown in a variable
        dropdownValue = dropdown.value;
        resolutionText = dropdown.options[dropdownValue].text;

        int width = int.Parse(resolutionText.Split('x')[0]), height = int.Parse(resolutionText.Split('x')[1]);
        Screen.SetResolution(width, height, fullscreen.isOn);
    }
}
