using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfLevelCheck : MonoBehaviour {

    public int LeftVegetables;
    public int LeftMeats;
    public List<MeatClass> Meats;
    public List<VegetableClass> Vegetables;
    public List<BlockClass> Blocks;
    public bool LevelEnded;
    public bool CheckStarted;
    public bool Movement;

	void Start ()
    {
        Meats = GetComponent<ObjectManagerScript>().AvailableMeats;
        Vegetables = GetComponent<ObjectManagerScript>().AvailableVegetables;
        Blocks =  GetComponent<ObjectManagerScript>().AvailableBlocks;
        LeftMeats = Meats.Count;
        LeftVegetables = Vegetables.Count;
        LevelEnded = false;
        CheckStarted = false;
        Movement = false;
    }
	
	void Update ()
    {
        if (LeftMeats == 0 || LeftVegetables == 0)
        {
            if (!CheckStarted)
            {
                CheckStarted = true;
                InvokeRepeating("CheckMovement", 0, 1);
            }
        }
	}

    void CheckMovement()
    {
        Movement = false;
        foreach (var vegetable in Vegetables)
        {
            if (!vegetable.IsDead && vegetable.IsShoted && !Movement)
            {
                if (vegetable.CurrentGameObject.GetComponent<VegetableController>().Rigidbody2D.velocity != Vector2.zero)
                {
                    Movement = true;
                }
            }
        }
        foreach (var meat in Meats)
        {
            if (!meat.IsDead && !Movement)
            {
                if (meat.CurrentGameObject.GetComponent<Rigidbody2D>().velocity != Vector2.zero)
                {
                    Movement = true;
                }
            }
        }
        foreach (var block in Blocks)
        {
            if (!block.IsDead && !Movement)
            {
                if (block.CurrentGameObject.GetComponent<Rigidbody2D>().velocity != Vector2.zero)
                {
                    Movement = true;
                }
            }
        }
        if (!Movement)
        {
            DisableButtons();
            if (LeftMeats > 0)
            {
                SceneManager.LoadScene("FailLevelEnd", LoadSceneMode.Additive);
            }
            else
            {
                SceneManager.LoadScene("SuccesLevelEnd", LoadSceneMode.Additive);
            }
        }
    }

    void DisableButtons()
    {
        GameObject.Find("PauseButton").SetActive(false);
        foreach (var button in GameObject.Find("Manager").GetComponent<ObjectManagerScript>().VegetableButtons)
        {
            button.SetActive(false);
        }
    }
}
