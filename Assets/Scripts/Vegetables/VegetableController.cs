using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            Camera.main.GetComponent<CameraMovementScript>().VegetableToFocus = gameObject;
            Camera.main.GetComponent<CameraMovementScript>().FocusOnVegetable = true;
            IsShoted = true;
            Vegetable.IsShoted = true;
            Rigidbody2D.gravityScale = PhysicsConstants.StandartGravityScale;
            gameObject.GetComponent<Collider2D>().isTrigger = false;
            //gameObject.GetComponent<Collider2D>().enabled = true;
            //Rigidbody2D.velocity = velocity;
            Rigidbody2D.AddForce(velocity, ForceMode2D.Impulse);
        }
    }

    public void CallDestroy()
    {
        if (Camera.main.GetComponent<CameraMovementScript>().VegetableToFocus == gameObject)
        {
            Camera.main.GetComponent<CameraMovementScript>().FocusOnVegetable = false;
        }
        Destroy(gameObject);
        GameObject.Find("Manager").GetComponent<EndOfLevelCheck>().LeftVegetables--;
        print(GameObject.Find("Manager").GetComponent<EndOfLevelCheck>().LeftVegetables);
        //GameOver();
    }

    

    void OnCollisionEnter2D(Collision2D collision)
    {
        Vegetable.OnCollision2D(collision);
        if (!abilityUsed)
        {
            Vegetable.UseSpecialAbility();
            abilityUsed = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            Vegetable.OnDestroyObject();
        }
    }

    void Update ()
    {
        Vegetable.CheckVelocity();
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && IsShoted && !abilityUsed)
        {
            print("tap");
            Vegetable.UseSpecialAbility();
            abilityUsed = true;
        }
	}
}
