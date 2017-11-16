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
        SoundManager.PlayMusic("Nordic Title");
        
    }
    public void ClickSoundListener()
    {
        SoundManager.PlaySoundUI("clickSound");
    }
    private void OnLevelWasLoaded(int level)
    {
        SceneLoad();
    }
    public void SceneLoad()
    {
        var Scene = SceneManager.GetActiveScene();
        if(Scene.isLoaded == true)
            SoundManager.PlaySound("LevelStartSound");
    }
}
