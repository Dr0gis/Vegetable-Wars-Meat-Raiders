
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ObjectManagerScript : MonoBehaviour
{
    public List<VegetableClass> AvailableVegetables;
    public List<BlockClass> AvailableBlocks;
    public List<MeatClass> AvailableMeats;
    public int CurrentLevelNumber;
    public int ScoreForTwoStars;
    public int ScoreForThreeStars;

    public GameObject ButtonGroupObject;
    public List<GameObject> VegetableButtons;

    public bool LevelEnded = false;

    private UnityEngine.Events.UnityAction changeVegetable(int indx)
    {
        return () =>
        {   
            foreach (GameObject go in VegetableButtons)
            {
                go.GetComponent<Button>().interactable = true;
            }
            if (AvailableVegetables[indx].CurrentGameObject != null && !AvailableVegetables[indx].CurrentGameObject.GetComponent<VegetableController>().IsShoted)
            {
                foreach (var vegetable in AvailableVegetables)
                {
                    if (vegetable.CurrentGameObject != null && !vegetable.CurrentGameObject.GetComponent<VegetableController>().IsShoted)
                    {
                        vegetable.CurrentGameObject.SetActive(false);
                    }
                }
                AvailableVegetables[indx].CurrentGameObject.SetActive(true);
                GameObject.Find("Catapult").GetComponent<ShotScript>().CurrentVegetable = AvailableVegetables[indx].CurrentGameObject;
                GameObject.Find("Catapult").GetComponent<ShotScript>().MoveCalf(true);
                Camera.main.GetComponent<CameraMovementScript>().FocusOnVegetable = false;
                Camera.main.GetComponent<CameraMovementScript>().MoveToCatapult();
                VegetableButtons[indx].GetComponent<Button>().interactable = false;
            }
        };
    }

    public void SetNextVagetable()
    {
        foreach (var vegetable in AvailableVegetables)
        {
            if (!vegetable.CurrentGameObject.GetComponent<VegetableController>().IsShoted)
            {
                vegetable.CurrentGameObject.SetActive(false);
            }
        }
        foreach (var vegetable in AvailableVegetables)
        {
            if (!vegetable.CurrentGameObject.GetComponent<VegetableController>().IsShoted)
            {
                vegetable.CurrentGameObject.SetActive(true);
                GameObject.Find("Catapult").GetComponent<ShotScript>().CurrentVegetable = vegetable.CurrentGameObject;
                break;
            }
        }
        VegetableButtons[0].GetComponent<Button>().interactable = false;
    }

    public void DisableShoted()
    {
        for (int i = 0; i < AvailableVegetables.Count; ++i)
        {
            if (AvailableVegetables[i].IsShoted)
            {
                VegetableButtons[i].SetActive(false);
            }
        }
    }

    public List<VegetableClass> GetAvailableVegetables()
    {
        List<VegetableClass> result = new List<VegetableClass>();
        foreach (var vegetable in ProgressManagerComponent.GetVegetablesOnLevel(CurrentLevelNumber))
        {
            if (vegetable != null)
            {
                result.Add(vegetable.Clone());
            }
        }
        return result;
    }

    public List<BlockClass> GetAvailableBlocks()
    {
        List<BlockClass> listBlocks = GetComponent<PullObjects>().Levels[CurrentLevelNumber].Blocks;
    
        return listBlocks;
    }

    public List<MeatClass> GetAvailableMeats()
    {
        List<MeatClass> listMeats = GetComponent<PullObjects>().Levels[CurrentLevelNumber].Meats;
        
        return listMeats;
    }

	public void Initiate()
    {
        GetComponent<PullObjects>().Initiate();

        Level currentLevel = GetComponent<PullObjects>().Levels[CurrentLevelNumber];

        GameObject catapult = GameObject.Find("Catapult");
        catapult.transform.position = currentLevel.CatapultPosition;

        Vector2 positionVegetable = catapult.GetComponent<Rigidbody2D>().position;

        Vector3 vegetableStartPosition = new Vector3(positionVegetable.x, positionVegetable.y + 1, -1);

        foreach(GameObject landscapeShape in currentLevel.Landscapes)
        {
            GameObject temp = Instantiate(landscapeShape);
            temp.transform.SetParent(GameObject.Find("Boundaries").transform);
        }

        AvailableVegetables = GetAvailableVegetables();
        foreach (var vegetable in AvailableVegetables)
        {
            vegetable.CurrentGameObject = Instantiate((GameObject)Resources.Load(vegetable.Prefab), vegetableStartPosition, Quaternion.identity);
            vegetable.CurrentGameObject.SetActive(false);
            vegetable.CurrentGameObject.GetComponent<VegetableController>().Vegetable = vegetable;
        }

        AvailableBlocks = GetAvailableBlocks();
        foreach (var block in AvailableBlocks)
        {
            block.CurrentGameObject = Instantiate((GameObject)Resources.Load(block.Prefab), block.Position, block.Rotation);
            block.CurrentGameObject.GetComponent<BlockController>().Block = block;
        }

        AvailableMeats = GetAvailableMeats();
        foreach (var meat in AvailableMeats)
        {
            meat.CurrentGameObject = Instantiate((GameObject)Resources.Load(meat.Prefab), meat.Position, Quaternion.identity);
            meat.CurrentGameObject.GetComponent<MeatController>().Meat = meat;
        }

        ScoreForTwoStars = GetComponent<PullObjects>().Levels[CurrentLevelNumber].ScoreForTwoStars;
        ScoreForThreeStars = GetComponent<PullObjects>().Levels[CurrentLevelNumber].ScoreForThreeStars;
    }
    public void ShowVegetableButtons()
    {
        List<VegetableClass> vegetables = GetAvailableVegetables();
        for (int i = 0; i < vegetables.Count; ++i)
        {
            VegetableButtons.Add(Instantiate((GameObject)Resources.Load("SelectButton"), ButtonGroupObject.transform));
            VegetableButtons[i].GetComponent<Button>().onClick.AddListener(changeVegetable(i));
            GetComponent<SoundSettings>().Buttons.Add(VegetableButtons[i].GetComponent<Button>());
            VegetableButtons[i].GetComponent<Button>().image.sprite = ((GameObject)Resources.Load(vegetables[i].Prefab)).GetComponent<SpriteRenderer>().sprite;
        }
    }

    void Awake()
    {
        CurrentLevelNumber = CurrentLevelSelected.NumberLevel;
        Initiate();
        GetComponent<SoundManagerComponent>().PlaySound("LevelStartSound");

        string LevelMusicTitle = GetComponent<PullObjects>().Levels[CurrentLevelNumber].MusicTitle;
        if (LevelMusicTitle != null && LevelMusicTitle != "")
        {
            SoundManager.PlayMusic(LevelMusicTitle);
        }

        ShowVegetableButtons();
    }

    void Start()
    {

    }
}