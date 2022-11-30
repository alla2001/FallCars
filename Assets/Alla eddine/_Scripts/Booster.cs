using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Booster : NetworkBehaviour
{
    public float boostTime;

    [Range(1f, 10f)]
    public float boostMul;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isServer)
            other.GetComponent<KartPowerManager>().Boost(boostTime, boostMul);
    }
}