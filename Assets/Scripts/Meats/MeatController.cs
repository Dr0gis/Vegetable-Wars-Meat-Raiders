using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        GameObject.Find("Manager").GetComponent<EndOfLevelCheck>().LeftMeats--;
        print(GameObject.Find("Manager").GetComponent<EndOfLevelCheck>().LeftMeats);
        //GameOver();
    }
    private void playEndGameSound()
    {
        GameObject.Find("Manager").GetComponent<SoundManagerComponent>().PlaySound("LevelEndSound");
    }
    void GameOver()
    {
        int availableMeats = 0; 
        foreach (var meat in GameObject.Find("Manager").GetComponent<ObjectManagerScript>().AvailableMeats)
        {
            if (!meat.IsDead)
            {
                availableMeats++;
            }
        }
        if (availableMeats == 0)
        {
            playEndGameSound();
            SceneManager.LoadScene("SuccesLevelEnd", LoadSceneMode.Additive);
            GameObject.Find("PauseButton").SetActive(false);
            foreach (var button in GameObject.Find("Manager").GetComponent<ObjectManagerScript>().VegetableButtons)
            {
                button.SetActive(false);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            Meat.OnDestroy();
        }
    }
}
