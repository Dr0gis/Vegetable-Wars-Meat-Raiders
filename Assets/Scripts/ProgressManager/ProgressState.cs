using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class ProgressState : ScriptableObject
{
    private int lastAvaliableLevelId;
    private int amountOfMoney;
    private List<int> starsOnLevel;
    private List<int> scoreOnLevel;

    public void LoadState()
    {
        lastAvaliableLevelId = PlayerPrefs.GetInt("lastAvaliableLevelId", 0);
        amountOfMoney = PlayerPrefs.GetInt("amountOfMoney", 0);
        starsOnLevel = new List<int>();
        scoreOnLevel = new List<int>();
        for (int i = 0; ; ++i)
        {
            int stars = PlayerPrefs.GetInt("starsOnLevel " + i, -1);
            if (stars != -1)
            {
                starsOnLevel.Add(stars);
            }
            else
            {
                break;
            }
        }
        for (int i = 0; ; ++i)
        {
            int score = PlayerPrefs.GetInt("scoreOnLevel " + i, -1);
            if (score != -1)
            {
                scoreOnLevel.Add(score);
            }
            else
            {
                break;
            }
        }
    }

    public void SaveState()
    {
        PlayerPrefs.SetInt("lastAvaliableLevelId", lastAvaliableLevelId);
        PlayerPrefs.SetInt("amountOfMoney", amountOfMoney);

        for (int i = 0; i < starsOnLevel.Count ; ++i)
        {
            PlayerPrefs.SetInt("starsOnLevel " + i, starsOnLevel[i]);
        }
        for (int i = 0; i < scoreOnLevel.Count; ++i)
        {
            PlayerPrefs.SetInt("scoreOnLevel " + i, scoreOnLevel[i]);
        }
    }

    public int LastAvaliableLevelId
    {
        get
        {
            return lastAvaliableLevelId;
        }
        set
        {
            lastAvaliableLevelId = value;
            PlayerPrefs.SetInt("lastAvaliableLevelId", lastAvaliableLevelId);
        }
    }

    public int AmountOfMoney
    {
        get
        {
            return amountOfMoney;
        }
        set
        {
            amountOfMoney = value;
            PlayerPrefs.SetInt("amountOfMoney", amountOfMoney);
        }
    }

    public bool IsLevelAvaliable(int index)
    {
        return index <= LastAvaliableLevelId;
    }

    public int GetStarsOnLevel(int index)
    {
        if (index >= LastAvaliableLevelId)
        {
            return 0;
        }
        return starsOnLevel[index];
    }

    public int GetScoreOnLevel(int index)
    {
        if (index >= LastAvaliableLevelId)
        {
            return 0;
        }
        return scoreOnLevel[index];
    }

    public void SetScoreOnLevel(int index, int score)
    {
        if (index == LastAvaliableLevelId)
        {
            LastAvaliableLevelId = LastAvaliableLevelId + 1;
        }
        if (index == scoreOnLevel.Count)
        {
            scoreOnLevel.Add(0);
        }
        if (index < LastAvaliableLevelId)
        {
            scoreOnLevel[index] = score;
            PlayerPrefs.SetInt("scoreOnLevel " + index, score);
        }
    }

    public void SetStarsOnLevel(int index, int stars)
    {
        if (index == LastAvaliableLevelId)
        {
            LastAvaliableLevelId = LastAvaliableLevelId + 1;
        }
        if (index == starsOnLevel.Count)
        {
            starsOnLevel.Add(0);
        }
        if (index < LastAvaliableLevelId)
        {
            starsOnLevel[index] = stars;
            PlayerPrefs.SetInt("starsOnLevel " + index, stars);
        }
    }
}
