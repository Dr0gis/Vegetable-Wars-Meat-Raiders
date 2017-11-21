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
        //ProgressManagerComponent.LastAvaliableLevelId = 0; //use this to clear level locks

        for (int i = 0; i < LevelButtons.Count; i++)
        {
            ChangeColor(LevelButtons[i], "DisableLevel");
            EnableButton(LevelButtons[i], false);
            EnableLock(LevelButtons[i], true);
            AddListenerOpenLevel(LevelButtons[i], i);
        }
        
        for (int j = 0; j < ProgressManagerComponent.LastAvaliableLevelId; j++)
        {
            LevelButtons[j].transform.GetChild(2).gameObject.SetActive(true);
            switch (ProgressManagerComponent.GetStarsOnLevel(j))
            {
                case 1:
                    LevelButtons[j].transform.GetChild(2).GetComponentsInChildren<Button>()[1].interactable = false;
                    LevelButtons[j].transform.GetChild(2).GetComponentsInChildren<Button>()[2].interactable = false;
                    ChangeColor(LevelButtons[j], "OneStars");
                    break;
                case 2:
                    LevelButtons[j].transform.GetChild(2).GetComponentsInChildren<Button>()[2].interactable = false;
                    ChangeColor(LevelButtons[j], "TwoStars");
                    break;
                case 3:
                    ChangeColor(LevelButtons[j], "ThreeStars");
                    break;
                default:
                    break;
            }
            EnableButton(LevelButtons[j], true);
            EnableLock(LevelButtons[j], false);
        }

        ChangeColor(LevelButtons[ProgressManagerComponent.LastAvaliableLevelId], "NewLevel");
        EnableButton(LevelButtons[ProgressManagerComponent.LastAvaliableLevelId], true);
        EnableLock(LevelButtons[ProgressManagerComponent.LastAvaliableLevelId], false);
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
    private void AddListenerOpenLevel(GameObject level, int index)
    {
        //level.GetComponentInChildren<Button>().onClick.AddListener(() => SceneManager.LoadScene("GameScene"));
        level.GetComponentInChildren<Button>().onClick.AddListener(LoadLevel(index));
    }

    private UnityEngine.Events.UnityAction LoadLevel(int indx)
    {
        return () =>
        {
            SceneManager.LoadScene("GameScene");
           // GameObject.Find("Manager").GetComponent<ObjectManagerScript>().CurrentLevel = indx;
        };
    }
}
