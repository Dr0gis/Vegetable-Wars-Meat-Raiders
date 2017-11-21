using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class MeatClass
{
    public int Health;
    public float Damage;
    public int Score;
    public Vector2 Position;
    public string Prefab;
    public GameObject CurrentGameObject;
    public bool IsDead;
    private bool wasFirstCollision = false;

    public MeatClass()
    {
        Health = 1;
        Damage = 1;
        Score = 0;
        Position = Vector2.zero;
        Prefab = null;
        CurrentGameObject = null;
        this.IsDead = false;
    }

    public MeatClass(int health, float damage, int score, string prefab, GameObject currentGameObject, bool isDead)
    {
        Health = health;
        Damage = damage;
        Score = score;
        Position = Vector2.zero;
        Prefab = prefab;
        CurrentGameObject = currentGameObject;
        this.IsDead = isDead;
    }

    public void OnCollision2D(Collision2D collision2D)
    {
        switch (collision2D.gameObject.tag)
        {
            case "Vegetable":
                VegetableClass vegatable = collision2D.gameObject.GetComponent<VegetableController>().Vegetable;
                vegatable.Health -= Mathf.RoundToInt(Damage *
                    (vegatable.CurrentGameObject.GetComponent<VegetableController>().Rigidbody2D.velocity.magnitude +
                     CurrentGameObject.GetComponent<VegetableController>().Rigidbody2D.velocity.magnitude) * PhysicsConstants.MagnitudeCoefficient *
                    CurrentGameObject.GetComponent<VegetableController>().Rigidbody2D.mass * PhysicsConstants.MassCoefficient);
                vegatable.CheckHealth();
                break;
            case "Block":
                BlockClass block = collision2D.gameObject.GetComponent<BlockController>().Block;
                block.Health -= Mathf.RoundToInt(Damage *
                    (block.CurrentGameObject.GetComponent<VegetableController>().Rigidbody2D.velocity.magnitude +
                     CurrentGameObject.GetComponent<VegetableController>().Rigidbody2D.velocity.magnitude) * PhysicsConstants.MagnitudeCoefficient *
                    CurrentGameObject.GetComponent<VegetableController>().Rigidbody2D.mass * PhysicsConstants.MassCoefficient);
                block.CheckHealth();
                break;
            case "Meat":
                MeatClass meat = collision2D.gameObject.GetComponent<MeatController>().Meat;
                Health -= Mathf.RoundToInt(Damage *
                    (meat.CurrentGameObject.GetComponent<VegetableController>().Rigidbody2D.velocity.magnitude +
                     CurrentGameObject.GetComponent<VegetableController>().Rigidbody2D.velocity.magnitude) * PhysicsConstants.MagnitudeCoefficient *
                    CurrentGameObject.GetComponent<VegetableController>().Rigidbody2D.mass * PhysicsConstants.MassCoefficient);
                CheckHealth();
                break;
            default:
                if (wasFirstCollision)
                {
                    Health -= Mathf.RoundToInt(PhysicsConstants.DefaultDamage *
                            CurrentGameObject.GetComponent<VegetableController>().Rigidbody2D.velocity.magnitude * PhysicsConstants.MagnitudeCoefficient *
                            CurrentGameObject.GetComponent<VegetableController>().Rigidbody2D.mass * PhysicsConstants.MassCoefficient);
                    CheckHealth();
                }
                else
                {
                    wasFirstCollision = true;
                }
                break;
        }
    }

    public void OnDestroy()
    {
        if (!IsDead)
        {
            IsDead = true;
            ScoreChanges scoreChanges = GameObject.Find("EventSystem").GetComponent<ScoreChanges>();
            scoreChanges.ScoreValue += Score;

            GameObject.Find("Manager").GetComponent<Scores>().Score += Score;

            scoreChanges.SetTextScore();
            CurrentGameObject.GetComponent<MeatController>().CallDestroy();
        }
    }

    public void CheckHealth()
    {
        if (Health <= 0)
        {
            OnDestroy();
        }
    }

    public MeatClass Clone()
    {
        return new MeatClass(Health, Damage, Score, Prefab, CurrentGameObject, IsDead);
    }
}
