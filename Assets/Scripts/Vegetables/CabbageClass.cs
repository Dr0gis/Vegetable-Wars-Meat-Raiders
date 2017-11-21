using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabbageClass : VegetableClass
{
    public CabbageClass() : base()
    {
        
    }

    public CabbageClass(float health, float damage, float speed, string prefab, GameObject gameObject)
        : base(health, damage, speed, prefab, gameObject)
    {
        
    }

    public override void UseSpecialAbility()
    {

    }

    public override VegetableClass Clone()
    {
        return new CabbageClass(Health, Damage, Speed, Prefab, CurrentGameObject);
    }
}
