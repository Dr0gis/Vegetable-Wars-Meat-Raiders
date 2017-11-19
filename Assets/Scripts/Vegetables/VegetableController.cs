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
            Rigidbody2D.simulated = true;
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
        GameOver();
    }

    void GameOver()
    {
        int availableVegetables = -1; //kostil
        foreach (var vegetable in GameObject.Find("Manager").GetComponent<ObjectManagerScript>().AvailableVegetables)
        {
            if (vegetable.CurrentGameObject != null)
            {
                availableVegetables++;
            }
        }
        int availableMeats = 0;
        foreach (var meat in GameObject.Find("Manager").GetComponent<ObjectManagerScript>().AvailableMeats)
        {
            if (!meat.isDead)
            {
                availableMeats++;
            }
        }
        if (availableVegetables == 0 && availableMeats > 0)
        {
            SceneManager.LoadScene("FailLevelEnd", LoadSceneMode.Additive);
            GameObject.Find("PauseButton").SetActive(false);
            for (int i = 1; i < 6; i++)
            {
                GameObject.Find("SelectButton" + i).SetActive(false);
            }
        }
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
