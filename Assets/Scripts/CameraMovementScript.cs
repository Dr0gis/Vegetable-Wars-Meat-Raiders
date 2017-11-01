using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementScript : MonoBehaviour
{
    private const float Speed = 0.02f;
    private float LevelLength = 50;
    private float LevelHeight = 20;
    private float MinCameraXPosition;
    private float MaxCameraXPosition;
    private float MinCameraYPosition;
    private float MaxCameraYPosition;
    private float MaxCameraSize;
    private float MinCameraSize = 4.0f;
    private float CameraSize = 7f;
    private const float ZoomSpeed = 0.02f;

    void Start()
    {
        MaxCameraSize = Mathf.Min(LevelHeight / 2.0f, (LevelLength * Screen.height) / (2.0f * Screen.width));
        SetCameraToBottom();
    }

    void SetCameraPositionBounds()
    {
        var vertExtent = CameraSize;
        var horzExtent = vertExtent * Screen.width / Screen.height;

        MinCameraXPosition = horzExtent - LevelLength / 2.0f;
        MaxCameraXPosition = LevelLength / 2.0f - horzExtent;
        MinCameraYPosition = vertExtent - LevelHeight / 2.0f;
        MaxCameraYPosition = LevelHeight / 2.0f - vertExtent;
    }

    void SetCameraToBottom()
    {
        SetCameraPositionBounds();
        Vector3 tmpPosY = transform.position;
        tmpPosY.y = MinCameraYPosition;
        transform.position = tmpPosY;
    }


    void LateUpdate()
    {
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
            CameraSize += deltaMagnitudeDiff * ZoomSpeed * Time.deltaTime;

            CameraSize = Mathf.Clamp(CameraSize, MinCameraSize, MaxCameraSize);
            SetCameraToBottom();
            Camera.main.orthographicSize = CameraSize;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            transform.Translate(-touchDeltaPosition.x * Speed, -touchDeltaPosition.y * Speed, 0);

            Vector3 tmpPosX = transform.position;
            tmpPosX.x = Mathf.Clamp(tmpPosX.x, MinCameraXPosition, MaxCameraXPosition);
            transform.position = tmpPosX;

            Vector3 tmpPosY = transform.position;
            tmpPosY.y = MinCameraYPosition;
            transform.position = tmpPosY;
        }
    }
}