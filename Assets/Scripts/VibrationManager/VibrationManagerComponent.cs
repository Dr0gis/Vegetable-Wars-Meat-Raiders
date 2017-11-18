using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationManagerComponent : MonoBehaviour
{
    public float GetEnableVibration()
    {
        return VibrationManager.GetEnableVibration() ? 1f : 0f;
    }

    public void SetEnableVibration(float enable)
    {
        VibrationManager.SetEnableVibration(enable.Equals(1f));
    }
}
