using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Net;
using System.IO;
using System.Net.Sockets;
using System;

public class HostServerManager : MonoBehaviour

{
    public enum host
    {
        publicHost, privateHost, localHost
    }

    public host hosting;
    public NetworkManager netManager;
    public String ip;

    public void Host()
    {
        switch (hosting)
        {
            case host.publicHost:
                string externalIpString = GetIPAddress();
                var externalIp = IPAddress.Parse(externalIpString);
                print("Public IP ADDRESS IS :" + externalIpString);
                ip = externalIpString;
                netManager.networkAddress = externalIpString;
                break;

            case host.privateHost:

                string myIP = GetLocalIPAddress();
                print("Private IP ADDRESS IS :" + myIP);
                ip = myIP;
                netManager.networkAddress = myIP;
                break;

            default:
                ip = netManager.networkAddress;
                break;
        }

        netManager.StartHost();
        MatchMakingManager.Instance.StartMatchMakingServer(netManager.networkAddress, "7777");
    }

    private static string GetIPAddress()
    {
        string address = "";
        WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
        using (WebResponse response = request.GetResponse())
        using (StreamReader stream = new StreamReader(response.GetResponseStream()))
        {
            address = stream.ReadToEnd();
        }

        int first = address.IndexOf("Address: ") + 9;
        int last = address.LastIndexOf("</body>");
        address = address.Substring(first, last - first);

        return address;
    }

    public static string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new Exception("No network adapters with an IPv4 address in the system!");
    }
}