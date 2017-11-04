using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatController : MonoBehaviour
{
    public MeatClass Meat;

	void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        Meat.OnCollision2D(collision2D);
    }

    public void CallDestroy()
    {
        Destroy(gameObject);
    }
}
