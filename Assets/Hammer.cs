using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KartGame.KartSystems;

public class Hammer : MonoBehaviour
{
    public float Speed = 1f;
    public Transform hammerHead;
    public float force = 1f;

    private void FixedUpdate()
    {
        transform.Rotate(0, 0, Speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("Playerafdwad");
            collision.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            collision.gameObject.GetComponent<Rigidbody>().velocity = transform.forward * force;
        }
    }
}