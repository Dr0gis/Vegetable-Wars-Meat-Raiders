using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoClass : VegetableClass
{
    public TomatoClass() : base()
    {

    }

    public TomatoClass(int health, float damage, int score, float speed, string prefab, GameObject gameObject, int cost) 
        : base(health, damage, score, speed, prefab, gameObject, cost)
    {

    }

    public override void UseSpecialAbility()
    {
        MonoBehaviour.print("explode");
        float radius = 10f;
        float power = 50f;
        Vector3 explosionPos = CurrentGameObject.transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPos, radius);
        foreach (Collider2D hit in colliders)
        {
            Rigidbody2D rb = null;
            try
            {
                rb = hit.GetComponent<Rigidbody2D>();
            }
            catch (Exception e)
            {
                MonoBehaviour.print(e);
            }
            if (rb != null)
            {
                MonoBehaviour.print(rb);
                //rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
                AddExplosionForce(rb, power, explosionPos, radius);
            }
        }
    }

    public static void AddExplosionForce(Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float explosionRadius)
    {
        var dir = (body.transform.position - explosionPosition);
        MonoBehaviour.print(dir);
        float wearoff = 1 - (dir.magnitude / explosionRadius);
        MonoBehaviour.print(wearoff);
        body.AddForce(dir.normalized * explosionForce * wearoff, ForceMode2D.Impulse);
        MonoBehaviour.print(dir.normalized * explosionForce * wearoff);
    }

    public override VegetableClass Clone()
    {
        return new TomatoClass(Health, Damage, Score, Speed, Prefab, CurrentGameObject, Cost);
    }
}
