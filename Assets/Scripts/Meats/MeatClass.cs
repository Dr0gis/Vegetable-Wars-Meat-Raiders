using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class MeatClass
{
    public int Health;
    public int Damage;
    public int Score;
    public string Prefab;
    public GameObject CurrentGameObject;
    public bool isDead;

    public MeatClass()
    {
        Health = 1;
        Damage = 1;
        Score = 0;
        Prefab = null;
        CurrentGameObject = null;
        this.isDead = false;
    }

    public MeatClass(int health, int damage, int score, string prefab, GameObject currentGameObject, bool isDead)
    {
        Health = health;
        Damage = damage;
        Score = score;
        Prefab = prefab;
        CurrentGameObject = currentGameObject;
        this.isDead = isDead;
    }

    public void OnCollision2D(Collision2D collision2D)
    {
        switch (collision2D.gameObject.tag)
        {
            case "Vegetable":
                VegetableClass vegatable = collision2D.gameObject.GetComponent<VegetableClass>();
                vegatable.Health -= Damage;
                vegatable.CheckHealth();
                break;
            case "Block":
                BlockClass block = collision2D.gameObject.GetComponent<BlockClass>();
                block.Health -= Damage;
                block.CheckHealth();
                break;
            case "Meat":
                Health -= Damage;
                CheckHealth();
                break;
            default:
                Health -= 1;
                CheckHealth();
                break;
        }
    }

    public void OnDestroy()
    {
        CurrentGameObject.GetComponent<MeatController>().CallDestroy();
    }

    public void CheckHealth()
    {
        if (Health <= 0)
        {
            OnDestroy();
            isDead = true;
        }
    }
}
