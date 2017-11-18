using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsUILevelSelection : MonoBehaviour
{
    public Button BackButton;
    public string BackScene;

    public List<GameObject> LevelButtons;

    private Dictionary<string, Color> colors = new Dictionary<string, Color>()
    {
        {"NewLevel", new Color32(246, 85, 160, 255)},
        {"TwoStars", new Color32(157, 210, 152, 255)},
        {"OneStars", new Color32(255, 192, 135, 255)},
        {"ThreeStars", new Color32(0, 173, 93, 255)},
        {"DisableLevel", new Color32(98, 98, 98, 255)}
    };

	void Start ()
    {
        BackButton.onClick.AddListener(backButtonListener);

        foreach (var level in LevelButtons)
        {
            ChangeColor(level, "DisableLevel");
            EnableButton(level, false);
            EnableLock(level, true);
            AddListenerOpenLevel(level);
        }

        ChangeColor(LevelButtons[0], "NewLevel");
        EnableButton(LevelButtons[0], true);
        EnableLock(LevelButtons[0], false);
    }

    private void backButtonListener()
    {
        SceneManager.LoadScene(BackScene);
    }

    private void ChangeColor(GameObject level, string color)
    {
        level.GetComponentInChildren<Button>().gameObject.GetComponent<Image>().color = colors[color];
    }
    private void EnableButton(GameObject level, bool enable)
    {
        level.GetComponentInChildren<Button>().interactable = enable;
    }
    private void EnableLock(GameObject level, bool enable)
    {
        level.transform.GetChild(3).gameObject.SetActive(enable);
    }
    private void AddListenerOpenLevel(GameObject level)
    {
        level.GetComponentInChildren<Button>().onClick.AddListener(() => SceneManager.LoadScene("GameScene"));
    }
}
