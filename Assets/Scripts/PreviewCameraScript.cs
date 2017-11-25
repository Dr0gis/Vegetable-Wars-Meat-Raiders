using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviewCameraScript : MonoBehaviour {

    private const float speed = 0.02f;
    //private float levelLength = 50;
    //private float levelHeight = 20;
    private float minCameraXPosition;
    private float maxCameraXPosition;
    private float minCameraYPosition;
    private float maxCameraYPosition;
    private float maxCameraSize;
    private float minCameraSize = 4.0f;
    private const float zoomSpeed = 0.3f;
    public Image Background;


    public float CameraSize { get; private set; }

    void Start()
    {
        GameObject.Find("Manager").GetComponent<PreviewObjectManager>().CurrentLevel = CurrentLevelSelected.NumberLevel;
        Camera.main.transform.position = CurrentLevelSelected.CameraPosition;
        float levelWidth = GameObject.Find("Right Wall").transform.position.x -
                          GameObject.Find("Left Wall").transform.position.x;
        float levelHeight = GameObject.Find("Sky").transform.position.y -
                           GameObject.Find("Floor").transform.position.y;
        CameraSize = CurrentLevelSelected.CameraScale;
        maxCameraSize = Mathf.Min(levelHeight / 2.0f, (levelWidth * Screen.height) / (2.0f * Screen.width));
        minCameraSize = 1.5f * GameObject.Find("Catapult").GetComponent<Collider2D>().bounds.size.x * Screen.height /
                        Screen.width;
        Camera.main.orthographicSize = Mathf.Clamp(CameraSize, minCameraSize, maxCameraSize);
        SetCameraPosition();
    }

    void SetCameraPositionBounds()
    {
        var vertExtent = CameraSize;
        var horzExtent = vertExtent * Screen.width / Screen.height;

        /*minCameraXPosition = horzExtent - levelLength / 2.0f;
        maxCameraXPosition = levelLength / 2.0f - horzExtent;
        minCameraYPosition = vertExtent - levelHeight / 2.0f;
        maxCameraYPosition = levelHeight / 2.0f - vertExtent;*/
        minCameraXPosition = GameObject.Find("Left Wall").transform.position.x + horzExtent;
        maxCameraXPosition = GameObject.Find("Right Wall").transform.position.x - horzExtent;
        maxCameraYPosition = GameObject.Find("Sky").transform.position.y - vertExtent;
        minCameraYPosition = GameObject.Find("Floor").transform.position.y + vertExtent;
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
            CameraSize += deltaMagnitudeDiff * zoomSpeed * Time.deltaTime;

            CameraSize = Mathf.Clamp(CameraSize, minCameraSize, maxCameraSize);
            SetCameraPosition();
            Camera.main.orthographicSize = CameraSize;
            print(CameraSize);
            print(minCameraSize);
            print(maxCameraSize);
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            transform.Translate(-touchDeltaPosition.x * speed, -touchDeltaPosition.y * speed, 0);
            SetCameraPosition();
        }
        SetCameraPosition();
        CurrentLevelSelected.CameraPosition = Camera.main.transform.position;
        CurrentLevelSelected.CameraScale = CameraSize;
    }
}
