using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatController : MonoBehaviour
{
	void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        GetComponent<MeatClass>().OnCollision2D(collision2D);
    }

    public void CallDestroy()
    {
        Destroy(gameObject);
    }
}
