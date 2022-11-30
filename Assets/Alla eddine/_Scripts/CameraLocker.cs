using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Cinemachine;

public class CameraLocker : MonoBehaviour

{
    public NetworkIdentity netWorkIdentity;
    public GameObject cameraPrefab;
    public static List<Camera> spectateCameras = new List<Camera>();

    // Start is called before the first frame update
    private void Start()
    {
        if (netWorkIdentity.isLocalPlayer)
        {
            GameObject temp = Instantiate(cameraPrefab, transform);
            temp.GetComponent<CinemachineFreeLook>().Follow = transform;
            temp.GetComponent<CinemachineFreeLook>().LookAt = transform;
        }
        else
        {
            GameObject temp = Instantiate(cameraPrefab, transform);
            temp.GetComponent<CinemachineFreeLook>().Follow = transform;
            temp.GetComponent<CinemachineFreeLook>().LookAt = transform;
            temp.SetActive(false);
            spectateCameras.Add(temp.GetComponentInChildren<Camera>());
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }
}