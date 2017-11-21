using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class MeatClass
{
    public float Health;
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

    public MeatClass(float health, float damage, int score, string prefab, GameObject currentGameObject, bool isDead)
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
        float damage;
        switch (collision2D.gameObject.tag)
        {
            case "Vegetable":
                VegetableClass vegatable = collision2D.gameObject.GetComponent<VegetableController>().Vegetable;
                damage = Damage *
                    (vegatable.CurrentGameObject.GetComponent<VegetableController>().Rigidbody2D.velocity.magnitude +
                     CurrentGameObject.GetComponent<Rigidbody2D>().velocity.magnitude) * PhysicsConstants.MagnitudeCoefficient *
                    CurrentGameObject.GetComponent<Rigidbody2D>().mass * PhysicsConstants.MassCoefficient;
                vegatable.Health -= damage;

                Debug.Log("Meat to vegetable   :   " + damage);

                vegatable.CheckHealth();
                break;
            case "Block":
                BlockClass block = collision2D.gameObject.GetComponent<BlockController>().Block;
                damage = Damage *
                    (block.CurrentGameObject.GetComponent<Rigidbody2D>().velocity.magnitude +
                     CurrentGameObject.GetComponent<Rigidbody2D>().velocity.magnitude) * PhysicsConstants.MagnitudeCoefficient *
                    CurrentGameObject.GetComponent<Rigidbody2D>().mass * PhysicsConstants.MassCoefficient;
                block.Health -= damage;

                Debug.Log("Meat to block   :   " + damage);

                block.CheckHealth();
                break;
            case "Meat":
                MeatClass meat = collision2D.gameObject.GetComponent<MeatController>().Meat;
                damage = Damage *
                    (meat.CurrentGameObject.GetComponent<Rigidbody2D>().velocity.magnitude +
                     CurrentGameObject.GetComponent<Rigidbody2D>().velocity.magnitude) * PhysicsConstants.MagnitudeCoefficient *
                    CurrentGameObject.GetComponent<Rigidbody2D>().mass * PhysicsConstants.MassCoefficient;
                Health -= damage;

                Debug.Log("Meat to meat   :   " + damage);

                CheckHealth();
                break;
            default:
                if (wasFirstCollision)
                {
                    damage = PhysicsConstants.DefaultDamage *
                            CurrentGameObject.GetComponent<Rigidbody2D>().velocity.magnitude * PhysicsConstants.MagnitudeCoefficient *
                            CurrentGameObject.GetComponent<Rigidbody2D>().mass * PhysicsConstants.MassCoefficient;
                    Health -= damage;

                    Debug.Log("Default to meat   :   " + damage);

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
