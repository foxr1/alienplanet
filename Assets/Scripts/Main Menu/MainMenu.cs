using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class MainMenu : MonoBehaviour
{
    // Camera Settings
    public Camera mainCamera;
    public PostProcessProfile postProcessProfile;
    private DepthOfField dof;
    public GameObject cameraRotator;
    public GameObject cinemachineCamera;

    // Menu Items
    public CanvasGroup menuItems;
    public CanvasGroup optionsMenuItems;
    public Toggle dofToggle;

    private bool transitionRunning = false;

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

        StartCoroutine(HideMenu(optionsMenuItems, 0f, true));
        ShowMenu(menuItems, optionsMenuItems, 0f, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !transitionRunning && menuItems.alpha != 1)
        {
            if (player != null)
            {
                Destroy(player);
                mainCamera.gameObject.SetActive(true);
            }

            if (mainCamera.gameObject.GetComponent<InGameFreeCam>().isActiveAndEnabled)
            {
                cameraRotator.transform.position = cameraRotator.GetComponent<CameraRotator>().startPosition;
                mainCamera.transform.rotation = cameraRotator.GetComponent<CameraRotator>().startRotation;
                cameraRotator.GetComponent<CameraRotator>().enabled = true;
            }

            if (optionsMenuItems.alpha == 1)
            {
                StartCoroutine(HideMenu(optionsMenuItems, 0.5f, false));
                ShowMenu(menuItems, optionsMenuItems, 0.5f, false);
            } 
            else
            {
                ShowMenu(menuItems, optionsMenuItems, 1f, true);
            }
        }
    }

    public void Cinematic()
    {
        cinemachineCamera.GetComponent<TimelinePlayer>().StartTimeline();

        mainCamera.gameObject.GetComponent<InGameFreeCam>().enabled = false;
        StartCoroutine(HideMenu(menuItems, 1f, true));
    }

    public void FreeCam()
    {
        cinemachineCamera.GetComponent<TimelinePlayer>().StopAndResetTimeline();
        cameraRotator.GetComponent<CameraRotator>().enabled = false;
        mainCamera.gameObject.GetComponent<InGameFreeCam>().enabled = true;
        StartCoroutine(HideMenu(menuItems, 1f, true));
    }

    public void FirstPerson()
    {
        cinemachineCamera.GetComponent<TimelinePlayer>().StopAndResetTimeline();
        player = Instantiate(playerObj, new Vector3(1000, 50, 0), Quaternion.identity);
        mainCamera.gameObject.SetActive(false);
        mainCamera.gameObject.GetComponent<InGameFreeCam>().enabled = false;
        StartCoroutine(HideMenu(menuItems, 1f, true));
    }

    public void Options()
    {
        StartCoroutine(HideMenu(menuItems, 0.5f, false));
        menuItems.interactable = false;
        ShowMenu(optionsMenuItems, menuItems, 0.5f, false);
    }

    public void Back()
    {
        StartCoroutine(HideMenu(optionsMenuItems, 0.5f, false));
        ShowMenu(menuItems, optionsMenuItems, 0.5f, false);
    }

    private void ShowMenu(CanvasGroup itemsToShow, CanvasGroup itemsToHide, float duration, bool dofTransition)
    {
        itemsToHide.interactable = false;
        itemsToShow.interactable = true;
        itemsToShow.gameObject.SetActive(true);
        if (dofTransition && dofToggle.isOn)
        {
            StartCoroutine(FadeDOF(2f, 0f, duration)); // Take camera back out of focus
        }
        StartCoroutine(FadeCGAlpha(0f, 1f, duration, itemsToShow)); // Fade in menu elements
        Cursor.lockState = CursorLockMode.None;
    }

    private IEnumerator HideMenu(CanvasGroup items, float duration, bool dofTransition)
    {
        transitionRunning = true;

        items.interactable = false;
        if (dofTransition && dofToggle.isOn)
        {
            StartCoroutine(FadeDOF(0f, 2f, duration)); // Bring camera back into focus
        }
        StartCoroutine(FadeCGAlpha(1f, 0f, duration, items)); // Fade out menu elements
        
        // Wait for animation to end then disable items to stop misclicks
        yield return new WaitForSeconds(duration);
        items.gameObject.SetActive(false);

        transitionRunning = false;
    }

    // Adapted from code found at https://forum.unity.com/threads/unity-4-6-ui-how-to-fade-a-canvas-group-in-and-then-out-after-a-seconds.299283/
    public IEnumerator FadeCGAlpha(float from, float to, float duration, CanvasGroup canvasGroup)
    {
        transitionRunning = true;

        float elaspedTime = 0f;
        while (elaspedTime <= duration)
        {
            elaspedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(from, to, elaspedTime / duration);
            yield return null;
        }
        canvasGroup.alpha = to;

        transitionRunning = false;
    }

    public IEnumerator FadeDOF(float from, float to, float duration)
    {
        transitionRunning = true;

        float elaspedTime = 0f;
        while (elaspedTime <= duration)
        {
            elaspedTime += Time.deltaTime;
            dof.aperture.value = Mathf.Lerp(from, to, elaspedTime / duration);
            yield return null;
        }
        dof.aperture.value = to;

        transitionRunning = false;
    }
}
