using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeQuality : MonoBehaviour
{
    TMP_Dropdown dropdown;
    int dropdownValue;

    void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        dropdown.SetValueWithoutNotify(0);
        QualitySettings.SetQualityLevel(dropdown.options.Count);
    }

    // Update is called once per frame
    void Update()
    {
        //Keep the current index of the Dropdown in a variable
        dropdownValue = dropdown.value;
        QualitySettings.SetQualityLevel(dropdown.options.Count - dropdownValue);
    }
}
