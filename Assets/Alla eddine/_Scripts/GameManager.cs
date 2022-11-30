using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.Events;

public class GameManager : NetworkBehaviour
{
    [System.Serializable]
    public class TriggerEvent : UnityEvent
    { }

    public TextMeshProUGUI text;

    [SyncVar]
    public bool GameStarted;

    public static GameManager instance;
    private bool startedCountDown;

    [SerializeField]
    public TriggerEvent OnCountDownStart;

    public TriggerEvent OnRoundStart;
    public int NumnberOfPlayers = 5;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    [ClientRpc]
    public void StartGame()
    {
        startedCountDown = true;
        OnCountDownStart?.Invoke();
        StartCoroutine(CountDown());
    }

    private IEnumerator CountDown()
    {
        text.text = "3";
        yield return new WaitForSeconds(1);
        text.text = "2";
        yield return new WaitForSeconds(1);
        text.text = "1";
        yield return new WaitForSeconds(1);
        GameStarted = true;
        text.text = "GO!";
        OnRoundStart?.Invoke();
        yield return new WaitForSeconds(1);
        text.text = "";
    }

    // Update is called once per frame
    private void Update()
    {
        if (NetworkManager.singleton.numPlayers >= NumnberOfPlayers && !startedCountDown)
        {
            StartGame();
        }
    }
}