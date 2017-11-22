using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;
using System;

public abstract class VegetableClass
{ 
    public float Health;
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

    public VegetableClass(float health, float damage, float speed, string prefab, GameObject gameObject)
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
        float damage;
        switch (collider.gameObject.tag)
        {
            case "Block":
                BlockClass block = collider.gameObject.GetComponent<BlockController>().Block;
                damage = Damage *
                    (block.CurrentGameObject.GetComponent<Rigidbody2D>().velocity.magnitude +
                     CurrentGameObject.GetComponent<VegetableController>().Rigidbody2D.velocity.magnitude) * PhysicsConstants.MagnitudeCoefficient *
                    CurrentGameObject.GetComponent<VegetableController>().Rigidbody2D.mass * PhysicsConstants.MassCoefficient;
                block.Health -= damage;

                Debug.Log("Vegetable to block   :   " + damage);

                block.CheckHealth();
                break;
            case "Meat":
                MeatClass meat = collider.gameObject.GetComponent<MeatController>().Meat;
                damage = Damage *
                    (meat.CurrentGameObject.GetComponent<Rigidbody2D>().velocity.magnitude +
                     CurrentGameObject.GetComponent<VegetableController>().Rigidbody2D.velocity.magnitude) * PhysicsConstants.MagnitudeCoefficient *
                    CurrentGameObject.GetComponent<VegetableController>().Rigidbody2D.mass * PhysicsConstants.MassCoefficient;
                meat.Health -= damage;

                Debug.Log("Vegetable to meat   :   " + damage);

                meat.CheckHealth();
                break;
            case "Catapult":

                break;
            case "Vegetable":

                break;
            default:
                damage = PhysicsConstants.DefaultDamage *
                            CurrentGameObject.GetComponent<VegetableController>().Rigidbody2D.velocity.magnitude * PhysicsConstants.MagnitudeCoefficient *
                            CurrentGameObject.GetComponent<VegetableController>().Rigidbody2D.mass * PhysicsConstants.MassCoefficient;
                Health -= damage;

                Debug.Log("Default to vegetable   :   " + damage);

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
