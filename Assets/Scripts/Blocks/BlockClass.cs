using UnityEngine;

namespace Assets.Scripts
{
    public abstract class BlockClass
    {

        public int Health;
        public float Damage;
        public int Score;
        public string Prefab;
        public Vector2 Position;
        public Quaternion Rotation;
        public GameObject CurrentGameObject;
        public bool IsDead;
        private bool isFirstCollision = false;

        protected BlockClass()
        {
            Prefab = null;
            Health = 1;
            Score = 0;
            Position = Vector2.zero;
            Rotation = Quaternion.identity;
            CurrentGameObject = null;
            IsDead = false;
        }

        public BlockClass(int health, float damage, int score, string prefab, GameObject gameObject)
        {
            Health = health;
            Damage = damage;
            Score = score;
            Prefab = prefab;
            Position = Vector2.zero;
            Rotation = Quaternion.identity;
            CurrentGameObject = gameObject;
            IsDead = false;
        }

        public virtual void OnCollision2D(Collision2D collision)
        {
            switch (collision.gameObject.tag)
            {
                case "Vegetable":
                    VegetableClass vegatable = collision.gameObject.GetComponent<VegetableController>().Vegetable;
                    vegatable.Health -= Mathf.RoundToInt(Damage *
                        (vegatable.CurrentGameObject.GetComponent<VegetableController>().Rigidbody2D.velocity.magnitude +
                         CurrentGameObject.GetComponent<VegetableController>().Rigidbody2D.velocity.magnitude) * PhysicsConstants.MagnitudeCoefficient *
                        CurrentGameObject.GetComponent<VegetableController>().Rigidbody2D.mass * PhysicsConstants.MassCoefficient);
                    vegatable.CheckHealth();
                    CurrentGameObject.GetComponent<AudioSource>().Play();
                    break;
                case "Block":
                    BlockClass block = collision.gameObject.GetComponent<BlockController>().Block;
                    block.Health -= Mathf.RoundToInt(Damage *
                        (block.CurrentGameObject.GetComponent<VegetableController>().Rigidbody2D.velocity.magnitude +
                         CurrentGameObject.GetComponent<VegetableController>().Rigidbody2D.velocity.magnitude) * PhysicsConstants.MagnitudeCoefficient *
                        CurrentGameObject.GetComponent<VegetableController>().Rigidbody2D.mass * PhysicsConstants.MassCoefficient);
                    block.CheckHealth();
                    CurrentGameObject.GetComponent<AudioSource>().Play();
                    break;
                case "Meat":
                    MeatClass meat = collision.gameObject.GetComponent<MeatController>().Meat;
                    meat.Health -= Mathf.RoundToInt(Damage *
                        (meat.CurrentGameObject.GetComponent<VegetableController>().Rigidbody2D.velocity.magnitude +
                         CurrentGameObject.GetComponent<VegetableController>().Rigidbody2D.velocity.magnitude) * PhysicsConstants.MagnitudeCoefficient *
                        CurrentGameObject.GetComponent<VegetableController>().Rigidbody2D.mass * PhysicsConstants.MassCoefficient);
                    meat.CheckHealth();
                    break;
                default:
                    if (isFirstCollision)
                    {
                        Health -= Mathf.RoundToInt(PhysicsConstants.DefaultDamage *
                            CurrentGameObject.GetComponent<VegetableController>().Rigidbody2D.velocity.magnitude * PhysicsConstants.MagnitudeCoefficient *
                            CurrentGameObject.GetComponent<VegetableController>().Rigidbody2D.mass * PhysicsConstants.MassCoefficient);
                        CheckHealth();
                    }
                    else
                    {
                        isFirstCollision = true;
                    }
                    break;
            }
        }
        public virtual void OnDestroy()
        {
            if (!IsDead)
            {

                IsDead = true;
                ScoreChanges scoreChanges = GameObject.Find("EventSystem").GetComponent<ScoreChanges>();
                scoreChanges.ScoreValue += Score;

                GameObject.Find("Manager").GetComponent<Scores>().Score += Score;

                scoreChanges.SetTextScore();
                CurrentGameObject.GetComponent<BlockController>().CallDestroy();
            }
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
