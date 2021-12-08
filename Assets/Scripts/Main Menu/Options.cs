using DigitalRuby.RainMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AzureSky;
using UnityEngine.Rendering.PostProcessing;

public class Options : MonoBehaviour
{
    public CanvasGroup debugInfo;
    public MainMenu mainMenu;
    public Camera mainCamera;
    public GameObject rainPrefab;
    public GameObject rainInGame;

    private void Start()
    {
        rainInGame = Instantiate(rainPrefab, new Vector3(1000, 10, 0), Quaternion.identity);
        rainInGame.GetComponent<RainScript>().Camera = mainCamera;
    }

    public void FullscreenToggle(bool toggle)
    {
        Screen.fullScreen = toggle;
    }

    public void VSyncToggle(bool toggle)
    {
        if (toggle)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }
    }

    public void DebugInfoToggle(bool toggle)
    {
        _ = (toggle) ? StartCoroutine(mainMenu.FadeCGAlpha(0f, 1f, 1f, debugInfo)) : StartCoroutine(mainMenu.FadeCGAlpha(1f, 0f, 1f, debugInfo));
    }

    public void RainToggle(bool toggle)
    {
        if (!toggle && rainInGame != null)
        {
            Destroy(rainInGame);
        } 
        else
        {
            rainInGame = Instantiate(rainPrefab, new Vector3(1000, 10, 0), Quaternion.identity);
            rainInGame.GetComponent<RainScript>().Camera = mainCamera;
        }
    }

    public void FogToggle(bool toggle)
    {
        mainCamera.GetComponent<AzureFogScattering>().enabled = toggle;
    }

    public void DepthOfFieldToggle(bool toggle)
    {
        _ = (toggle) ? StartCoroutine(mainMenu.FadeDOF(2f, 0f, 1f)) : StartCoroutine(mainMenu.FadeDOF(0f, 2f, 1f));
    }
}
