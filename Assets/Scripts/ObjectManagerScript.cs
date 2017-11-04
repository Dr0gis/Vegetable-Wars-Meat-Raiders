using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class ObjectManagerScript : MonoBehaviour
{
    public Vector3 VegetableStartPosition = new Vector3(-10, -5, 0);

    public List<VegetableClass> AvaliableVegetables;
    public List<BlockClass> AvaliableBlocks;

    public Button VegetableButton1;
    public Button VegetableButton2;
    public Button VegetableButton3;
    public Button VegetableButton4;
    public Button VegetableButton5;

    private UnityEngine.Events.UnityAction changeVegetable(int indx)
    {
        return () =>
        {
            if (AvaliableVegetables[indx].CurrentGameObject != null && !AvaliableVegetables[indx].CurrentGameObject.GetComponent<VegetableController>().IsShoted)
            {
                foreach (var vegetable in AvaliableVegetables)
                {
                    if (vegetable.CurrentGameObject != null && !vegetable.CurrentGameObject.GetComponent<VegetableController>().IsShoted)
                    {
                        vegetable.CurrentGameObject.SetActive(false);
                    }
                }
                AvaliableVegetables[indx].CurrentGameObject.SetActive(true);
                GameObject.Find("Catapult").GetComponent<ShotScript>().CurrentVegetable = AvaliableVegetables[indx].CurrentGameObject;
            }
        };
    }

    public void SetNextVagetable()
    {
        foreach (var vegetable in AvaliableVegetables)
        {
            if (!vegetable.CurrentGameObject.GetComponent<VegetableController>().IsShoted)
            {
                vegetable.CurrentGameObject.SetActive(false);
            }
        }
        foreach (var vegetable in AvaliableVegetables)
        {
            if (!vegetable.CurrentGameObject.GetComponent<VegetableController>().IsShoted)
            {
                vegetable.CurrentGameObject.SetActive(true);
                GameObject.Find("Catapult").GetComponent<ShotScript>().CurrentVegetable = vegetable.CurrentGameObject;
                break;
            }
        }
    }

    public List<VegetableClass> GetAvaliableVegetables()
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

    public List<BlockClass> GetAvaliableBlocks()
    {
        List<BlockClass> listBlocks = new List<BlockClass>();

        BlockClass tempBlock = GetComponent<PullObjects>().Blocks[0].Clone();
        tempBlock.Position = new Vector2(6, -9f);
        tempBlock.Rotation = Quaternion.identity;

        listBlocks.Add(tempBlock);

        tempBlock = GetComponent<PullObjects>().Blocks[1].Clone();
        tempBlock.Position = new Vector2(8, -9f);
        tempBlock.Rotation = Quaternion.identity;

        listBlocks.Add(tempBlock);

        tempBlock = GetComponent<PullObjects>().Blocks[2].Clone();
        tempBlock.Position = new Vector2(11, -9f);
        tempBlock.Rotation = Quaternion.identity;

        listBlocks.Add(tempBlock);

        /*tempBlock = GetComponent<PullObjects>().Blocks[0].Clone();
        tempBlock.Position = new Vector2(9.5f, -7.25f);
        tempBlock.Rotation = new Quaternion(0, 0, 90, 0);

        listBlocks.Add(tempBlock);*/

        return listBlocks;
    }

	public void Initiate()
    {
        GetComponent<PullObjects>().Initiate();

        AvaliableVegetables = GetAvaliableVegetables();
        foreach (var vegetable in AvaliableVegetables)
        {
            vegetable.CurrentGameObject = Instantiate((GameObject)Resources.Load(vegetable.Prefab), VegetableStartPosition, Quaternion.identity);
            vegetable.CurrentGameObject.SetActive(false);
            vegetable.CurrentGameObject.GetComponent<VegetableController>().Vegetable = vegetable;
        }

        AvaliableBlocks = GetAvaliableBlocks();
        foreach (var block in AvaliableBlocks)
        {
            block.CurrentGameObject = Instantiate((GameObject)Resources.Load(block.Prefab), block.Position, block.Rotation);
            block.CurrentGameObject.GetComponent<BlockController>().Block = block;
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
