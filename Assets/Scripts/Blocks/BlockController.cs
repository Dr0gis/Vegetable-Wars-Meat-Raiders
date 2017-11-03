using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Assets.Scripts
{
    class BlockController : MonoBehaviour
    {
        void Start()
        {
            
        }
        void Update()
        {
            
        }
        void OnCollisionEnter2D(Collision2D collision)
        {
            GetComponent<BlockClass>().OnCollision2D(collision);
        }
        public void CallDestroy()
        {
            Destroy(gameObject);
        }
    }
}
