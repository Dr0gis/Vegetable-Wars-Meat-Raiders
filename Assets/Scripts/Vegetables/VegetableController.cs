using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetableController : MonoBehaviour
{
    public bool IsShoted;

    private bool abilityUsed;
    public Rigidbody2D Rigidbody2D;
    public VegetableClass Vegetable;

	void Start ()
	{
	    IsShoted = false;
	    Rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
	}

    public void Shoot(Vector2 velocity)
    {
        if (!IsShoted)
        {
            IsShoted = true;
            Rigidbody2D.velocity = velocity;
        }
    }

    public void CallDestroy()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Vegetable.OnCollision2D(collision);
    }
	
	void Update ()
    {
        Vegetable.CheckVelocity();
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && IsShoted && !abilityUsed)
        {
            Vegetable.UseSpecialAbility();
            abilityUsed = true;
        }
	}
}
