
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
    public int CurrentLevel;
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
        //return new List<VegetableClass>()
        //{
        //    GetComponent<PullObjects>().Vegetables[0].Clone(),
        //    GetComponent<PullObjects>().Vegetables[0].Clone(),
        //    GetComponent<PullObjects>().Vegetables[1].Clone(),
        //    GetComponent<PullObjects>().Vegetables[1].Clone(),
        //    GetComponent<PullObjects>().Vegetables[2].Clone()
        //};
        List<VegetableClass> result = new List<VegetableClass>();
        print(CurrentLevel + " level");
        foreach (var vegetable in ProgressManagerComponent.GetVegetablesOnLevel(CurrentLevel))
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
        List<BlockClass> listBlocks = GetComponent<PullObjects>().Levels[CurrentLevel].Blocks;
    
        return listBlocks;
    }

    public List<MeatClass> GetAvailableMeats()
    {
        List<MeatClass> listMeats = GetComponent<PullObjects>().Levels[CurrentLevel].Meats;
        
        return listMeats;
    }

	public void Initiate()
    {
        GetComponent<PullObjects>().Initiate();

        GameObject catapult = GameObject.Find("Catapult");
        Vector2 positionVegetable = catapult.GetComponent<Rigidbody2D>().position;

        Vector3 vegetableStartPosition = new Vector3(positionVegetable.x, positionVegetable.y + 1, -1);

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

        ScoreForTwoStars = GetComponent<PullObjects>().Levels[CurrentLevel].ScoreForTwoStars;
        ScoreForThreeStars = GetComponent<PullObjects>().Levels[CurrentLevel].ScoreForThreeStars;
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
        Initiate();
        GetComponent<SoundManagerComponent>().PlaySound("LevelStartSound");

        string LevelMusicTitle = GetComponent<PullObjects>().Levels[CurrentLevel].MusicTitle;
        if (LevelMusicTitle != null && LevelMusicTitle != "")
        {
            SoundManager.PlayMusic(LevelMusicTitle);
        }
    }

    void Start()
    {
        CurrentLevel = CurrentLevelSelected.NumberLevel;

        ShowVegetableButtons();
        
    }
}