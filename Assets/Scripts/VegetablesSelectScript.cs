﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VegetablesSelectScript : MonoBehaviour
{
    public Button PreviewButton;
    public Button PlayButton;
    public Button BackButton;
    public List<GameObject> SlotsVegetables;
    public List<Button> SelectVegetables;
    public Text CoinsText;
    public Canvas SelectCanvas;
    public Canvas VegetableCanvas;
    public Text MoneyField;

    private Scene currentScene;
    private Scene previewScene;
    private Level currentLevel;
    private ProgressState.SavableVegetableList vegetablesOnLevel;
    private PullObjects pullObjects;
    private int lastSelectedSlotIndx = -1;
    private bool isSelectCanvasOpen = false;

    void Start ()
	{
	    pullObjects = GetComponent<PullObjects>();
	    pullObjects.Initiate();

	    currentLevel = pullObjects.Levels[CurrentLevelSelected.NumberLevel];
        int maxVegetables = currentLevel.MaxVegetables;

        vegetablesOnLevel = ProgressManagerComponent.GetVegetablesOnLevel(CurrentLevelSelected.NumberLevel);

        foreach (var slot in SlotsVegetables)
	    {
	        slot.transform.GetChild(0).gameObject.SetActive(false); // Empty
	        slot.transform.GetChild(1).gameObject.SetActive(false); // Vegetable
	        slot.transform.GetChild(2).gameObject.SetActive(true); // Lock

            GetComponent<SoundSettings>().Buttons.Add(slot.transform.GetChild(0).GetComponent<Button>());
            GetComponent<SoundSettings>().Buttons.Add(slot.transform.GetChild(1).GetComponent<Button>());
            GetComponent<SoundSettings>().Buttons.Add(slot.transform.GetChild(1).transform.GetChild(3).GetComponent<Button>());
        }

        for (int i = 0; i < SelectVegetables.Count; ++i)
        {
            SelectVegetables[i].onClick.AddListener(SelectVegetable(i));
            GetComponent<SoundSettings>().Buttons.Add(SelectVegetables[i]);
        }

        for (int i = 0; i < maxVegetables; ++i)
	    {
	        SlotsVegetables[i].transform.GetChild(0).gameObject.SetActive(true); // Empty
            SlotsVegetables[i].transform.GetChild(1).gameObject.SetActive(false); // Vegetable
            SlotsVegetables[i].transform.GetChild(2).gameObject.SetActive(false); // Lock

            SlotsVegetables[i].transform.GetChild(0).GetComponent<Button>().onClick.AddListener(OpenVegetableSelectionCanvas(i));
            SlotsVegetables[i].transform.GetChild(1).GetComponent<Button>().onClick.AddListener(OpenDeteleButton(i));
            SlotsVegetables[i].transform.GetChild(1).gameObject.transform.GetChild(3).gameObject.GetComponent<Button>().onClick.AddListener(DeteleVegetable);

            if (vegetablesOnLevel[i] != null)
            {
                SetVegetableAvaliable(SlotsVegetables[i], vegetablesOnLevel[i]);
            }
        }

        CoinsText.text = "" + ProgressManagerComponent.AmountOfMoney;
        PlayButton.onClick.AddListener(PlayButtonListener);
        BackButton.onClick.AddListener(BackButtonListener);
        PreviewButton.onClick.AddListener(PreviewButtonListener);

	    EnablePlayButton();
	}

    private void DeteleVegetable()
    {
        if (lastSelectedSlotIndx != -1)
        {
            ProgressManagerComponent.AmountOfMoney = ProgressManagerComponent.AmountOfMoney + vegetablesOnLevel[lastSelectedSlotIndx].Cost;
            MoneyField.GetComponent<Text>().text = ProgressManagerComponent.AmountOfMoney.ToString();

            SlotsVegetables[lastSelectedSlotIndx].transform.GetChild(1).gameObject.transform.GetChild(3).gameObject.SetActive(false);
            SlotsVegetables[lastSelectedSlotIndx].transform.GetChild(1).gameObject.SetActive(false);
            SlotsVegetables[lastSelectedSlotIndx].transform.GetChild(0).gameObject.SetActive(true);

            vegetablesOnLevel.RemoveAt(lastSelectedSlotIndx);

            lastSelectedSlotIndx = -1;
        }
        EnablePlayButton();
    }

    private UnityEngine.Events.UnityAction OpenDeteleButton(int slotIndx)
    {
        return () =>
        {
            if (lastSelectedSlotIndx != -1)
            {
                SlotsVegetables[lastSelectedSlotIndx].transform.GetChild(1).gameObject.transform.GetChild(3).gameObject.SetActive(false);
            }
            lastSelectedSlotIndx = slotIndx;
            SlotsVegetables[lastSelectedSlotIndx].transform.GetChild(1).gameObject.transform.GetChild(3).gameObject.SetActive(true);
        };
    }

    private UnityEngine.Events.UnityAction OpenVegetableSelectionCanvas(int slotIndx)
    {
        return () =>
        {
            if (lastSelectedSlotIndx != -1)
            {
                SlotsVegetables[lastSelectedSlotIndx].transform.GetChild(1).gameObject.transform.GetChild(3).gameObject.SetActive(false);
            }
            lastSelectedSlotIndx = slotIndx;

            for (int i = 0; i < SelectVegetables.Count; ++i)
            {
                if (pullObjects.Vegetables[i].Cost > ProgressManagerComponent.AmountOfMoney)
                {
                    SelectVegetables[i].interactable = false;
                }
            }

            SelectCanvas.gameObject.SetActive(true);
            VegetableCanvas.gameObject.SetActive(false);

            isSelectCanvasOpen = true;
        };
    }

    private void SetVegetableAvaliable(GameObject lastSelectedSlot, VegetableClass vegetable)
    {
        lastSelectedSlot.transform.GetChild(1).gameObject.SetActive(true);
        lastSelectedSlot.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.GetComponent<Image>().sprite =
            ((GameObject)Resources.Load(vegetable.Prefab)).GetComponent<SpriteRenderer>().sprite;
        lastSelectedSlot.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.GetComponent<Image>().preserveAspect = true;

        lastSelectedSlot.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = vegetable.Prefab;

        if (vegetable.Cost == 0)
        {
            lastSelectedSlot.transform.GetChild(1).gameObject.transform.GetChild(2).gameObject.GetComponent<Text>().text = "free";
        }
        else if (vegetable.Cost == 1)
        {
            lastSelectedSlot.transform.GetChild(1).gameObject.transform.GetChild(2).gameObject.GetComponent<Text>().text =
                vegetable.Cost.ToString() + " coin";
        }
        else
        {
            lastSelectedSlot.transform.GetChild(1).gameObject.transform.GetChild(2).gameObject.GetComponent<Text>().text =
                vegetable.Cost.ToString() + " coins";
        }

        lastSelectedSlot.transform.GetChild(0).gameObject.SetActive(false);

        EnablePlayButton();
    }

    private UnityEngine.Events.UnityAction SelectVegetable(int index)
    {
        return () =>
        {
            if (pullObjects.Vegetables[index].Cost <= ProgressManagerComponent.AmountOfMoney)
            {
                ProgressManagerComponent.AmountOfMoney = ProgressManagerComponent.AmountOfMoney - pullObjects.Vegetables[index].Cost;
                MoneyField.GetComponent<Text>().text = ProgressManagerComponent.AmountOfMoney.ToString();

                SelectCanvas.gameObject.SetActive(false);
                VegetableCanvas.gameObject.SetActive(true);

                SetVegetableAvaliable(SlotsVegetables[lastSelectedSlotIndx], pullObjects.Vegetables[index]);

                vegetablesOnLevel.Insert(lastSelectedSlotIndx, pullObjects.Vegetables[index].Clone());

                lastSelectedSlotIndx = -1;
                isSelectCanvasOpen = false;
            }
        };
    }

    private void PlayButtonListener()
    {
        for (int i = 0; i < vegetablesOnLevel.Count; ++i)
        {
            if (vegetablesOnLevel[i] != null)
            {
                vegetablesOnLevel[i].Cost = 0;
            }
        }
        vegetablesOnLevel.Save();

        //return selected vegetables and other parameters to level
        SceneManager.LoadScene("GameScene");
    }

    private void BackButtonListener()
    {
        if (isSelectCanvasOpen)
        {
            SelectCanvas.gameObject.SetActive(false);
            VegetableCanvas.gameObject.SetActive(true);
            lastSelectedSlotIndx = -1;
            isSelectCanvasOpen = false;
        }
        else
        {
            SceneManager.LoadScene("LevelSelection");
        }
    }

    private void PreviewButtonListener()
    {
        //save selected vegetables here
        SceneManager.LoadScene("PreviewScene");
    }

    private void EnablePlayButton()
    {
        bool ableToPlay = false;
        for (int i = 0; i < vegetablesOnLevel.Count; ++i)
        {
            if (vegetablesOnLevel[i] != null)
            {
                ableToPlay = true;
                break;
            }
        }
        if (ableToPlay)
        {
            PlayButton.interactable = true;
        }
        else
        {
            PlayButton.interactable = false;
        }
    }
}
