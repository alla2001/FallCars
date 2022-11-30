using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostThrusterRandomRotation : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.Rotate(Random.Range(0, 180f), 0, 0);
    }
}