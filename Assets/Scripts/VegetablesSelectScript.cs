using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VegetablesSelectScript : MonoBehaviour {

    public Button PreviewButton;
    public Button PlayButton;
    public Button BackButton;

    private Scene currentScene;
    private Scene previewScene;

	void Start ()
    {
        PlayButton.onClick.AddListener(PlayButtonListener);
        BackButton.onClick.AddListener(() => SceneManager.LoadScene("LevelSelection"));
        PreviewButton.onClick.AddListener(PreviewButtonListener);
	}
	
    private void PlayButtonListener()
    {
        //return selected vegetables and other parameters to level
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
