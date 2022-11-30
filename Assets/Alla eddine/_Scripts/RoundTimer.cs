using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Mirror;
using TMPro;

public class RoundTimer : NetworkBehaviour
{
    public float Timer = 15;
    public static RoundTimer instance;
    public UnityAction TimerStart;
    public UnityAction TimerEnd;
    public TextMeshProUGUI text;
    private bool timerRunning = false;
    private float timerTime = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        GameManager.instance.OnRoundStart.AddListener(StartTimer);
    }

    [ClientRpc]
    public void StartTimer()
    {
        timerRunning = true;
        timerTime = Time.time;
        print("[ Start Timer ]");
        TimerStart?.Invoke();
        StartCoroutine(WaitTimer());
    }

    private IEnumerator WaitTimer()
    {
        yield return new WaitForSeconds(Timer);
        print("[ End Timer ]");
        TimerEnd.Invoke();

        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Update()
    {
        if (timerRunning)
            text.text = "Time left :" + (Timer - (int)(Time.time - timerTime)).ToString();
    }
}