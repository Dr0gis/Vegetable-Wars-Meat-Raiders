using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationManager : MonoBehaviour
{
    private VibrationManagerSettings _settings;

    public static void SetEnableVibration(bool enable)
    {
        if (Instance != null)
        {
            Instance._settings.SetEnableVibration(enable);
        }
    }

    public static bool GetEnableVibration()
    {
        if (Instance != null)
        {
            return Instance._settings.GetEnableVibration();
        }
        return false;
    }




    private static VibrationManager _instance;
    private static bool _inited;

    public static VibrationManager Instance
    {
        get
        {
            if (applicationIsQuitting)
            {
                /*Debug.LogWarning("[Singleton] Instance '" + typeof(VibrationManager) +
                                 "' already destroyed on application quit." +
                                 " Won't create again - returning null.");*/
                return null;
            }

            if (_inited)
                return _instance;

            return new GameObject("Vibration (singleton)").AddComponent<VibrationManager>();
        }
    }

    private static bool applicationIsQuitting = false;

    public void OnDestroy()
    {
        applicationIsQuitting = true;
    }


    void Awake()
    {
        // Only one instance of SoundManager at a time!
        if (_inited)
        {
            Destroy(gameObject);
            return;
        }
        _inited = true;
        _instance = this;
        DontDestroyOnLoad(gameObject);

        _settings = Resources.Load<VibrationManagerSettings>("VibrationManagerSettings");
        if (_settings == null)
        {
            Debug.LogWarning("VibrationManagerSettings not founded resources. Using default settings");
            _settings = ScriptableObject.CreateInstance<VibrationManagerSettings>();
        }

        _settings.LoadSettings();
    }
}
