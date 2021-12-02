using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class MainMenu : MonoBehaviour
{
    // Camera Settings
    public Camera mainCamera;
    private PostProcessProfile postProcessProfile;
    private DepthOfField dof;
    public GameObject cameraRotator;

    // Menu Items
    public CanvasGroup menuItems;

    // Start is called before the first frame update
    void Start()
    {
        // Adapted from code found at https://forum.unity.com/threads/changing-depthoffield-parameters-from-script.854026/
        postProcessProfile = mainCamera.GetComponent<PostProcessVolume>().profile;
        dof = postProcessProfile.GetSetting<DepthOfField>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Cinematic()
    {
        EnterLevel();
    }

    public void FreeCam()
    {
        cameraRotator.GetComponent<CameraRotator>().enabled = false;
        EnterLevel();
    }

    public void FirstPerson()
    {

    }

    private void EnterLevel()
    {
        StartCoroutine(FadeDOF(0f, 2f, 1f)); // Bring camera back into focus
        StartCoroutine(FadeCGAlpha(1f, 0f, 1f, menuItems)); // Fade out menu elements
    }

    // Adapted from code found at https://forum.unity.com/threads/unity-4-6-ui-how-to-fade-a-canvas-group-in-and-then-out-after-a-seconds.299283/
    private IEnumerator FadeCGAlpha(float from, float to, float duration, CanvasGroup canvasGroup)
    {
        float elaspedTime = 0f;
        while (elaspedTime <= duration)
        {
            elaspedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(from, to, elaspedTime / duration);
            yield return null;
        }
        canvasGroup.alpha = to;
    }

    private IEnumerator FadeDOF(float from, float to, float duration)
    {
        float elaspedTime = 0f;
        while (elaspedTime <= duration)
        {
            elaspedTime += Time.deltaTime;
            dof.aperture.value = Mathf.Lerp(from, to, elaspedTime / duration);
            yield return null;
        }
        dof.aperture.value = to;
    }
}
