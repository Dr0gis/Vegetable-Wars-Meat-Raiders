
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
    public Text CoinsText;
    public Text ReceivedCoins;

	void Start ()
	{
	    int score = GameObject.Find("Manager").GetComponent<Scores>().Score;
        int stars = 1;
	    int currentLevel = GameObject.Find("Manager").GetComponent<ObjectManagerScript>().CurrentLevel;
        //ProgressManagerComponent.LastAvaliableLevelId = Mathf.Max(GameObject.Find("Manager").GetComponent<ObjectManagerScript>().CurrentLevel + 1, ProgressManagerComponent.LastAvaliableLevelId);
        //int minScore = GameObject.Find("Manager").GetComponent<Scores>().MinScore;
        //int maxScore = GameObject.Find("Manager").GetComponent<Scores>().MaxScore;

        //if (GameObject.Find("Manager").GetComponent<Scores>().Score >= minScore + 0.4 * (maxScore-minScore))
        //{
        //    SecondStar.interactable = true;
        //    stars++;
        //}
        //if (GameObject.Find("Manager").GetComponent<Scores>().Score >= minScore + 0.8 * (maxScore - minScore))
        //{
        //    ThirdStar.interactable = true;
        //       stars++;
        //   }

        // TODO: Add stars logic here 
	    foreach (var vegetable in GameObject.Find("Manager").GetComponent<ObjectManagerScript>().AvailableVegetables)
	    {
	        if (!vegetable.IsShoted)
	        {
	            score += vegetable.Score;
	        }
	    }
	    if (score >= GameObject.Find("Manager").GetComponent<ObjectManagerScript>().ScoreForTwoStars)
	    {
	        stars++;
	        SecondStar.interactable = true;
        }
	    if (score >= GameObject.Find("Manager").GetComponent<ObjectManagerScript>().ScoreForThreeStars)
	    {
	        stars++;
	        ThirdStar.interactable = true;
	    }

        if (GameObject.Find("Manager").GetComponent<Scores>().Score > ProgressManagerComponent.GetScoreOnLevel(GameObject.Find("Manager").GetComponent<ObjectManagerScript>().CurrentLevel))
        {
            ProgressManagerComponent.SetScoreOnLevel(currentLevel, score);
            ProgressManagerComponent.SetStarsOnLevel(currentLevel, stars);
        }

	    int receivedCoins = 5;
	    switch (stars)
	    {
            case 1:
                if (ProgressManagerComponent.GetStarsOnLevel(currentLevel) == 0)
                {
                    receivedCoins += 5;
                }
                break;
	        case 2:
	            if (ProgressManagerComponent.GetStarsOnLevel(currentLevel) == 0)
	            {
	                receivedCoins += 5;
	                receivedCoins += 10;
                }
	            else
	            {
	                if (ProgressManagerComponent.GetStarsOnLevel(currentLevel) == 1)
	                {
	                    receivedCoins += 10;
	                }
	            }
                break;
	        case 3:
	            if (ProgressManagerComponent.GetStarsOnLevel(currentLevel) == 0)
	            {
	                receivedCoins += 5;
	                receivedCoins += 10;
	                receivedCoins += 15;
                }
	            else
	            {
	                if (ProgressManagerComponent.GetStarsOnLevel(currentLevel) == 1)
	                {
	                    receivedCoins += 10;
	                    receivedCoins += 15;
                    }
	                else
	                {
	                    if (ProgressManagerComponent.GetStarsOnLevel(currentLevel) == 2)
	                    {
	                        receivedCoins += 15;
	                    }
	                }
	            }
                break;
        }
	    ReceivedCoins.text = "" + receivedCoins;
	    ProgressManagerComponent.AmountOfMoney += receivedCoins;
        CoinsText.text = "" + ProgressManagerComponent.AmountOfMoney;
	    Score.text = Convert.ToString(score);
    }
	
	
	void Update ()
    {
		
	}
}
