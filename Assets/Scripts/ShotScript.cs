using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour
{
    private Rigidbody2D catapultRigidbody;

    private Vector2 startPoint;

    private int shotFingerId;

    public bool IsTakingAimNow { get; private set; }

    public ObjectManagerScript vegetableManager;

    public float MaxTensionByX = 300;
    public float MaxTensionByY = 300;
    public float MinTensionByX = 25;
    public float MinTensionByY = 25;
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
        LineRenderer sightLine = GetComponent<LineRenderer>();
        Rigidbody2D rigidbody = CurrentVegetable.GetComponent<Rigidbody2D>();
        Vector2 pos = new Vector2(CurrentVegetable.transform.position.x, CurrentVegetable.transform.position.y);

        float timestep = Time.fixedDeltaTime / Physics2D.velocityIterations;
        Vector2 gravityAccel = Physics2D.gravity * rigidbody.gravityScale * timestep * timestep;
        float drag = 1f - timestep * rigidbody.drag;
        Vector2 moveStep = pushVector * timestep;

        sightLine.positionCount = PredictionStepsNumber;

        sightLine.SetPosition(0, pos);
        for (int i = 1; i < PredictionStepsNumber; ++i)
        {
            moveStep += gravityAccel;
            moveStep *= drag;
            pos += moveStep;

            sightLine.SetPosition(i, pos);
        }
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
    }
}
