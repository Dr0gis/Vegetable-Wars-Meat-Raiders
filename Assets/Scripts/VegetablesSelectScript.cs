using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VegetablesSelectScript : MonoBehaviour {

    public Button PreviewButton;
    public Button PlayButton;
    public Button BackButton;
    public List<GameObject> SlotsVegetables;
    public Text CoinsText;

    private Scene currentScene;
    private Scene previewScene;
    private Level currentLevel;
    private ProgressState.SavableVegetableList vegetablesOnLevel;

    void Start ()
	{
	    PullObjects pullObjects = GetComponent<PullObjects>();
	    pullObjects.Initiate();

	    currentLevel = pullObjects.Levels[CurrentLevelSelected.NumberLevel];
        int maxVegetables = currentLevel.MaxVegetables;

	    foreach (var slot in SlotsVegetables)
	    {
	        slot.transform.GetChild(1).gameObject.SetActive(false); // Empty
	        slot.transform.GetChild(1).gameObject.SetActive(false); // Vegetable
	        slot.transform.GetChild(2).gameObject.SetActive(true); // Lock
	    }

        for (int i = 0; i < maxVegetables; ++i)
	    {
	        SlotsVegetables[i].transform.GetChild(0).gameObject.SetActive(true); // Empty
            SlotsVegetables[i].transform.GetChild(1).gameObject.SetActive(false); // Vegetable
            SlotsVegetables[i].transform.GetChild(2).gameObject.SetActive(false); // Lock
        }

	    vegetablesOnLevel = ProgressManagerComponent.GetVegetablesOnLevel(CurrentLevelSelected.NumberLevel);

        CoinsText.text = "" + ProgressManagerComponent.AmountOfMoney;
        PlayButton.onClick.AddListener(PlayButtonListener);
        BackButton.onClick.AddListener(BackButtonListener);
        PreviewButton.onClick.AddListener(PreviewButtonListener);
	}


    private void BackButtonListener()
    {
        CurrentLevelSelected.CameraScale = 9f;
        CurrentLevelSelected.CameraPosition = new Vector3(0f, -4f, -10f);
        SceneManager.LoadScene("LevelSelection");
    }
    private void PlayButtonListener()
    {
        //return selected vegetables and other parameters to level
        CurrentLevelSelected.CameraScale = 9f;
        CurrentLevelSelected.CameraPosition = new Vector3(0f, -4f, -10f);
        SceneManager.LoadScene("GameScene");
    }

    private void PreviewButtonListener()
    {
        //save selected vegetables here
        SceneManager.LoadScene("PreviewScene");
    }

    void Update ()
    {
	    	
	}
}
