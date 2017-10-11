// Just add this script to your camera. It doesn't need any configuration.

using UnityEngine;

public class TouchCamera : MonoBehaviour
{
    public int yMin = 150;
    public int yMax = 2800;
    public int xMin = 90;
    public int xMax = 3150;
    public int zMin = -3800;
    public int zMax = 715;
    public float pinchRatio = 3;
    public float scrollSpeed = 100;
    Vector2?[] oldTouchPositions = {
        null,
        null
    };
    Vector2 oldTouchVector;
    float oldTouchDistance;

    void Update()
    {

        cameraBoundingArea();//checks camera is in bounds


        if (Input.touchCount == 0)
        { //no touch
            oldTouchPositions[0] = null;
            oldTouchPositions[1] = null;
        }
        else if (Input.touchCount == 1)
        {//1 finger touch, move camera
            if (oldTouchPositions[0] == null || oldTouchPositions[1] != null)
            {
                oldTouchPositions[0] = Input.GetTouch(0).position;
                oldTouchPositions[1] = null;
            }
            else
            {
                Vector2 newTouchPosition = Input.GetTouch(0).position;

                transform.position += transform.TransformDirection((Vector3)((oldTouchPositions[0] - newTouchPosition) * Camera.main.orthographicSize / Camera.main.pixelHeight * scrollSpeed));

                oldTouchPositions[0] = newTouchPosition;
            }
        }
        else
        {// 2 or more touch, zoom in or out
            Touch touch1 = Input.touches[0];
            Touch touch2 = Input.touches[1];

            const float minPinchDistance = 0;

            float pinchDistanceDelta;
            float pinchDistance;

            // ... if at least one of them moved ...
            if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
            {
                // ... check the delta distance between them ...
                pinchDistance = Vector2.Distance(touch1.position, touch2.position);
                float prevDistance = Vector2.Distance(touch1.position - touch1.deltaPosition,
                                                      touch2.position - touch2.deltaPosition);
                pinchDistanceDelta = pinchDistance - prevDistance;

                // ... if it's greater than a minimum threshold, it's a pinch!
                if (Mathf.Abs(pinchDistanceDelta) > minPinchDistance)
                {
                    if ((pinchDistanceDelta > 0 && transform.position.y == yMin) || (pinchDistanceDelta < 0 && transform.position.y == yMax))//stop zoom in/out if at edge of bounding box
                        return;
                    pinchDistanceDelta *= pinchRatio;
                    transform.position += transform.forward * pinchDistanceDelta;
                }
            }
        }
    }

    public void cameraBoundingArea()//ensures camera stays in certain area of map
    {
        if (transform.localPosition.y < yMin)
            transform.localPosition = new Vector3(transform.localPosition.x, yMin, transform.localPosition.z);
        if (transform.localPosition.y > yMax)
            transform.localPosition = new Vector3(transform.localPosition.x, yMax, transform.localPosition.z);
        if (transform.localPosition.x < xMin)
            transform.localPosition = new Vector3(xMin, transform.localPosition.y, transform.localPosition.z);
        else if (transform.localPosition.x > xMax)
            transform.localPosition = new Vector3(xMax, transform.localPosition.y, transform.localPosition.z);
        if (transform.localPosition.z < zMin)
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, zMin);
        else if (transform.localPosition.z > zMax)
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, zMax);
    }

    public void zoomIn(int amnt)
    {
        transform.position += transform.forward * amnt;
    }

    public void zoomOut(int amnt)
    {
        transform.position -= transform.forward * amnt;
    }
}
