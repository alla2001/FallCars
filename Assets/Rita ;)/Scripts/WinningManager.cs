using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class WinningManager : NetworkBehaviour
{
    public List<int> winnersIDs = new List<int>();

    public List<Scene> levelScenes = new List<Scene>();
    public int sceneNo;

    /* - get player ids of the players who passed the level
     * - get second level
     * - disconnect losers
     * - open second level for the winners
     */
    private void Start()
    {
        RoundTimer.instance.TimerEnd += OnRoundTimerEnd;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            winnersIDs.Add(other.GetComponent<NetworkIdentity>().connectionToClient.connectionId);
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
            else
            {
                Debug.Log("Winner - " + connection.Key);
            }
        }
        NetworkManager nmanager = new NetworkManager();
        // changes scene for all remaining players
        if (levelScenes[sceneNo] == SceneManager.GetActiveScene())
        {
            sceneNo++;
            nmanager.ServerChangeScene(levelScenes[sceneNo].name); 
        }

    }
}