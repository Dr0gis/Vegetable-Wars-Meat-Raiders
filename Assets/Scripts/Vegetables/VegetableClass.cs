using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;
using System;

public abstract class VegetableClass : MonoBehaviour
{ 
    public int Health;
    public int Damage;
    public float Speed;
    public string Prefab;
    public GameObject CurrentGameObject;

    private bool wasCollision = false;

    public VegetableClass()
    {
        Health = 1;
        Damage = 0;
        Speed = 1;
        Prefab = null;
        CurrentGameObject = null;
    }

    public VegetableClass(int health, int damage, float speed, string prefab, GameObject gameObject)
    {
        Health = health;
        Damage = damage;
        Speed = speed;
        Prefab = prefab;
        CurrentGameObject = gameObject;
    }

    public void OnCollision2D(Collision2D collider)
    {
        if (!wasCollision && collider.gameObject.tag != "Catapult")
        {
            // GameObject.Find("Manager").GetComponent<ObjectManagerScript>().SetNextVagetable();
            wasCollision = true;
        }
        switch (collider.gameObject.tag)
        {
            case "Block":
                BlockClass block = collider.gameObject.GetComponent<BlockClass>();
                block.Health -= Damage;
                block.CheckHealth();
                break;
            case "Meat":
                MeatClass meat = collider.gameObject.GetComponent<MeatClass>();
                meat.Health -= Damage;
                meat.CheckHealth();
                break;
            default:
                Health -= 1;
                CheckHealth();
                break;
        }
    }

    public void OnDestroyObject()
    {
        if (false && CurrentGameObject.GetComponent<VegetableController>().IsShoted)
        {
            CurrentGameObject.GetComponent<VegetableController>().CallDestroy();
        }
    }

    public abstract void UseSpecialAbility();

    public void CheckVelocity()
    {
        if (false && CurrentGameObject.GetComponent<Rigidbody2D>().velocity == Vector2.zero)
        {
            OnDestroyObject();
        }
    }

    public void CheckHealth()
    {
        if (Health <= 0)
        {
            OnDestroyObject();
        }
    }

    public abstract VegetableClass Clone();
}
