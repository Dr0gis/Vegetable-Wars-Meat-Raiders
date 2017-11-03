using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoClass : VegetableClass
{
    public TomatoClass() : base()
    {

    }

    public TomatoClass(int health, int damage, float speed, string prefab, GameObject gameObject) 
        : base(health, damage, speed, prefab, gameObject)
    {

    }

    public override void UseSpecialAbility()
    {
        
    }

    public override VegetableClass Clone()
    {
        return new TomatoClass(Health, Damage, Speed, Prefab, CurrentGameObject);
    }
}
