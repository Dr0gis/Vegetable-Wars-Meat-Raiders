using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour
{
    private Rigidbody2D catapultRigidbody;

    private Vector2 startPoint;
    private Vector2? previousSHotVector = null;

    private int shotFingerId;

    public bool IsTakingAimNow;

    public ObjectManagerScript vegetableManager;

    public float MaxTensionByX = 4.5f;
    public float MaxTensionByY = 4.5f;
    public float MinTensionByX = 0.5f;
    public float MinTensionByY = 0.5f;
    public float StrengthScale = 5;

    public GameObject CurrentVegetable;

    public int PredictionStepsNumber = 250;

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
        LineRenderer trajectoryLine = GetComponent<LineRenderer>();
        Rigidbody2D vegetableRigitbody = CurrentVegetable.GetComponent<Rigidbody2D>();

        float timestep = Time.fixedDeltaTime / Physics2D.velocityIterations;

        Vector3 gravityAccel = Physics2D.gravity * PhysicsConstants.StandartGravityScale * timestep * timestep * vegetableRigitbody.mass * vegetableRigitbody.mass;
        float drag = 1f - timestep * vegetableRigitbody.drag;
        Vector3 moveStep = pushVector * timestep;

        Vector3 currentPosition = new Vector3(CurrentVegetable.transform.position.x, CurrentVegetable.transform.position.y, -1);

        trajectoryLine.positionCount = PredictionStepsNumber;
        trajectoryLine.SetPosition(0, currentPosition);

        for (int i = 1; i < PredictionStepsNumber; ++i)
        {
            moveStep += gravityAccel;
            moveStep *= drag;
            currentPosition += moveStep;

            if (OverlappedByCollider(currentPosition))
            {
                trajectoryLine.positionCount = i;
                break;
            }

            trajectoryLine.SetPosition(i, currentPosition);
        }
    }

    private GameObject GetNextDefaultVegetable()
    {
        return CurrentVegetable;
    }

    private Vector2 ClampVector(Vector2 vector, float minX, float maxX, float minY, float maxY)
    {
        Vector2 newVector = vector;
        newVector.x = Mathf.Clamp(newVector.x, minX, maxX);
        newVector.y = Mathf.Clamp(newVector.y, minY, maxY);
        return newVector;
    }

    private bool OverlappedByCollider(Vector2 point)
    {
        return Physics2D.OverlapPoint(point) != null && Physics2D.OverlapPoint(point) != CurrentVegetable.GetComponent<Collider2D>();
    }

    private bool areGoingToIntersect(Collider2D constant, PolygonCollider2D moving, Vector2 movement, Vector2 scale)
    {
        foreach (var point in moving.points)
        {
            if (constant.OverlapPoint(new Vector2(point.x * scale.x, point.y * scale.y) + movement))
            {
                return true;
            }
        }
        return false;
    }

    private Vector2 PrepareVector(Vector2 vectorToPrepare)
    {
        Vector2 pushVector = vectorToPrepare;
        pushVector *= StrengthScale;

        pushVector = ClampVector(pushVector, -MaxTensionByX * StrengthScale, MaxTensionByX * StrengthScale,
                                             -MaxTensionByY * StrengthScale, MaxTensionByY * StrengthScale);

        return pushVector;
    }

    private void MakeShot(Vector2 pushVector)
    {
        GetComponent<AudioSource>().Play();
        GetComponent<LineRenderer>().positionCount = 0;
        CurrentVegetable.GetComponent<VegetableController>().Shoot(pushVector);
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
                    shotFingerId = initialTouch.Value.fingerId;
                }
            }
            else
            {
                Touch? shotTouch = GetShotTouch();
                if (shotTouch.HasValue)
                {
                    shotVector = previousSHotVector;
                    if (shotTouch.Value.phase == TouchPhase.Moved)
                    {
                        Vector2 moveToPoint = ClampVector(Camera.main.ScreenToWorldPoint(shotTouch.Value.position),
                            startPoint.x + -MaxTensionByX, startPoint.x + MaxTensionByX, startPoint.y + -MaxTensionByY, startPoint.y + MaxTensionByY);

                        Vector2 movement = CurrentVegetable.GetComponent<Rigidbody2D>().position - moveToPoint;

                        if (!areGoingToIntersect(CurrentVegetable.GetComponent<Collider2D>(), 
                                gameObject.GetComponentInChildren<PolygonCollider2D>(), movement + (Vector2)transform.Find("CatapultBack").transform.position,
                                transform.Find("CatapultBack").transform.lossyScale) &&
                            !OverlappedByCollider(moveToPoint))
                        {
                            CurrentVegetable.GetComponent<Rigidbody2D>().position = moveToPoint;

                            shotVector = startPoint - (Vector2)Camera.main.ScreenToWorldPoint(shotTouch.Value.position);
                            previousSHotVector = shotVector;
                        }
                        isShotMade = false;
                    }
                    else if (shotTouch.Value.phase == TouchPhase.Ended)
                    {
                        shotVector = startPoint - (Vector2)Camera.main.ScreenToWorldPoint(shotTouch.Value.position);

                        isShotMade = true;
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
        vegetableManager = GameObject.Find("Manager").GetComponent<ObjectManagerScript>();
        vegetableManager.Initiate();
        vegetableManager.SetNextVagetable();
        GetComponent<LineRenderer>().startWidth = 0.2f;
        GetComponent<LineRenderer>().endWidth = 0.05f;

        startPoint = CurrentVegetable.GetComponent<Rigidbody2D>().position;
    }

    void Update()
    {
        Vector2? shotVector;
        bool isShotMade;

        if (CurrentVegetable != null && CurrentVegetable.GetComponent<VegetableController>().IsShoted == false)
        {
            TakeAim(out shotVector, out isShotMade);

            if (shotVector.HasValue && (Mathf.Abs(shotVector.Value.x) > MinTensionByX || Mathf.Abs(shotVector.Value.y) > MinTensionByY))
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
            else
            {
                GetComponent<LineRenderer>().positionCount = 0;
            }
        }
    }
}
