using System.Collections;
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
