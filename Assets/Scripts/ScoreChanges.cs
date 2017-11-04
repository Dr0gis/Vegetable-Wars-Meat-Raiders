using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreChanges : MonoBehaviour
{
    public Text Score;
    public int ScoreValue = 0;

    void Start()
    {
        //SetTextScore();
    }

    public void SetTextScore()
    {
        Score.text = ScoreValue.ToString();
    }
}
