using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderBehaviour : MonoBehaviour
{
    public float movementDistance = 10.0f;
    public float movementAmount = 1.0f;
    Vector3 originalPosition;
    bool isMovingForward = true;
    float tempX;
    float boundaryForward;
    float boundaryBackward;
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        boundaryForward = originalPosition.x + movementDistance;
        boundaryBackward = originalPosition.x - movementDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMovingForward)
        {
            if (transform.position.x < boundaryForward)
            {
                tempX =  movementAmount * Time.deltaTime;
            }
            else
            {
                isMovingForward = false;
            }
        }
        else
        {
            if (transform.position.x > boundaryBackward)
            {
                tempX = -movementAmount * Time.deltaTime;
            }
            else
            {
                isMovingForward = true;
            }
        }
        transform.position += new Vector3(tempX, 0, 0);

    }
}
