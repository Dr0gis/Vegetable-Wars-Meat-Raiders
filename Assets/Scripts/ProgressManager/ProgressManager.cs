using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class ProgressManager : MonoBehaviour
{
    private ProgressState progressState;

    private static ProgressManager instance;
    private static bool inited;

    public static ProgressManager Instance
    {
        get
        {
            if (applicationIsQuitting)
            {
                return null;
            }
            if (inited)
            {
                return instance;
            }

            return new GameObject("Progress (singleton)").AddComponent<ProgressManager>();
        }
    }

    private static bool applicationIsQuitting = false;

    public void OnDestroy()
    {
        applicationIsQuitting = true;
    }

    void Awake()
    {
        if (inited)
        {
            Destroy(gameObject);
            return;
        }
        inited = true;
        instance = this;
        DontDestroyOnLoad(gameObject);

        progressState = Resources.Load<ProgressState>("ProgressState");
        if (progressState == null)
        {
            Debug.LogWarning("ProgressState not founded resources. Using default settings");
            progressState = ScriptableObject.CreateInstance<ProgressState>();
        }
        progressState.LoadState();
    }

    public static int LastAvaliableLevelId
    {
        get
        {
            return Instance.progressState.LastAvaliableLevelId;
        }
        set
        {
            Instance.progressState.LastAvaliableLevelId = value;
        }
    }

    public static int AmountOfMoney
    {
        get
        {
            return Instance.progressState.AmountOfMoney;
        }
        set
        {
            Instance.progressState.AmountOfMoney = value;
        }
    }

    public static bool IsLevelAvaliable(int index)
    {
        return Instance.progressState.IsLevelAvaliable(index);
    }

    public static int GetStarsOnLevel(int index)
    {
        return Instance.progressState.GetStarsOnLevel(index);
    }

    public static int GetScoreOnLevel(int index)
    {
        return Instance.progressState.GetScoreOnLevel(index);
    }

    public static void SetScoreOnLevel(int index, int score)
    {
        Instance.progressState.SetScoreOnLevel(index, score);
    }

    public static void SetStarsOnLevel(int index, int stars)
    {
        Instance.progressState.SetStarsOnLevel(index, stars);
    }

    public static ProgressState.SavableVegetableList GetVegetablesOnLevel(int index)
    {
        return Instance.progressState.GetVegetablesOnLevel(index);
    }
}