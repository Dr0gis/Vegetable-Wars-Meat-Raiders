﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Assets.Scripts
{
    class BreadClass : BlockClass
    {
        public BreadClass() : base()
        {
        }

        public BreadClass(int health, float damage, int score, string prefab, GameObject gameObject, string collisionSoundTitle) : base(health, damage, score, prefab, gameObject, collisionSoundTitle)
        {       
        }

        public override void OnCollision2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Vegetable")
            {
                Rigidbody2D vegetableRigidbody2D = collision.gameObject.GetComponent<Rigidbody2D>();
                vegetableRigidbody2D.velocity /= 2;
            }
            base.OnCollision2D(collision);
        }

        public override BlockClass Clone()
        {
            return new BreadClass(Health, Damage, Score, Prefab, CurrentGameObject, CollisionSound);
        }
    }
}
