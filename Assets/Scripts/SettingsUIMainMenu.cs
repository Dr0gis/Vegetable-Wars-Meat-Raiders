using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsUIMainMenu : MonoBehaviour
{
    public Button PlayButton;
    public Button LeaderboardButton;
    public Button SettingsButton;

    void Awake()
    {
        SoundManager.PlayMusic("Nordic Title");
    }

	void Start ()
    {
	    PlayButton.onClick.AddListener(playButtonListener);
	    LeaderboardButton.onClick.AddListener(leaderboardButtonListener);
	    SettingsButton.onClick.AddListener(settingsButtonListener);
    }

    void playButtonListener()
    {
        SceneManager.LoadScene("LevelSelection");
    }

    void leaderboardButtonListener()
    {
        
    }

    void settingsButtonListener()
    {
        SceneManager.LoadScene("SettingsMenu");
    }

}
