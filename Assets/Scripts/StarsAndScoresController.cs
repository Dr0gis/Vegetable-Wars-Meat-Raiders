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
	    int minScore = GameObject.Find("Manager").GetComponent<Scores>().MinScore;
	    int maxScore = GameObject.Find("Manager").GetComponent<Scores>().MaxScore;
        Score.text = Convert.ToString(GameObject.Find("Manager").GetComponent<Scores>().Score);
	    if (GameObject.Find("Manager").GetComponent<Scores>().Score >= minScore + 0.4 * (maxScore-minScore))
	    {
	        SecondStar.interactable = true;
	    }
	    if (GameObject.Find("Manager").GetComponent<Scores>().Score >= minScore + 0.8 * (maxScore - minScore))
	    {
	        ThirdStar.interactable = true;
        }
	}
	
	
	void Update ()
    {
		
	}
}
