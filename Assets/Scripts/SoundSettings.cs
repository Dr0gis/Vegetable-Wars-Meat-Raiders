using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    public GameObject clickSoundPref;
    public List<Button> Buttons;
    void Start()
    {
        clickSoundPref.GetComponent<AudioSource>().enabled = true;
        foreach (var button in Buttons)
        {
            button.onClick.AddListener(ClickSoundListener);
        }
    }
    public void ClickSoundListener()
    {       
        clickSoundPref.GetComponent<AudioSource>().PlayOneShot(clickSoundPref.GetComponent<AudioSource>().clip);
    }
}
