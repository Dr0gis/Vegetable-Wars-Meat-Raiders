using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementScript : MonoBehaviour
{
    private const float speed = 0.02f;
    private float levelLength = 50;
    private float levelHeight = 20;
    private float minCameraXPosition;
    private float maxCameraXPosition;
    private float minCameraYPosition;
    private float maxCameraYPosition;
    private float maxCameraSize;
    private float minCameraSize = 4.0f;
    private float cameraSize = 7f;
    private const float zoomSpeed = 0.1f;
    public bool FocusOnVegetable;
    public GameObject VegetableToFocus;
    public bool IsZooming { get; private set; }

void Start()
{
        FocusOnVegetable = false;
        maxCameraSize = Mathf.Min(levelHeight / 2.0f, (levelLength * Screen.height) / (2.0f * Screen.width));
        minCameraSize = 1.5f * GameObject.Find("Catapult").GetComponent<Collider2D>().bounds.size.x * Screen.height /
                        Screen.width;
        Camera.main.orthographicSize = Mathf.Clamp(cameraSize, minCameraSize, maxCameraSize);
        SetCameraPosition();
        MoveToCatapult();
        IsZooming = false;
    }

    void SetCameraPositionBounds()
    {
        var vertExtent = cameraSize;
        var horzExtent = vertExtent * Screen.width / Screen.height;

        minCameraXPosition = horzExtent - levelLength / 2.0f;
        maxCameraXPosition = levelLength / 2.0f - horzExtent;
        minCameraYPosition = vertExtent - levelHeight / 2.0f;
        maxCameraYPosition = levelHeight / 2.0f - vertExtent;
    }

    void SetCameraPosition()
    {
        SetCameraPositionBounds();
        Vector3 tmpPosY = transform.position;
        tmpPosY.y = minCameraYPosition;
        transform.position = tmpPosY;
        Vector3 tmpPosX = transform.position;
        tmpPosX.x = Mathf.Clamp(tmpPosX.x, minCameraXPosition, maxCameraXPosition);
        transform.position = tmpPosX;
    }

    void SetCameraPosition(float XPosition)
    {
        SetCameraPositionBounds();
        Vector3 tmpPosY = transform.position;
        tmpPosY.y = Mathf.Clamp(tmpPosY.y, minCameraYPosition, maxCameraYPosition);
        transform.position = tmpPosY;
        
        Vector3 tmpPosX = transform.position;
        float rightDeltaPosition = XPosition - (tmpPosX.x + (cameraSize * Screen.width / Screen.height) * 4 / 5);
        float leftDeltaPosition = -XPosition + (tmpPosX.x - (cameraSize * Screen.width / Screen.height) * 4 / 5);

        if (rightDeltaPosition > 0)
        {
            tmpPosX.x = Mathf.Min(maxCameraXPosition, tmpPosX.x + rightDeltaPosition);
        }

        if (leftDeltaPosition > 0)
        {
            tmpPosX.x = Mathf.Max(minCameraXPosition, tmpPosX.x - leftDeltaPosition);
        }
        transform.position = tmpPosX;
    }


    void LateUpdate()
    {
        if (FocusOnVegetable)
        {
            SetCameraPosition(VegetableToFocus.transform.position.x);
        }
        if (Input.touchCount == 2)
        {
            IsZooming = true;
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
            cameraSize += deltaMagnitudeDiff * zoomSpeed * Time.deltaTime;

            cameraSize = Mathf.Clamp(cameraSize, minCameraSize, maxCameraSize);
            SetCameraPosition();
            Camera.main.orthographicSize = cameraSize;
        }
        else
        {
            IsZooming = false;
        }
        if (Input.touchCount > 0 && !IsZooming && Input.GetTouch(0).phase == TouchPhase.Moved && !GameObject.Find("Catapult").GetComponent<ShotScript>().IsTakingAimNow)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            transform.Translate(-touchDeltaPosition.x * speed, -touchDeltaPosition.y * speed, 0);
            
            SetCameraPosition();
        }
        SetCameraPosition();
    }

    public void MoveToCatapult()
    {
        Vector3 currentPosition = transform.position;
        currentPosition.x = Mathf.Clamp(GameObject.FindGameObjectWithTag("Catapult").transform.position.x, minCameraXPosition, maxCameraXPosition);
        transform.position = currentPosition;
    }
}
