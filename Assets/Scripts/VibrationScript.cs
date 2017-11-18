<<<<<<< HEAD
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
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationScript : MonoBehaviour
{
    void Start ()
    {
        
	}

    void OnDestroy()
    {
        if (GetComponent<VibrationManagerComponent>().GetEnableVibration().Equals(1f))
        { 
            Handheld.Vibrate();
        }
    }

}
>>>>>>> 13faec30fb397ba8ba391b78a46b3e5ca3c87e83
