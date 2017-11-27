using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundSettings : MonoBehaviour
{
    public List<Button> Buttons;

    void Start()
    {
        foreach (var button in Buttons)
        {
            button.onClick.AddListener(ClickSoundListener);
        }
    }
    public void ClickSoundListener()
    {
        SoundManager.PlaySoundUI("clickSound");
    }
}
