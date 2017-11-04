using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Assets.Scripts
{
    class BlockController : MonoBehaviour
    {
        public BlockClass Block;

        void Start()
        {
            
        }
        void Update()
        {
            
        }
        void OnCollisionEnter2D(Collision2D collision)
        {
            Block.OnCollision2D(collision);
        }
        public void CallDestroy()
        {
            Destroy(gameObject);
        }
    }
}
