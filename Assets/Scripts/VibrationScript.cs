
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnDestroy()
    {
        if (!GameObject.Find("Manager").GetComponent<ObjectManagerScript>().LevelEnded)
        {
            
        }
        {
            Handheld.Vibrate();
        }
    }

}

