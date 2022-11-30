using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGen : MonoBehaviour
{
    public float waitTime;
    int nextStep=0;
    public Transform startPos;
    public GameObject[] PrefabRoads;//Holds various types of roads
    public List<GameObject> SpawnedRoads;//List of roads that are still there

    private void Start()
    {
        StartCoroutine(SpawnTime());
    }
    void Update()
    {
        //nextStep += 20;
        //Instantiate(PrefabRoads[Random.Range(0,PrefabRoads.Length)],new Vector3(startPos.position.x+nextStep,startPos.position.y,startPos.position.y),Quaternion.identity);
    }

    IEnumerator SpawnTime()
    {
        yield return new WaitForSeconds(waitTime);
        nextStep += 20;
        Vector3 position = new Vector3(startPos.position.x + nextStep, startPos.position.y, startPos.position.y);
        SpawnedRoads.Add(Instantiate(PrefabRoads[Random.Range(0, PrefabRoads.Length)], position, Quaternion.identity));
        if(SpawnedRoads.Count >= 4)
        {
            Destroy(SpawnedRoads[0]);
            SpawnedRoads.RemoveAt(0);
        }
        StartCoroutine(SpawnTime());
    }
}
