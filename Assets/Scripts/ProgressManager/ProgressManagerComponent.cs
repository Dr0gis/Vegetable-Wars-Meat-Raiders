using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class ProgressManagerComponent : MonoBehaviour
{
    public static int LastAvaliableLevelId
    {
        get
        {
            return ProgressManager.LastAvaliableLevelId;
        }
        set
        {
            ProgressManager.LastAvaliableLevelId = value;
        }
    }

    public static int AmountOfMoney
    {
        get
        {
            return ProgressManager.AmountOfMoney;
        }
        set
        {
            ProgressManager.AmountOfMoney = value;
        }
    }

    public static bool IsLevelAvaliable(int index)
    {
        return ProgressManager.IsLevelAvaliable(index);
    }

    public static int GetStarsOnLevel(int index)
    {
        return ProgressManager.GetStarsOnLevel(index);
    }

    public static int GetScoreOnLevel(int index)
    {
        return ProgressManager.GetScoreOnLevel(index);
    }

    public static void SetScoreOnLevel(int index, int score)
    {
        ProgressManager.SetScoreOnLevel(index, score);
    }

    public static void SetStarsOnLevel(int index, int stars)
    {
        ProgressManager.SetStarsOnLevel(index, stars);
    }
}
