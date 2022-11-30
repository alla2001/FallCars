using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class WinManager : NetworkBehaviour
{
    public List<int> winnersIDs = new List<int>();
    public string nextLevel;

    private void Start()
    {
        RoundTimer.instance.TimerEnd += OnRoundTimerEnd;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            winnersIDs.Add(other.GetComponent<NetworkIdentity>().connectionToClient.connectionId);
            if (other.gameObject.GetComponentInChildren<Camera>())
                other.gameObject.GetComponentInChildren<Camera>().enabled = false;
            CameraLocker.spectateCameras.Remove(other.gameObject.GetComponentInChildren<Camera>());
            NetworkServer.Destroy(other.gameObject);

            if (CameraLocker.spectateCameras.Count > 0)
                CameraLocker.spectateCameras[0].transform.parent.gameObject.SetActive(true);
            if (winnersIDs.Count >= NetworkManager.singleton.numPlayers + 1)
            {
                OnRoundTimerEnd();
            }
        }
    }

    public void OnRoundTimerEnd()
    {
        Dictionary<int, NetworkConnectionToClient> copyDic = new Dictionary<int, NetworkConnectionToClient>(NetworkServer.connections);
        foreach (KeyValuePair<int, NetworkConnectionToClient> connection in copyDic)
        {
            if (!winnersIDs.Contains(connection.Key))
            {
                connection.Value.Disconnect();
            }
        }
        NetworkManager.singleton.ServerChangeScene(nextLevel);
    }
}