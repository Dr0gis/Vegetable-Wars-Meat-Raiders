using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreChanges : MonoBehaviour
{
    public Text Score;

    public void SetTextScore(string ScoreValue)
    {
        Score.text = ScoreValue;
    }
}
