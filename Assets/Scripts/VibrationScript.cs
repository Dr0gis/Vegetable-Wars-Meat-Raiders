using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationScript : MonoBehaviour {

    private static bool isOn;
	// Use this for initialization
	void Start ()
    {
        isOn = true;	
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    public static bool IsOn
    {
        get
        {
            return isOn;
        }
        set
        {
            isOn = value;
        }
    }

    void OnDestroy()
    {
        if (isOn)
        { 
            Handheld.Vibrate();
        }
    }

}
