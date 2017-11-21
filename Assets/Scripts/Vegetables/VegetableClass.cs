using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;
using System;

public abstract class VegetableClass
{ 
    public int Health;
    public float Damage;
    public float Speed;
    public string Prefab;
    public GameObject CurrentGameObject;
    public bool IsDead;
    private bool wasCollision = false;

    public VegetableClass()
    {
        Health = 1;
        Damage = 0;
        Speed = 1;
        Prefab = null;
        CurrentGameObject = null;
        IsDead = false;
    }

    public VegetableClass(int health, float damage, float speed, string prefab, GameObject gameObject)
    {
        Health = health;
        Damage = damage;
        Speed = speed;
        Prefab = prefab;
        CurrentGameObject = gameObject;
        IsDead = false;
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
                BlockClass block = collider.gameObject.GetComponent<BlockController>().Block;
                block.Health -= Mathf.RoundToInt(Damage *
                    (block.CurrentGameObject.GetComponent<VegetableController>().Rigidbody2D.velocity.magnitude +
                     CurrentGameObject.GetComponent<VegetableController>().Rigidbody2D.velocity.magnitude) * PhysicsConstants.MagnitudeCoefficient *
                    CurrentGameObject.GetComponent<VegetableController>().Rigidbody2D.mass * PhysicsConstants.MassCoefficient);
                block.CheckHealth();
                break;
            case "Meat":
                MeatClass meat = collider.gameObject.GetComponent<MeatController>().Meat;
                meat.Health -= Mathf.RoundToInt(Damage *
                    (meat.CurrentGameObject.GetComponent<VegetableController>().Rigidbody2D.velocity.magnitude +
                     CurrentGameObject.GetComponent<VegetableController>().Rigidbody2D.velocity.magnitude) * PhysicsConstants.MagnitudeCoefficient *
                    CurrentGameObject.GetComponent<VegetableController>().Rigidbody2D.mass * PhysicsConstants.MassCoefficient);
                meat.CheckHealth();
                break;
            case "Catapult":

                break;
            case "Vegetable":

                break;
            default:
                Health -= Mathf.RoundToInt(PhysicsConstants.DefaultDamage *
                            CurrentGameObject.GetComponent<VegetableController>().Rigidbody2D.velocity.magnitude * PhysicsConstants.MagnitudeCoefficient *
                            CurrentGameObject.GetComponent<VegetableController>().Rigidbody2D.mass * PhysicsConstants.MassCoefficient); ;
                CheckHealth();
                break;
        }
    }

    public void OnDestroyObject()
    {
        if (CurrentGameObject.GetComponent<VegetableController>().IsShoted && !IsDead)
        {
            IsDead = true;
            CurrentGameObject.GetComponent<VegetableController>().CallDestroy();
        }
    }

    public abstract void UseSpecialAbility();

    public void CheckVelocity()
    {
        if (CurrentGameObject.GetComponent<VegetableController>().Rigidbody2D.velocity == Vector2.zero)
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
