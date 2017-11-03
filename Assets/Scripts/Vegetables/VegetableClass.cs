using System.Collections;
using System.Collections.Generic;
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
                //collider.gameObject.GetComponent<BlockClass>().Health -= Damage;
                break;
                //.....
        }
    }

    public void OnDestroy()
    {
        CurrentGameObject.GetComponent<VegetableController>().CallDestroy();
    }

    public abstract void UseSpecialAbility();

    public void Check()
    {
        if (Health <= 0)
        {
            OnDestroy();
        }
        if (CurrentGameObject.GetComponent<Rigidbody2D>().velocity == Vector2.zero)
        {
            OnDestroy();
        }
    }
}
