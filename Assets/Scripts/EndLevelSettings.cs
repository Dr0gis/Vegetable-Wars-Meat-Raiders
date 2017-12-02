using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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
        nextLevel = GameObject.Find("Manager").GetComponent<ObjectManagerScript>().CurrentLevelNumber + 1;
        RestartButton.onClick.AddListener(RestartButtonListener);
	    NextLevelButton.onClick.AddListener(NextLevelButtonListener);
	    LevelSelectionButton.onClick.AddListener(LevelSelectionButtonListener);
        GetComponent<PullObjects>().Initiate();
        //GameObject.Find("EventSystem").GetComponent<PullObjects>().Initiate();
        int totalLevelCount = GetComponent<PullObjects>().Levels.Count;
        //disable NextLevelButton if next level is blocked
	    if ((GameObject.Find("Manager").GetComponent<ObjectManagerScript>().CurrentLevelNumber == ProgressManagerComponent.LastAvaliableLevelId && !LevelCompleted) || nextLevel >= totalLevelCount)
	    {
	        NextLevelButton.interactable = false;
	    }
        CoinsText.text = "" + ProgressManagerComponent.AmountOfMoney;
    }

    void RestartButtonListener()
    {
        SceneManager.LoadScene("GameScene");
    }

    void NextLevelButtonListener()
    {
        CurrentLevelSelected.NumberLevel = nextLevel;
        SceneManager.LoadScene("VegetablesSelection");
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
