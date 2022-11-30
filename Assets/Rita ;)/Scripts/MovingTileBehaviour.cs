using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTileBehaviour : MonoBehaviour
{
    public float movementHeight = 10.0f;
    public float speed = 1.0f;
    public float delay = 0.0f;
    public float movementPause = 5.0f;
    Vector3 originalPos;
    float maxHeightY;
    float minHeightY;
    bool isMovingUp = true;
    bool countedDelay = false;
    float tempY;
    float tempTimer;

    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
        maxHeightY = originalPos.y + movementHeight;
        minHeightY = originalPos.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (!countedDelay)
        {
            if (tempTimer < delay)
            {
                tempTimer += Time.deltaTime;
            }
            else
            {
                countedDelay = true;
            }
        }
        else 
        {
            if (isMovingUp)
            {

                if (transform.position.y < maxHeightY)
                {
                    tempY = speed * Time.deltaTime;
                }
                else
                {
                    isMovingUp = false;
                }
            }
            else
            {
                if (transform.position.y > minHeightY)
                {
                    tempY = -speed * Time.deltaTime;
                }
                else
                {
                    StartCoroutine(TimerCount(movementPause));

                }
            }
            transform.position += new Vector3(0, tempY, 0);
            tempY = 0;
        }
    }
    IEnumerator TimerCount(float time)
    {
        yield return new WaitForSeconds(time);
        isMovingUp = true;
        yield return null;
    }
}
