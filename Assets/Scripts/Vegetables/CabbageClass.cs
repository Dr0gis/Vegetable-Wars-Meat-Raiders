using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabbageClass : VegetableClass
{
    public CabbageClass() : base()
    {
        
    }

    public CabbageClass(int health, float damage, int score, float speed, string prefab, GameObject gameObject, int cost)
        : base(health, damage, score, speed, prefab, gameObject, cost)
    {
        
    }

    public override void UseSpecialAbility()
    {

    }

    public override VegetableClass Clone()
    {
        return new CabbageClass(Health, Damage, Score, Speed, Prefab, CurrentGameObject, Cost);
    }
}
