using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using  UnityEngine.SceneManagement;

public class EndLevelSettings : MonoBehaviour
{

    public Button RestartButton;
    public Button NextLevelButton;
    public Button LevelSelectionButton;
    public bool LevelCompleted;

	void Start ()
    {
		RestartButton.onClick.AddListener(RestartButtonListener);
	    NextLevelButton.onClick.AddListener(NextLevelButtonListener);
	    LevelSelectionButton.onClick.AddListener(LevelSelectionButtonListener);
        //disable NextLevelButton if next level is blocked
	    if (!LevelCompleted)
	    {
	        NextLevelButton.interactable = false; // now disabling if level failed
	    }
	}

    void RestartButtonListener()
    {
        SceneManager.LoadScene("GameScene");
    }

    void NextLevelButtonListener()
    {
        // Load next level
    }

    void LevelSelectionButtonListener()
    {
        SceneManager.LoadScene("LevelSelection");
    }

	void Update ()
    {
		
	}
}
