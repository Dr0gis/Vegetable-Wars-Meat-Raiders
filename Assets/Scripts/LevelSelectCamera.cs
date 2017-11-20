using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectCamera : MonoBehaviour {

    private const float speed = 0.02f;
    private float maxHeight;
    private float maxWidth;

    public Image Background;
    void Start ()
    {
        maxHeight = Background.GetComponent<RectTransform>().localToWorldMatrix.m13 * 2;
        maxWidth = Background.GetComponent<RectTransform>().localToWorldMatrix.m03 * 2;
    }

    void LateUpdate()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            transform.Translate(-touchDeltaPosition.x * speed, -touchDeltaPosition.y * speed, 0);
            ClampPositions();
        }
    }

    void ClampPositions()
    {

        Vector3 tmpPosY = transform.position;
        tmpPosY.y = Mathf.Clamp(tmpPosY.y, 0, maxHeight);
        transform.position = tmpPosY;

        Vector3 tmpPosX = transform.position;
        tmpPosX.x = Mathf.Clamp(tmpPosX.x, 0, maxWidth);
        transform.position = tmpPosX;
    }

}


