using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.IO;
using System.Text;
using System.Net.Http;
using System.Linq.Expressions;
using Newtonsoft.Json.Linq;
using Mirror;
using TMPro;

public class MatchMakingManager : MonoBehaviour
{
    private string gatewayURL = "http://18.158.82.175:2534/";
    private string id = "";
    public static MatchMakingManager Instance;
    public HostServerManager hostServerManager;

    private string MatchIp;
    public TMP_InputField input;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
        DontDestroyOnLoad(this);
    }

    public void StartMatchMakingServer(string ip, string port)
    {
        MatchIp = input.text;
        gatewayURL = "http://" + MatchIp + ":2534/";
        print(gatewayURL);
        string values = " { \"ip\": \"" + ip + "\",  \"port\": \"" + port + "\",\"playercount \":\"0\" }";

        var content = Encoding.ASCII.GetBytes(values);
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(gatewayURL);
        request.ContentType = "application/json";

        request.Method = "PUT";
        var newStream = request.GetRequestStream(); // get a ref to the request body so it can be modified
        newStream.Write(content, 0, content.Length);
        newStream.Close();

        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        Stream resStream = response.GetResponseStream();
        StreamReader reader = new StreamReader(resStream);
        string text = reader.ReadToEnd();
        JObject json = JObject.Parse(text);
        id = ((JObject)(json["Respons"]))["id"].ToString();
        print(text);
        print("id :" + id);
    }

    public void UpdatePlayerCount()
    {
        if (NetworkManager.singleton.numPlayers >= NetworkManager.singleton.maxConnections)
        {
            LeaveMatchMakingServer();
            return;
        }
        string values = " { \"id\": \"" + id + "\", \"playercount\": \"" + NetworkManager.singleton.numPlayers + 1 + "\" }";

        var content = Encoding.ASCII.GetBytes(values);
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(gatewayURL);
        request.ContentType = "application/json";

        request.Method = "POST";
        var newStream = request.GetRequestStream(); // get a ref to the request body so it can be modified
        newStream.Write(content, 0, content.Length);
        newStream.Close();
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        Stream resStream = response.GetResponseStream();
        StreamReader reader = new StreamReader(resStream);
        string text = reader.ReadToEnd();
        print(text);
    }

    public void LeaveMatchMakingServer()
    {
        string values = " { \"id\": \"" + id + "\", \"ip\": \"" + hostServerManager.ip + "\" }"; ;
        print(values);
        var content = Encoding.ASCII.GetBytes(values);
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(gatewayURL);
        request.ContentType = "application/json";

        request.Method = "DELETE";
        var newStream = request.GetRequestStream(); // get a ref to the request body so it can be modified
        newStream.Write(content, 0, content.Length);
        newStream.Close();

        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        Stream resStream = response.GetResponseStream();
        StreamReader reader = new StreamReader(resStream);
        string text = reader.ReadToEnd();
        print(text);
    }

    public void FindServer()
    {   //    {
        //        "Respons":
        //        {
        //             "faild": false,
        //             "ip": "192.168.1.1",
        //             "port": "6584"
        //        }
        //    }
        MatchIp = input.text;
        gatewayURL = "http://" + MatchIp + ":2534/";
        print(gatewayURL);
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(gatewayURL);
        request.Method = "GET";
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        Stream resStream = response.GetResponseStream();
        StreamReader reader = new StreamReader(resStream);
        string text = reader.ReadToEnd();
        print(text);
        JObject json = JObject.Parse(text);
        NetworkManager.singleton.networkAddress = ((JObject)(json["Respons"]))["ip"].ToString();
        print(text);
        NetworkManager.singleton.StartClient();
    }

    private void Start()
    {
        NetworkManager.singleton.OnServerConnectEvent += UpdatePlayerCount;
        NetworkManager.singleton.OnServerClosedEvent += LeaveMatchMakingServer;
    }

    private void OnApplicationQuit()
    {
        if (id != "")
        {
            LeaveMatchMakingServer();
        }
    }
}