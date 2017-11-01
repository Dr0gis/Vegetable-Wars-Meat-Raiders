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

    private void MakeShot(Vector2 pushVector)
    {
        pushVector.x = Mathf.Max(Mathf.Min(pushVector.x, MaxTensionByX), -MaxTensionByX);
        pushVector.y = Mathf.Max(Mathf.Min(pushVector.y, MaxTensionByY), -MaxTensionByY);

        CurrentVegetable.GetComponent<Rigidbody2D>().velocity = pushVector * StrengthScale;

        // Change vegetable here
    }

    private void TakeAim()
    {
        if (Input.touchCount > 0 && !Camera.main.GetComponent<CameraMovementScript>().IsZooming)
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
                    if (shotTouch.Value.phase == TouchPhase.Moved)
                    {
                        // Probably visualization here
                    }
                    else if (shotTouch.Value.phase == TouchPhase.Ended)
                    {
                        MakeShot(startPoint - shotTouch.Value.position);
                        IsTakingAimNow = false;
                    }
                }
            }
        }
    }

    void Start()
    {
        catapultRigidbody = GetComponent<Rigidbody2D>();
        IsTakingAimNow = false;
    }

    void Update()
    {
        TakeAim();
    }
}
