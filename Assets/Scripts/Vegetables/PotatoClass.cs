using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoClass : VegetableClass
{
    public PotatoClass() : base()
    {

    }

    public PotatoClass(float health, float damage, float speed, string prefab, GameObject gameObject) 
        : base(health, damage, speed, prefab, gameObject)
    {

    }

    public override void UseSpecialAbility()
    {
        
    }

    public override VegetableClass Clone()
    {
        return new PotatoClass(Health, Damage, Speed, Prefab, CurrentGameObject);
    }
}
