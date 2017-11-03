using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetableController : MonoBehaviour
{
    private bool isShoted;
    private bool abilityUsed;
    private Rigidbody2D rigidbody2D;

	void Start ()
	{
	    isShoted = false;
	    rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
	}

    public void Shoot(Vector2 velocity)
    {
        isShoted = true;
        rigidbody2D.velocity = velocity;
    }

    public void CallDestroy()
    {
        Destroy(gameObject);
    }
	
	void Update ()
    {
	    gameObject.GetComponent<VegetableClass>().CheckVelocity();
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && isShoted && !abilityUsed)
        {
            gameObject.GetComponent<VegetableClass>().UseSpecialAbility();
            abilityUsed = true;
        }
	}
}
