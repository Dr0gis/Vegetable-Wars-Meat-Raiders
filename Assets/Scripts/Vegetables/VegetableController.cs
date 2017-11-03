using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetableController : MonoBehaviour
{

    private bool isShoted;
    private Rigidbody2D rigidbody2D;


	void Start ()
	{
	    isShoted = false;
	    rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
	}

    public void Shoot()
    {
        isShoted = true;
        //add velocity here
    }

    public void CallDestroy()
    {
        Destroy(gameObject);
    }
	
	void Update ()
    {
	    gameObject.GetComponent<VegetableClass>().Check();
        if (Input.touchCount>0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            gameObject.GetComponent<VegetableClass>().UseSpecialAbility();
        }
	}
}
