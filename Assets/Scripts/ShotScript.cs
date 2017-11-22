using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour
{
    private Rigidbody2D catapultRigidbody;

    private CameraMovementScript mainCameraMovementScript;
    private float initialCameraSize;

    private Vector2 startPoint;

    private int shotFingerId;

    public bool IsTakingAimNow;

    public ObjectManagerScript vegetableManager;

    public float MaxTensionByX = 300;
    public float MaxTensionByY = 300;
    public float MinTensionByX = 55;
    public float MinTensionByY = 55;
    public float StrengthScale = 0.05f;

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

        Vector3 gravityAccel = Physics2D.gravity * vegetableRigitbody.gravityScale * timestep * timestep * vegetableRigitbody.mass * vegetableRigitbody.mass;
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

            if (Physics2D.OverlapPoint(currentPosition) != null &&
                Physics2D.OverlapPoint(currentPosition) != CurrentVegetable.GetComponent<Collider2D>())
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

    private Vector2 PrepareVector(Vector2 vectorToPrepare)
    {
        Vector2 pushVector = vectorToPrepare;
        pushVector *= StrengthScale * mainCameraMovementScript.CameraSize / initialCameraSize;

        pushVector.x = Mathf.Max(Mathf.Min(pushVector.x, MaxTensionByX * StrengthScale), -MaxTensionByX * StrengthScale);
        pushVector.y = Mathf.Max(Mathf.Min(pushVector.y, MaxTensionByY * StrengthScale), -MaxTensionByY * StrengthScale);

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

    void Start()
    {
        catapultRigidbody = GetComponent<Rigidbody2D>();
        IsTakingAimNow = false;
        vegetableManager = GameObject.Find("Manager").GetComponent<ObjectManagerScript>();
        vegetableManager.Initiate();
        vegetableManager.SetNextVagetable();
        GetComponent<LineRenderer>().startWidth = 0.2f;
        GetComponent<LineRenderer>().endWidth = 0.05f;

        mainCameraMovementScript = GameObject.Find("Main Camera").GetComponent<CameraMovementScript>();
        initialCameraSize = mainCameraMovementScript.CameraSize;
    }

    void Update()
    {
        Vector2? shotVector;
        bool isShotMade;

        TakeAim(out shotVector, out isShotMade);

        if (CurrentVegetable != null &&
            CurrentVegetable.GetComponent<VegetableController>().IsShoted == false &&
            shotVector.HasValue && 
            (Mathf.Abs(shotVector.Value.x) > MinTensionByX || Mathf.Abs(shotVector.Value.y) > MinTensionByY))
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
