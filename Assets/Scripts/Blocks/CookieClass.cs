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

        public CookieClass(int health, int score, string prefab, GameObject gameObject) : base(health, score, prefab, gameObject)
        {

        }

        public override BlockClass Clone()
        {
            return new CookieClass(Health, Score, Prefab, CurrentGameObject);
        }
    }
}
