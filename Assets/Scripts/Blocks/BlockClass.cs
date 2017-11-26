using System;
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
        public string CollisionSound;
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

        public BlockClass(int health, float damage, int score, string prefab, GameObject gameObject, string collisionSoundTitle)
        {
            Health = health;
            Damage = damage;
            Score = score;
            Prefab = prefab;
            Position = Vector2.zero;
            Rotation = Quaternion.identity;
            CurrentGameObject = gameObject;
            CollisionSound = collisionSoundTitle;
            IsDead = false;
        }

        public virtual void OnCollision2D(Collision2D collision)
        {
            int damage;
            switch (collision.gameObject.tag)
            {
                case "Vegetable":
                    VegetableClass vegatable = collision.gameObject.GetComponent<VegetableController>().Vegetable;
                    damage = Mathf.RoundToInt(Damage *
                        (vegatable.CurrentGameObject.GetComponent<VegetableController>().Rigidbody2D.velocity.magnitude +
                         CurrentGameObject.GetComponent<Rigidbody2D>().velocity.magnitude) * PhysicsConstants.MagnitudeCoefficient *
                        CurrentGameObject.GetComponent<Rigidbody2D>().mass * PhysicsConstants.MassCoefficient);
                    vegatable.Health -= damage;
                    vegatable.CheckHealth();

                    Debug.Log("Block to vegetable   :   " + damage);

                    CurrentGameObject.GetComponent<SoundManagerComponent>().PlaySound(CollisionSound);
                    break;
                case "Block":
                    BlockClass block = collision.gameObject.GetComponent<BlockController>().Block;
                    damage = Mathf.RoundToInt(Damage *
                        (block.CurrentGameObject.GetComponent<Rigidbody2D>().velocity.magnitude +
                         CurrentGameObject.GetComponent<Rigidbody2D>().velocity.magnitude) * PhysicsConstants.MagnitudeCoefficient *
                        CurrentGameObject.GetComponent<Rigidbody2D>().mass * PhysicsConstants.MassCoefficient);
                    block.Health -= damage;
                    block.CheckHealth();

                    Debug.Log("Block to block   :   " + damage);

                    CurrentGameObject.GetComponent<SoundManagerComponent>().PlaySound(CollisionSound);
                    break;
                case "Meat":
                    MeatClass meat = collision.gameObject.GetComponent<MeatController>().Meat;
                    damage = Mathf.RoundToInt(Damage *
                        (meat.CurrentGameObject.GetComponent<Rigidbody2D>().velocity.magnitude +
                         CurrentGameObject.GetComponent<Rigidbody2D>().velocity.magnitude) * PhysicsConstants.MagnitudeCoefficient *
                        CurrentGameObject.GetComponent<Rigidbody2D>().mass * PhysicsConstants.MassCoefficient);
                    meat.Health -= damage;

                    Debug.Log("Block to meat   :   " + damage);

                    meat.CheckHealth();
                    break;
                default:
                    if (isFirstCollision)
                    {
                        damage = Mathf.RoundToInt(PhysicsConstants.DefaultDamage *
                            CurrentGameObject.GetComponent<Rigidbody2D>().velocity.magnitude * PhysicsConstants.MagnitudeCoefficient *
                            CurrentGameObject.GetComponent<Rigidbody2D>().mass * PhysicsConstants.MassCoefficient);
                        Health -= damage;

                        Debug.Log("Default to block   :   " + damage);

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
