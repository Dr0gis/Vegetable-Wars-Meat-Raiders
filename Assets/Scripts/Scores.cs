using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scores : MonoBehaviour
{

    public int Score;
    public int MaxScore;
    public int MinScore;

	void Start ()
    {
        //UpdateScores();
    }

    public void UpdateScores()
    {
        foreach (var meat in GameObject.Find("Manager").GetComponent<ObjectManagerScript>().AvailableMeats)
        {
            MinScore += meat.Score;
            MaxScore += meat.Score;
        }
        foreach (var block in GameObject.Find("Manager").GetComponent<ObjectManagerScript>().AvailableBlocks)
        {
            MaxScore += block.Score;
        }
    }

	void Update ()
    {
		
	}
}
