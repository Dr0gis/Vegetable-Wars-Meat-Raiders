
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StarsAndScoresController : MonoBehaviour
{

    public Text Score;
    public Button SecondStar;
    public Button ThirdStar;

	void Start ()
	{
        int stars = 1;
        //ProgressManagerComponent.LastAvaliableLevelId = Mathf.Max(GameObject.Find("Manager").GetComponent<ObjectManagerScript>().CurrentLevel + 1, ProgressManagerComponent.LastAvaliableLevelId);
	    int minScore = GameObject.Find("Manager").GetComponent<Scores>().MinScore;
	    int maxScore = GameObject.Find("Manager").GetComponent<Scores>().MaxScore;
        Score.text = Convert.ToString(GameObject.Find("Manager").GetComponent<Scores>().Score);
	    if (GameObject.Find("Manager").GetComponent<Scores>().Score >= minScore + 0.4 * (maxScore-minScore))
	    {
	        SecondStar.interactable = true;
            stars++;
	    }
	    if (GameObject.Find("Manager").GetComponent<Scores>().Score >= minScore + 0.8 * (maxScore - minScore))
	    {
	        ThirdStar.interactable = true;
            stars++;
        }
        if (GameObject.Find("Manager").GetComponent<Scores>().Score > ProgressManagerComponent.GetScoreOnLevel(GameObject.Find("Manager").GetComponent<ObjectManagerScript>().CurrentLevel))
        {
            ProgressManagerComponent.SetScoreOnLevel(GameObject.Find("Manager").GetComponent<ObjectManagerScript>().CurrentLevel, GameObject.Find("Manager").GetComponent<Scores>().Score);
            ProgressManagerComponent.SetStarsOnLevel(GameObject.Find("Manager").GetComponent<ObjectManagerScript>().CurrentLevel, stars);
        }
	}
	
	
	void Update ()
    {
		
	}
}
