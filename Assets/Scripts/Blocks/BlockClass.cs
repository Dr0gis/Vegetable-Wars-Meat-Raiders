using UnityEngine;

namespace Assets.Scripts
{
    public abstract class BlockClass
    {
        public int Health;
        public int Score;
        public string Prefab;
        public Vector2 Position;
        public Quaternion Rotation;
        public GameObject CurrentGameObject;

        protected BlockClass()
        {
            Prefab = null;
            Health = 1;
            Score = 0;
            Position = Vector2.zero;
            Rotation = Quaternion.identity;
            CurrentGameObject = null;
        }

        public BlockClass(int health, int score, string prefab, GameObject gameObject)
        {
            Health = health;
            Score = score;
            Prefab = prefab;
            Position = Vector2.zero;
            Rotation = Quaternion.identity;
            CurrentGameObject = gameObject;
        }

        public virtual void OnCollision2D(Collision2D collision)
        {
            switch (collision.gameObject.tag)
            {
                case "Vegetable":
                    VegetableClass vegatable = collision.gameObject.GetComponent<VegetableController>().Vegetable;
                    vegatable.Health -= 1;
                    vegatable.CheckHealth();
                    break;
                case "Block":
                    BlockClass block = collision.gameObject.GetComponent<BlockController>().Block;
                    block.Health -= 1;
                    block.CheckHealth();
                    break;
                case "Meat":
                    MeatClass meat = collision.gameObject.GetComponent<MeatController>().Meat;
                    meat.Health -= 1;
                    meat.CheckHealth();
                    break;
                default:
                    Health -= 0;
                    CheckHealth();
                    break;
            }
        }
        public virtual void OnDestroy()
        {
            ScoreChanges scoreChanges =  GameObject.Find("EventSystem").GetComponent<ScoreChanges>();
            scoreChanges.ScoreValue += Score;
            scoreChanges.SetTextScore();
            CurrentGameObject.GetComponent<BlockController>().CallDestroy();
        }
        public virtual void CheckHealth()
        {
            if (Health <= 0)
            {
                OnDestroy();
            }
        }

        public abstract BlockClass Clone();
    }
}
