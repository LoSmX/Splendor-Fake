using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Camera maincam;
    public Player p1;
    public Player p2;
    public Player currentPlayer;

    public float zoomSpeed = 100;
    public float moveSpeed = 0.1f;
    public float rotSpeed = 0.01f;

    private Vector2 startPos;
    private float lastDistance;
    private GameObject clickedObject;

    // Switch current player
    public void onEndTurnEvent(object data)
    {
        if((Player)data == p1)
        {
            currentPlayer = p2;
        }
        else
        {
            currentPlayer = p1;
        }
    }

    // When something is pressed call the on click event of the current player
    private void callOnClickEvent(Player currentPlayer, RaycastHit hit)
    {
        
        if (hit.collider.gameObject.TryGetComponent<Nobel>(out Nobel nobel))
        {
            currentPlayer.onClickEvent(nobel);
        }else if ( hit.collider.gameObject.TryGetComponent<Card>(out Card card))
        {
            currentPlayer.onClickEvent(card);
        }

        if (hit.collider.gameObject.TryGetComponent<Chip>(out Chip chip))
        {
            currentPlayer.onClickEvent(chip);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // One finger controls
        if (Input.touchCount == 1)
        {
            Ray ray;
            RaycastHit hit;

            //Get touched object
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                ray = maincam.ScreenPointToRay(Input.touches[0].position);
                if (Physics.Raycast(ray, out hit))
                {
                    clickedObject = hit.collider.gameObject;
                }
            }

            // Move camera on finger swipe
            if (Input.touches[0].phase == TouchPhase.Moved)
            {
                //Check if moved away from object
                ray = maincam.ScreenPointToRay(Input.touches[0].position);
                if (Physics.Raycast(ray, out hit))
                {
                    // if moved away from object set clicked object to none
                    if (clickedObject != hit.collider.gameObject)
                    {
                        clickedObject = null;
                    }
                }

                //Move Camera
                Vector2 distance = Input.touches[0].position - startPos;
                maincam.transform.Translate(Vector3.left * distance.y * Time.deltaTime * moveSpeed, Space.World);
                maincam.transform.Translate(Vector3.forward * distance.x * Time.deltaTime * moveSpeed, Space.World);
            }

            if (Input.touches[0].phase == TouchPhase.Ended)
            {
                ray = maincam.ScreenPointToRay(Input.touches[0].position);
                if (Physics.Raycast(ray, out hit))
                {
                    // if object same as last clicked object call Click event
                    if (clickedObject == hit.collider.gameObject)
                    {
                        callOnClickEvent(currentPlayer, hit);
                    }
                }
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
