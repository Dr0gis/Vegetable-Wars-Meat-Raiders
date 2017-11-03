using UnityEngine;

namespace Assets.Scripts
{
    public abstract class BlockClass
    {
        public int Health;
        public int Score;
        public string Prefab;
        public GameObject CurrentGameObject;

        protected BlockClass()
        {
            Prefab = null;
            Health = 1;
            Score = 0;
            CurrentGameObject = null;
        }

        public BlockClass(int health, int score, string prefab, GameObject gameObject)
        {
            Prefab = prefab;
            Health = health;
            Score = score;
            CurrentGameObject = gameObject;
        }

        public virtual void OnCollision2D(Collision2D collision)
        {
            switch (collision.gameObject.tag)
            {
                case "Vegetable":
                    VegetableClass vegatable = collision.gameObject.GetComponent<VegetableClass>();
                    vegatable.Health -= 1;
                    vegatable.CheckHealth();
                    break;
                case "Block":
                    BlockClass block = collision.gameObject.GetComponent<BlockClass>();
                    block.Health -= 1;
                    block.CheckHealth();
                    break;
                case "Meat":
                    MeatClass meat = collision.gameObject.GetComponent<MeatClass>();
                    meat.Health -= 1;
                    meat.CheckHealth();
                    break;
                default:
                    Health -= 1;
                    CheckHealth();
                    break;
            }
        }
        public virtual void OnDestroy()
        {
            CurrentGameObject.GetComponent<BlockController>().CallDestroy();
        }
        public virtual void CheckHealth()
        {
            if (Health <= 0)
            {
                OnDestroy();
            }
        }
    }
}
