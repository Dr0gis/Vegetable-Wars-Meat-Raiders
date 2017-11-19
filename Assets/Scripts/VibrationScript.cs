using System;
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
            GameObject manager = GameObject.Find("Manager");
            ObjectManagerScript objectManagerScript = null;
            try
            {
                objectManagerScript = manager.GetComponent<ObjectManagerScript>();
            }
            catch (Exception e)
            {
                print(e);
            }
            
            if (objectManagerScript != null && !objectManagerScript.LevelEnded)
            {
                Handheld.Vibrate();
            }
        }
    }

}
