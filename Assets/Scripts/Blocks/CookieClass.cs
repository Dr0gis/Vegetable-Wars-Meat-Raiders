using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Assets.Scripts
{
    class CookieClass : BlockClass
    {
        public CookieClass() : base()
        {

        }

        public CookieClass(int health, float damage, int score, string prefab, GameObject gameObject, string collisionSoundTitle) : base(health, damage, score, prefab, gameObject, collisionSoundTitle)
        {
            
        }

        public override BlockClass Clone()
        {
            return new CookieClass(Health, Damage, Score, Prefab, CurrentGameObject, CollisionSound);
        }
    }
}
