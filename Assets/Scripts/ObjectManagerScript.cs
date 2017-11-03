using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectManagerScript : MonoBehaviour
{
    public Vector3 VegetableStartPosition = new Vector3(-10, -5, 0);

    public List<VegetableClass> AvaliableVegetables;

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
        GetComponent<PullObjects>().Initiate();
        return new List<VegetableClass>()
        {
            GetComponent<PullObjects>().Vegetables[0].Clone(),
            GetComponent<PullObjects>().Vegetables[0].Clone(),
            GetComponent<PullObjects>().Vegetables[1].Clone(),
            GetComponent<PullObjects>().Vegetables[1].Clone(),
            GetComponent<PullObjects>().Vegetables[2].Clone()
        };
    }

	public void Initiate()
    { 
        AvaliableVegetables = GetAvaliableVegetables();
        foreach (var vegetable in AvaliableVegetables)
        {
            vegetable.CurrentGameObject = Instantiate((GameObject)Resources.Load(vegetable.Prefab), VegetableStartPosition, Quaternion.identity);
            vegetable.CurrentGameObject.SetActive(false);
            vegetable.CurrentGameObject.GetComponent<VegetableController>().Vegetable = vegetable;
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
