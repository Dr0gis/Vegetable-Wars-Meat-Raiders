using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour
{
    private Rigidbody2D catapultRigidbody;

    private Vector2 startPoint;

    public bool IsTakingAimNow { get; private set; }

    private int shotFingerId;

    public float MaxTensionByX = 200;
    public float MaxTensionByY = 200;
    public float StrengthScale;

    public GameObject CurrentVegetable;

    private Touch? GetShotTouch()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.fingerId == shotFingerId)
            {
                return touch;
            }
        }
        return null;
    }

    private Touch? GetInitialTouch()
    {
        foreach (Touch touch in Input.touches)
        {
            if (catapultRigidbody.OverlapPoint(Camera.main.ScreenToWorldPoint(touch.position)) 
                && touch.phase == TouchPhase.Began)
            {
                return touch;
            }
        }
        return null;
    }

    private void Visualize(Vector2 pushVector)
    {
        // Visualize heare
    }

    private GameObject GetNextDefaultVegetable()
    {
        return CurrentVegetable;
    }

    private Vector2 PrepareVector(Vector2 vectorToPrepare)
    {
        Vector2 pushVector = vectorToPrepare;
        pushVector.x = Mathf.Max(Mathf.Min(pushVector.x, MaxTensionByX), -MaxTensionByX);
        pushVector.y = Mathf.Max(Mathf.Min(pushVector.y, MaxTensionByY), -MaxTensionByY);
        pushVector *= StrengthScale;
        return pushVector;
    }

    private void MakeShot(Vector2 pushVector)
    {
        CurrentVegetable.GetComponent<Rigidbody2D>().velocity = pushVector;
    }

    private void TakeAim(out Vector2? shotVector, out bool isShotMade)
    {
        isShotMade = false;
        shotVector = null;

        if (Input.touchCount > 0 && !Camera.main.GetComponent<CameraMovementScript>().IsZooming && Time.timeScale != 0)
        {
            if (!IsTakingAimNow)
            {
                Touch? initialTouch = GetInitialTouch();
                if (initialTouch.HasValue)
                {
                    IsTakingAimNow = true;
                    startPoint = initialTouch.Value.position;
                    shotFingerId = initialTouch.Value.fingerId;
                }
            }
            else
            {
                Touch? shotTouch = GetShotTouch();
                if (shotTouch.HasValue)
                {
                    shotVector = startPoint - shotTouch.Value.position;
                    if (shotTouch.Value.phase == TouchPhase.Moved)
                    {
                        isShotMade = false;
                    }
                    else if (shotTouch.Value.phase == TouchPhase.Ended)
                    {
                        isShotMade = true;
                        IsTakingAimNow = false;
                    }
                }
            }
        }
    }

    public 

    void Start()
    {
        catapultRigidbody = GetComponent<Rigidbody2D>();
        IsTakingAimNow = false;
    }

    void Update()
    {
        Vector2? shotVector;
        bool isShotMade;

        TakeAim(out shotVector, out isShotMade);

        if (shotVector.HasValue)
        {
            Vector2 pushVector = PrepareVector(shotVector.Value);
            if (isShotMade == false)
            {
                Visualize(pushVector);
            }
            else if (isShotMade == true)
            {
                MakeShot(pushVector);
                CurrentVegetable = GetNextDefaultVegetable();
            }
        }

    }
}
