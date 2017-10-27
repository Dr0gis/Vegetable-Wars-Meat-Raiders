using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnOffPauseScript : MonoBehaviour {

    private bool isPaused;

    public float TimeSpeed;

    public GameObject PauseButton;
    public GameObject ResumeButton;

    public GameObject PauseCanvas;
    public GameObject GameCanvas;

    private void Pause()
    {
        GameCanvas.SetActive(false);
        PauseCanvas.SetActive(true);
        TimeSpeed = 0;
    }

    private void Resume()
    {
        PauseCanvas.SetActive(false);
        GameCanvas.SetActive(true);
        TimeSpeed = 1;
    }

    void Start()
    {
        isPaused = false;
        TimeSpeed = 1;

        ResumeButton.GetComponent<Button>().onClick.AddListener(Resume);
        PauseButton.GetComponent<Button>().onClick.AddListener(Pause);
    }

    void Update()
    {
        Time.timeScale = TimeSpeed;
    }
}
