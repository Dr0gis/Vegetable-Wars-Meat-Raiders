using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class ObjectManagerScript : MonoBehaviour
{
    public List<VegetableClass> AvailableVegetables;
    public List<BlockClass> AvailableBlocks;
    public List<MeatClass> AvailableMeats;
    public int CurrentLevel;

    public Button VegetableButton1;
    public Button VegetableButton2;
    public Button VegetableButton3;
    public Button VegetableButton4;
    public Button VegetableButton5;

    public bool LevelEnded = false;

    private UnityEngine.Events.UnityAction changeVegetable(int indx)
    {
        return () =>
        {
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
    }

    public List<VegetableClass> GetAvailableVegetables()
    {
        return new List<VegetableClass>()
        {
            GetComponent<PullObjects>().Vegetables[0].Clone(),
            GetComponent<PullObjects>().Vegetables[0].Clone(),
            GetComponent<PullObjects>().Vegetables[1].Clone(),
            GetComponent<PullObjects>().Vegetables[1].Clone(),
            GetComponent<PullObjects>().Vegetables[2].Clone()
        };
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
            GetComponent<Scores>().MaxScore += block.Score;
        }

        AvailableMeats = GetAvailableMeats();
        foreach (var meat in AvailableMeats)
        {
            meat.CurrentGameObject = Instantiate((GameObject)Resources.Load(meat.Prefab), meat.Position, Quaternion.identity);
            meat.CurrentGameObject.GetComponent<MeatController>().Meat = meat;
            GetComponent<Scores>().MaxScore += meat.Score;
            GetComponent<Scores>().MinScore += meat.Score;
        }
    }

    void Start()
    {
        VegetableButton1.onClick.AddListener(changeVegetable(0));
        VegetableButton2.onClick.AddListener(changeVegetable(1));
        VegetableButton3.onClick.AddListener(changeVegetable(2));
        VegetableButton4.onClick.AddListener(changeVegetable(3));
        VegetableButton5.onClick.AddListener(changeVegetable(4));
    }
}
