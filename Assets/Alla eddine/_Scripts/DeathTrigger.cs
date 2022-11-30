using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Mirror.Authenticators;
using KartGame.Track;
using KartGame.KartSystems;

public class DeathTrigger : NetworkBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        other.GetComponent<Racer>().Respawn();
    }

    public void Respaw(GameObject go)
    {
        go.transform.position = Vector3.zero;
    }
}