using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDetection : MonoBehaviour
{
    public Camera maincam;
    public GameEvent onCoinschangedListener;

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
        // One finger controls
        if (Input.touchCount == 1)
        {
            Ray ray;
            RaycastHit hit;
            string touchedObjectName = null;
            
            //Get coins
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                ray = maincam.ScreenPointToRay(Input.touches[0].position);

                if(Physics.Raycast(ray, out hit))
                {
                    touchedObjectName = hit.collider.name;

                    Coins coins = new();

                    if (string.Equals(touchedObjectName, "cw"))
                    {
                        coins.coinsArr[0]++;
                    }
                    if (string.Equals(touchedObjectName, "cbr"))
                    {
                        coins.coinsArr[1]++;
                    }
                    if (string.Equals(touchedObjectName, "cr"))
                    {
                        coins.coinsArr[2]++;
                    }
                    if (string.Equals(touchedObjectName, "cgr"))
                    {
                        coins.coinsArr[3]++;
                    }
                    if (string.Equals(touchedObjectName, "cbl"))
                    {
                        coins.coinsArr[4]++;
                    }
                    if (string.Equals(touchedObjectName, "cgo"))
                    {
                        coins.coinsArr[5]++;
                    }

                    onCoinschangedListener.Raise(coins);
                }
            }

            // Move camera on finger swipe
            if (Input.touches[0].phase == TouchPhase.Moved)
            {
                //Move Camera
                Vector2 distance = Input.touches[0].position - startPos;
                maincam.transform.Translate(Vector3.left * distance.y * Time.deltaTime * moveSpeed, Space.World);
                maincam.transform.Translate(Vector3.forward * distance.x * Time.deltaTime * moveSpeed, Space.World);
            }
            startPos = Input.touches[0].position;
        }

        // Two finger controls
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
