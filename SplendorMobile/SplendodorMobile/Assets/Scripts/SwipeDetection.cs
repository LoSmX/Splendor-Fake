using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    public Camera maincam;
    public float zoomSpeed = 100;
    public float moveSpeed = 0.1f;
    public float rotSpeed = 0.01f;

    private Vector2 startPos;
    private float lastDistance;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Move camera
        if (Input.touchCount == 1)
        {
            if (Input.touches[0].phase == TouchPhase.Moved)
            {
                Vector2 distance = Input.touches[0].position - startPos;

                maincam.transform.Translate(Vector3.left * distance.y * Time.deltaTime * moveSpeed, Space.World);
                maincam.transform.Translate(Vector3.forward * distance.x * Time.deltaTime * moveSpeed, Space.World);
            }
            startPos = Input.touches[0].position;
        }

        
        if (Input.touchCount == 2)
        {
            
            
            // Zoom camera
            if (Input.touches[0].phase == TouchPhase.Began || Input.touches[1].phase == TouchPhase.Began)
            {
                lastDistance = Vector2.Distance(Input.touches[0].position, Input.touches[1].position);
            }
            if (Input.touches[0].phase == TouchPhase.Moved || Input.touches[1].phase == TouchPhase.Moved)
            {
                float distance = Vector2.Distance(Input.touches[0].position, Input.touches[1].position);
                if(distance > lastDistance)
                {
                    maincam.transform.Translate(Vector3.forward * (distance - lastDistance) * Time.deltaTime * zoomSpeed, Space.Self);
                }
                else if (distance < lastDistance)
                {
                    maincam.transform.Translate(Vector3.back * (lastDistance - distance) * Time.deltaTime * zoomSpeed, Space.Self);
                }
                lastDistance = distance;
            }
            if (Input.touches[0].phase == TouchPhase.Ended)
            {
                startPos = Input.touches[1].position;
            }
            if (Input.touches[1].phase == TouchPhase.Ended)
            {
                startPos = Input.touches[0].position;
            }

            // Tilt camera
            if (Input.touches[0].phase == TouchPhase.Moved && Input.touches[1].phase == TouchPhase.Moved)
            {
                if(Input.touches[0].deltaPosition.y < 0 || Input.touches[0].deltaPosition.y < 0)
                {
                    maincam.transform.RotateAround(new Vector3(0, 1, 0), new  Vector3(0, 0, -1), Input.touches[0].deltaPosition.y * rotSpeed);
                }

                if (Input.touches[0].deltaPosition.y > 0 || Input.touches[0].deltaPosition.y > 0)
                {
                    maincam.transform.RotateAround(new Vector3(0, 1, 0), new Vector3(0, 0, -1), Input.touches[0].deltaPosition.y * rotSpeed);
                }
            }

        }
    }
}
