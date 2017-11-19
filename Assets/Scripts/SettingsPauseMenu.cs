using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsPauseMenu : MonoBehaviour
{
    public Button SkipButton;
    public Button MenuButton;
    public Button RetryButton;
    public string Scene;

	void Start ()
    {
        SkipButton.interactable = false;
        MenuButton.onClick.AddListener(menuButtonListener);
        RetryButton.onClick.AddListener(retryButtonListener);
    }

    private void menuButtonListener()
    {
        GameObject.Find("Manager").GetComponent<ObjectManagerScript>().LevelEnded = true;
        SceneManager.LoadScene(Scene);
    }
    private void retryButtonListener()
    {
        GameObject.Find("Manager").GetComponent<ObjectManagerScript>().LevelEnded = true;
        SceneManager.LoadScene("GameScene");
    }
}
