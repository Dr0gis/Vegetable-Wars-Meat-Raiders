using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour
{
    private Rigidbody2D catapultRigidbody;

    private PolygonCollider2D calfTrigger;

    private LineRenderer leftElastic;
    private LineRenderer rightElastic;
    private LineRenderer trajectoryLine;
    private LineRenderer leftLowerBlack;
    private LineRenderer leftUpperBlack;
    private LineRenderer rightLowerBlack;
    private LineRenderer rightUpperBlack;

    private Vector2 startPoint;
    private Vector2? previousSHotVector = null;

    private int shotFingerId;

    private bool isCalfStopped;

    public bool IsTakingAimNow;

    public ObjectManagerScript vegetableManager;

    public float MaxTensionByX = 4.5f;
    public float MaxTensionByY = 4.5f;
    public float MinTensionByX = 0.5f;
    public float MinTensionByY = 0.5f;
    public float StrengthScale = 5;

    public GameObject CurrentVegetable;

    public int PredictionStepsNumber = 60;

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
        Rigidbody2D vegetableRigitbody = CurrentVegetable.GetComponent<Rigidbody2D>();

        float timestep = Time.fixedDeltaTime / Physics2D.velocityIterations;

        Vector3 gravityAccel = Physics2D.gravity * PhysicsConstants.StandartGravityScale * timestep * timestep * vegetableRigitbody.mass * vegetableRigitbody.mass;
        float drag = 1f - timestep * vegetableRigitbody.drag;
        Vector3 moveStep = pushVector * timestep;

        Vector3 currentPosition = new Vector3(CurrentVegetable.transform.position.x, CurrentVegetable.transform.position.y, 0.5f);

        trajectoryLine.positionCount = PredictionStepsNumber;
        trajectoryLine.SetPosition(0, currentPosition);

        for (int i = 1; i < PredictionStepsNumber; ++i)
        {
            for (int j = 0; j < 5; ++j)
            {
                moveStep += gravityAccel;
                moveStep *= drag;
                currentPosition += moveStep;
            }

            if (OverlappedByCollider(currentPosition))
            {
                trajectoryLine.positionCount = i;
                break;
            }

            trajectoryLine.SetPosition(i, currentPosition);
        }
    }

    public void MoveCalf(bool moveAnyway = false)
    {
        moveCalf(new Vector2(CurrentVegetable.GetComponent<Collider2D>().bounds.min.x + 0.25f,
                    (CurrentVegetable.transform.position.y + CurrentVegetable.GetComponent<Collider2D>().bounds.min.y) / 2), moveAnyway);
    }

    private void moveCalf(Vector2 point, bool moveAnyway = false)
    {
        if (!IsTakingAimNow && calfTrigger.OverlapPoint(point))
        {
            isCalfStopped = true;
        }
        if (!isCalfStopped || moveAnyway)
        {
            transform.Find("Calf").transform.position = new Vector3(point.x, point.y, -1.5f);

            leftElastic.SetPosition(0, new Vector3(startPoint.x + 1, startPoint.y - 0.4f, 5f));
            leftElastic.SetPosition(1, new Vector3(point.x, point.y, 5f));

            rightElastic.SetPosition(0, new Vector3(startPoint.x - 0.7f, startPoint.y - 0.4f, -0.3f));
            rightElastic.SetPosition(1, new Vector3(point.x, point.y, -0.3f));


            Vector2 leftLine = new Vector2(startPoint.x + 1 - point.x, startPoint.y - 0.4f - point.y);
            Vector2 rightLine = new Vector2(startPoint.x - 0.7f - point.x, startPoint.y - 0.4f - point.y);

            leftLine.Normalize();
            rightLine.Normalize();
            leftLine *= 0.125f;
            rightLine *= 0.125f;


            leftLowerBlack.SetPosition(0, new Vector3(startPoint.x + 1          + leftLine.y, startPoint.y - 0.4f    - leftLine.x, 4f));
            leftLowerBlack.SetPosition(1, new Vector3(point.x                   + leftLine.y, point.y                - leftLine.x, 4f));

            leftUpperBlack.SetPosition(0, new Vector3(startPoint.x + 1          - leftLine.y, startPoint.y - 0.4f    + leftLine.x, 4f));
            leftUpperBlack.SetPosition(1, new Vector3(point.x                   - leftLine.y, point.y                + leftLine.x, 4f));

            rightLowerBlack.SetPosition(0, new Vector3(startPoint.x - 0.7f      + leftLine.y, startPoint.y - 0.4f     - leftLine.x, -0.4f));
            rightLowerBlack.SetPosition(1, new Vector3(point.x                  + leftLine.y, point.y                 - leftLine.x, -0.4f));

            rightUpperBlack.SetPosition(0, new Vector3(startPoint.x - 0.7f      - leftLine.y, startPoint.y - 0.4f     + leftLine.x, -0.4f));
            rightUpperBlack.SetPosition(1, new Vector3(point.x                  - leftLine.y, point.y                 + leftLine.x, -0.4f));
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
        GetComponent<SoundManagerComponent>().PlaySound("ShotSound1");
        GetComponent<LineRenderer>().positionCount = 0;
        CurrentVegetable.GetComponent<VegetableController>().Shoot(pushVector);
        vegetableManager.DisableShoted();
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
                    isCalfStopped = false;
                }
            }
            else
            {
                Touch? shotTouch = GetShotTouch();
                if (shotTouch.HasValue)
                {
                    Vector2 moveToPoint = ClampVector(Camera.main.ScreenToWorldPoint(shotTouch.Value.position),
                               startPoint.x + -MaxTensionByX, startPoint.x + 0.5f, startPoint.y + -MaxTensionByY, startPoint.y + 0.5f);

                    if (previousSHotVector == null)
                    {
                        previousSHotVector = startPoint - moveToPoint;
                    }
                    shotVector = previousSHotVector;

                    if (shotTouch.Value.phase == TouchPhase.Moved)
                    {
                        Vector2 movement = CurrentVegetable.GetComponent<Rigidbody2D>().position - moveToPoint;

                        if (!areGoingToIntersect(CurrentVegetable.GetComponent<Collider2D>(), 
                                gameObject.GetComponentInChildren<PolygonCollider2D>(), movement + (Vector2)transform.Find("CatapultBack").transform.position,
                                transform.Find("CatapultBack").transform.lossyScale))
                        {
                            CurrentVegetable.transform.position = moveToPoint;

                            MoveCalf();

                            shotVector = startPoint - moveToPoint;
                            previousSHotVector = shotVector;
                        }
                        isShotMade = false;
                    }
                    else if (shotTouch.Value.phase == TouchPhase.Ended)
                    {
                        //shotVector = startPoint - (Vector2)Camera.main.ScreenToWorldPoint(shotTouch.Value.position);

                        isShotMade = true;
                        IsTakingAimNow = false;
                    }
                }
            }
        }
    }

    public void ReturnBack()
    {
        IsTakingAimNow = false;
        CurrentVegetable.transform.position = startPoint;
        MoveCalf(true);
        isCalfStopped = true;
        previousSHotVector = null;
        trajectoryLine.positionCount = 0;
    }

    void Start()
    {
        catapultRigidbody = GetComponent<Rigidbody2D>();
        IsTakingAimNow = false;
        vegetableManager = GameObject.Find("Manager").GetComponent<ObjectManagerScript>();
        vegetableManager.SetNextVagetable();
        GetComponent<LineRenderer>().startWidth = 0.2f;
        GetComponent<LineRenderer>().endWidth = 0.05f;

        calfTrigger = transform.Find("CalfTrigger").GetComponent<PolygonCollider2D>();
        startPoint = CurrentVegetable.GetComponent<Rigidbody2D>().position;

        trajectoryLine = GetComponent<LineRenderer>();
        leftElastic = transform.Find("LeftElastic").GetComponent<LineRenderer>();
        rightElastic = transform.Find("RightElastic").GetComponent<LineRenderer>();

        leftLowerBlack = transform.Find("LeftLowerBlack").GetComponent<LineRenderer>();
        leftUpperBlack = transform.Find("LeftUpperBlack").GetComponent<LineRenderer>();

        rightLowerBlack = transform.Find("RightLowerBlack").GetComponent<LineRenderer>();
        rightUpperBlack = transform.Find("RightUpperBlack").GetComponent<LineRenderer>();

        leftElastic.positionCount = 2;
        rightElastic.positionCount = 2;

        leftLowerBlack.positionCount = 2;
        leftUpperBlack.positionCount = 2;

        rightLowerBlack.positionCount = 2;
        rightUpperBlack.positionCount = 2;

        MoveCalf(true);
    }

    void Update()
    {
        Vector2? shotVector;
        bool isShotMade;

        if (CurrentVegetable != null)
        {
            MoveCalf();
            if (CurrentVegetable.GetComponent<VegetableController>().IsShoted == false)
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
}
