using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

// Adapted code from https://learn.unity.com/tutorial/starting-timeline-through-a-c-script-2019-3#5ff8d183edbc2a0020996602
public class TimelinePlayer : MonoBehaviour
{
    private PlayableDirector director;
    public GameObject controlPanel;
    public MainMenu mainMenu;

    private void Awake()
    {
        director = GetComponent<PlayableDirector>();
        director.played += Director_Played;
        director.stopped += Director_Stopped;
    }

    private void Director_Stopped(PlayableDirector obj)
    {
        GetComponent<AudioListener>().enabled = false;
        controlPanel.SetActive(true);
        mainMenu.ShowMenu(mainMenu.menuItems, mainMenu.optionsMenuItems, 0f, true);
    }

    private void Director_Played(PlayableDirector obj)
    {
        GetComponent<AudioListener>().enabled = true;
        controlPanel.SetActive(false);
    }

    public void StartTimeline()
    {
        director.Play();
    }

    public void StopAndResetTimeline()
    {
        director.Stop();
        director.time = 0;
    }
}
