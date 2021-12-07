using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Adapted from code found at https://www.youtube.com/watch?v=c07rrroPKvw
public class ShowInfo : MonoBehaviour
{
    public float timer, refresh, avgFramerate;
    public string display = "{0} FPS";
    public TextMeshProUGUI fpsText, memoryText;

    // Update is called once per frame
    void Update()
    {
        // FPS Text
        float timelapse = Time.smoothDeltaTime;
        timer = timer <= 0 ? refresh : timer -= timelapse;

        if (timer <= 0) avgFramerate = (int)(1f / timelapse);
        fpsText.text = string.Format(display, avgFramerate.ToString());

        // Total Memory Text
        memoryText.text = GetComponent<MemoryProfiler>().GetTotalMemoryUsed();
    }
}
