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
    public Text CoinsText;
    public bool LevelCompleted;
    private int nextLevel;
    private ObjectManagerScript manager;

	void Start ()
    {
        manager = GameObject.Find("Manager").GetComponent<ObjectManagerScript>();
        GameObject.Find("Manager").GetComponent<ObjectManagerScript>().LevelEnded = true;
        nextLevel = GameObject.Find("Manager").GetComponent<ObjectManagerScript>().CurrentLevel + 1;
        RestartButton.onClick.AddListener(RestartButtonListener);
	    NextLevelButton.onClick.AddListener(NextLevelButtonListener);
	    LevelSelectionButton.onClick.AddListener(LevelSelectionButtonListener);
        //disable NextLevelButton if next level is blocked
	    if (GameObject.Find("Manager").GetComponent<ObjectManagerScript>().CurrentLevel == ProgressManagerComponent.LastAvaliableLevelId && !LevelCompleted)
	    {
	        NextLevelButton.interactable = false; // now disabling if level failed
	    }
        CoinsText.text = "" + ProgressManagerComponent.AmountOfMoney;
    }

    void RestartButtonListener()
    {
        SceneManager.LoadScene("GameScene");
    }

    void NextLevelButtonListener()
    {
        
        SceneManager.LoadScene("GameScene");
        manager.CurrentLevel = nextLevel; //doesn't work
        //change level here
    }

    void LevelSelectionButtonListener()
    {
        SceneManager.LoadScene("LevelSelection");
    }

	void Update ()
    {
        Time.timeScale = 0;
    }
}
