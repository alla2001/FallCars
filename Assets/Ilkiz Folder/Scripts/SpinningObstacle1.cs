using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningObstacle1 : MonoBehaviour
{
    public float rotationSpeed;
    public float force = 25;

    private void Start()
    {
        rotationSpeed = 60;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Rotate(new Vector3(0, rotationSpeed, 0) * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            collision.gameObject.GetComponent<Rigidbody>().velocity = (transform.position - collision.transform.position).normalized * force;
        }
    }
}