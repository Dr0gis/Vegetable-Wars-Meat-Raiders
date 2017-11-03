using UnityEngine;
namespace Assets.Scripts
{
    class DishClass : BlockClass
    {
        public DishClass() : base()
        {
            
        }

        public DishClass(int health, int score, string prefab, GameObject gameObject) : base(health, score, prefab, gameObject)
        {

        }
    }
}
