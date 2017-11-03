using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetableController : MonoBehaviour
{
    public bool IsShoted;

    private bool abilityUsed;
    private Rigidbody2D rigidbody2D;

	void Start ()
	{
	    IsShoted = false;
	    rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
	}

    public void Shoot(Vector2 velocity)
    {
        if (!IsShoted)
        {
            IsShoted = true;
            rigidbody2D.velocity = velocity;
        }
    }

    public void CallDestroy()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<VegetableClass>().OnCollision2D(collision);
    }
	
	void Update ()
    {
	    gameObject.GetComponent<VegetableClass>().CheckVelocity();
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && IsShoted && !abilityUsed)
        {
            gameObject.GetComponent<VegetableClass>().UseSpecialAbility();
            abilityUsed = true;
        }
	}
}
