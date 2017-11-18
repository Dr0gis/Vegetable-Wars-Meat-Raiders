using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsUiSettingsMenu : MonoBehaviour
{
    public Button BackButton;
    public Slider MusicSlider;
    public Slider SoundSlider;
    public Slider VibrationSlider;

	void Start ()
    {
	    BackButton.onClick.AddListener(() => SceneManager.LoadScene("MainMenu"));

        MusicSlider.onValueChanged.AddListener(GetComponent<SoundManagerComponent>().ChangeMusicVolume);
        MusicSlider.value = GetComponent<SoundManagerComponent>().GetCurrentMusicVolume();

        SoundSlider.onValueChanged.AddListener(GetComponent<SoundManagerComponent>().ChangeSoundVolume);
        SoundSlider.value = GetComponent<SoundManagerComponent>().GetCurrentSoundVolume();

        VibrationSlider.onValueChanged.AddListener(GetComponent<VibrationManagerComponent>().SetEnableVibration);
        VibrationSlider.value = GetComponent<VibrationManagerComponent>().GetEnableVibration();
    }	

}