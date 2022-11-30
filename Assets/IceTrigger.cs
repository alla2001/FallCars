using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KartGame.KartSystems;

public class IceTrigger : MonoBehaviour
{
    public AddativeKartModifier mod;

    private void OnCollisionEnter(Collision collision)
    {
        print("Slidning");
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<KartMovement>().AddKartModifier(mod);
            print("Slidning");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<KartMovement>().RemoveKartModifier(mod);
            print("Not Slidning");
        }
    }
}