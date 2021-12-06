using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    // Player
    public GameObject playerObj;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        // Adapted from code found at https://forum.unity.com/threads/changing-depthoffield-parameters-from-script.854026/
        postProcessProfile = mainCamera.GetComponent<PostProcessVolume>().profile;
        dof = postProcessProfile.GetSetting<DepthOfField>();
        mainCamera.gameObject.GetComponent<InGameFreeCam>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (player != null)
            {
                Destroy(player);
                mainCamera.gameObject.SetActive(true);
            }
            ShowMenu();
        }
    }

    public void Cinematic()
    {
        cameraRotator.GetComponent<CameraRotator>().enabled = true;
        mainCamera.gameObject.GetComponent<InGameFreeCam>().enabled = false;
        HideMenu();
    }

    public void FreeCam()
    {
        cameraRotator.GetComponent<CameraRotator>().enabled = false;
        mainCamera.gameObject.GetComponent<InGameFreeCam>().enabled = true;
        HideMenu();
    }

    public void FirstPerson()
    {
        player = Instantiate(playerObj, new Vector3(1000, 50, 0), Quaternion.identity);
        mainCamera.gameObject.SetActive(false);
        HideMenu();
    }

    private void ShowMenu()
    {
        StartCoroutine(FadeDOF(2f, 0f, 1f)); // Take camera back out of focus
        StartCoroutine(FadeCGAlpha(0f, 1f, 1f, menuItems)); // Fade in menu elements
        Cursor.lockState = CursorLockMode.None;
        GetComponent<GraphicRaycaster>().enabled = true; // Enable interaction with main menu when not shown on screen
    }

    private void HideMenu()
    {
        StartCoroutine(FadeDOF(0f, 2f, 1f)); // Bring camera back into focus
        StartCoroutine(FadeCGAlpha(1f, 0f, 1f, menuItems)); // Fade out menu elements
        GetComponent<GraphicRaycaster>().enabled = false; // Disable interaction with main menu when not shown on screen
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
