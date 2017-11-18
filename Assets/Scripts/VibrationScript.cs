using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationScript : MonoBehaviour
{
    void Start ()
    {
        GetComponent<VibrationManagerComponent>().GetEnableVibration();
    }

    void OnDestroy()
    {
        if (GetComponent<VibrationManagerComponent>().GetEnableVibration().Equals(1f))
        { 
            Handheld.Vibrate();
        }
    }

}
