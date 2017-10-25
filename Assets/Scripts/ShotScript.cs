using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour
{
    private Rigidbody2D catapultRigitbody;

    private Vector2 startPoint;

    private bool isTakingAimNow;
    private int shotFingerId;

    public float MaxEndX;
    public float MaxEndY;
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
            if (catapultRigitbody.OverlapPoint(Camera.main.ScreenToWorldPoint(touch.position)) 
                && touch.phase == TouchPhase.Began)
            {
                return touch;
            }
        }
        return null;
    }

    private void MakeShot(Vector2 pushVector)
    {
        pushVector.x = Mathf.Max(Mathf.Min(pushVector.x, MaxEndX), -MaxEndX);
        pushVector.y = Mathf.Max(Mathf.Min(pushVector.y, MaxEndY), -MaxEndX);

        CurrentVegetable.GetComponent<Rigidbody2D>().velocity = pushVector * StrengthScale;

        // Change vegetable here
    }

    private void TakeAim()
    {
        if (Input.touchCount > 0)
        {
            if (!isTakingAimNow)
            {
                Touch? initialTouch = GetInitialTouch();
                if (initialTouch.HasValue)
                {
                    isTakingAimNow = true;
                    startPoint = initialTouch.Value.position;
                    shotFingerId = initialTouch.Value.fingerId;
                }
            }
            else
            {
                Touch? shotTouch = GetShotTouch();
                if (shotTouch.HasValue)
                {
                    if (shotTouch.Value.phase == TouchPhase.Moved)
                    {
                        // Probably visualization here
                    }
                    else if (shotTouch.Value.phase == TouchPhase.Ended)
                    {
                        MakeShot(startPoint - shotTouch.Value.position);
                        isTakingAimNow = false;
                    }
                    else
                    {
                        // throw new Exeption
                    }
                }
                else
                {
                    // throw new Exeption
                }
            }
        }
    }

    void Start()
    {
        catapultRigitbody = GetComponent<Rigidbody2D>();
        isTakingAimNow = false;
    }

    void Update()
    {
        TakeAim();
    }
}
