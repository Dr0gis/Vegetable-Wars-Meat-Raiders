using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoClass : VegetableClass
{
    public PotatoClass() : base()
    {

    }

    public PotatoClass(int health, float damage, int score, float speed, string prefab, GameObject gameObject, int cost) 
        : base(health, damage, score, speed, prefab, gameObject, cost)
    {

    }

    public override void UseSpecialAbility()
    {
        
    }

    public override VegetableClass Clone()
    {
        return new PotatoClass(Health, Damage, Score, Speed, Prefab, CurrentGameObject, Cost);
    }
}
