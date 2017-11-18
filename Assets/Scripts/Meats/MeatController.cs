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
        GameOver();
    }

    void GameOver()
    {
        int availableMeats = -1; // because Destroy() works later
        foreach (var meat in GameObject.Find("Manager").GetComponent<ObjectManagerScript>().AvailableMeats)
        {
            if (meat.CurrentGameObject != null)
            {
                availableMeats++;
            }
        }
        if (availableMeats == 0)
        {
            SceneManager.LoadScene("SuccesLevelEnd", LoadSceneMode.Additive);
            GameObject.Find("PauseButton").SetActive(false);
            for (int i = 1; i < 6; i++)
            {
                GameObject.Find("SelectButton" + i).SetActive(false);
            }
        }
    }
}
