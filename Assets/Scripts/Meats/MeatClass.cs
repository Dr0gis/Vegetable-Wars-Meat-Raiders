using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatClass
{
    public int Health;
    public int Damage;
    public string Prefab;
    public GameObject CurrentGameObject;
    public bool isDead;

    public MeatClass()
    {
        Health = 1;
        Prefab = null;
        CurrentGameObject = null;
        this.isDead = false;
    }

    public MeatClass(int health, string prefab, GameObject currentGameObject, bool isDead)
    {
        Health = health;
        Prefab = prefab;
        CurrentGameObject = currentGameObject;
        this.isDead = isDead;
    }

    public void OnCollision2D(Collision2D collision2D)
    {
        switch (collision2D.gameObject.tag)
        {
            case "Vegetable":
                //VegatableClass vegatable = collision2D.gameObject.GetComponent<VegatableClass>();
                //vegatable.Helth -= Damage;
                //vegatable.CheckHealth();
                break;
            case "Block":
                //BlockClass block = collision2D.gameObject.GetComponent<BlockClass>();
                //block.Health -= Damage;
                //block.CheckHealth();
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
