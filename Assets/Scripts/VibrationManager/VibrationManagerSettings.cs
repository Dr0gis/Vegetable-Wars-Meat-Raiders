using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationManagerSettings : ScriptableObject
{
    private bool _enabledVibration;

    public void SaveSettings()
    {
        PlayerPrefs.SetInt("VM_EnabledVibration", _enabledVibration ? 1 : 0);
    }

    public void LoadSettings()
    {
        _enabledVibration = PlayerPrefs.GetInt("VM_EnabledVibration", 0) == 1;
    }

    public bool GetEnableVibration()
    {
        return _enabledVibration;
    }

    public void SetEnableVibration(bool enable)
    {
        _enabledVibration = enable;
        SaveSettings();
    }
}
