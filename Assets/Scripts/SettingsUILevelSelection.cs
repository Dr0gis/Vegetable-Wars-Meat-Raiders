using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsUILevelSelection : MonoBehaviour
{
    public Button BackButton;

    public string BackScene;

	void Start ()
    {
        BackButton.onClick.AddListener(backButtonListener);
    }

    private void backButtonListener()
    {
        SceneManager.LoadScene(BackScene);
    }
}
