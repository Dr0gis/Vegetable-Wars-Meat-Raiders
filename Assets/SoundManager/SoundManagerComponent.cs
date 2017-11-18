using UnityEngine;
using System.Collections;

public class SoundManagerComponent : MonoBehaviour
{
    public void PlaySound(string name)
    {
        SoundManager.PlaySound(name);
    }

    public void PlaySoundNotPausable(string name)
    {
        SoundManager.PlaySoundUI(name);
    }

    public void ChangeSoundVolume(float volume)
    {
        SoundManager.SetSoundVolume(volume);
    }

    public void ChangeMusicVolume(float volume)
    {
        SoundManager.SetMusicVolume(volume);
    }

    public float GetCurrentMusicVolume()
    {
        return SoundManager.GetMusicVolume();
    }

    public float GetCurrentSoundVolume()
    {
        return SoundManager.GetSoundVolume();
    }

    public void ToggleMusicMuted()
    {
        SoundManager.SetMusicMuted(!SoundManager.GetMusicMuted());
    }

    public void ToggleSoundMuted()
    {
        SoundManager.SetSoundMuted(!SoundManager.GetSoundMuted());
    }
}
