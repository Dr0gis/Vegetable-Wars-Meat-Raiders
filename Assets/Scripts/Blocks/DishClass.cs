using UnityEngine;
namespace Assets.Scripts
{
    class DishClass : BlockClass
    {
        public DishClass() : base()
        {
            
        }

        public DishClass(int health, float damage, int score, string prefab, GameObject gameObject, string collisionSoundTitle) : base(health, damage, score, prefab, gameObject, collisionSoundTitle)
        {
        }

        public override BlockClass Clone()
        {
            return new BreadClass(Health, Damage, Score, Prefab, CurrentGameObject, CollisionSound);
        }
    }
}
