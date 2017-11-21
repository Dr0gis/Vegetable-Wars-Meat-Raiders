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
    public Vector2 Position;
    public string Prefab;
    public GameObject CurrentGameObject;
    public bool IsDead;
    private bool isFirstCollision = false;

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

    public MeatClass(int health, int damage, int score, string prefab, GameObject currentGameObject, bool isDead)
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
                    (vegatable.CurrentGameObject.GetComponent<VegetableController>().Rigidbody2D.velocity.magnitude +  ) *
                    magnitudeCoefficient *
                    CurrentGameObject.GetComponent<VegetableController>().Rigidbody2D.mass *
                    massCoefficient); ;
                vegatable.CheckHealth();
                break;
            case "Block":
                BlockClass block = collision2D.gameObject.GetComponent<BlockController>().Block;
                block.Health -= Damage;
                block.CheckHealth();
                break;
            case "Meat":
                Health -= Damage;
                CheckHealth();
                break;
            default:
                if (isFirstCollision)
                {
                    Health -= 1;
                    CheckHealth();
                }
                else
                {
                    isFirstCollision = true;
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
