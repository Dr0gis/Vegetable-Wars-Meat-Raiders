using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public abstract class VegetableClass { 

    public int Health;
    public int Damage;
    public float Speed;
    public string Prefab;
    public GameObject CurrentGameObject;

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

    public void OnDestroy()
    {
        CurrentGameObject.GetComponent<VegetableController>().CallDestroy();
    }

    public abstract void UseSpecialAbility();

    public void CheckVelocity()
    {
        if (CurrentGameObject.GetComponent<Rigidbody2D>().velocity == Vector2.zero)
        {
            OnDestroy();
        }
    }

    public void CheckHealth()
    {
        if (Health <= 0)
        {
            OnDestroy();
        }
    }
}
