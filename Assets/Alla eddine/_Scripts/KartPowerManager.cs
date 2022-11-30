using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Mirror;

public class KartPowerManager : NetworkBehaviour
{
    public enum PowerType
    {
        None, Boosted,
    }

    [System.Serializable]
    public class TriggerEvent : UnityEvent
    { }

    public float power = 1;
    public PowerType currentPower = PowerType.None;
    public TriggerEvent OnBoostStart;
    public TriggerEvent OnBoostEnd;

    [ClientRpc]
    public void Boost(float timer, float boostMul)
    {
        OnBoostStart.Invoke();
        power = boostMul;
        currentPower = PowerType.Boosted;
        StartCoroutine(WaitPower(timer));
    }

    private IEnumerator WaitPower(float timer)
    {
        yield return new WaitForSeconds(timer);
        EndBoost();
    }

    [ClientRpc]
    public void EndBoost()
    {
        currentPower = PowerType.None;
        power = 1;
        OnBoostEnd.Invoke();
    }
}