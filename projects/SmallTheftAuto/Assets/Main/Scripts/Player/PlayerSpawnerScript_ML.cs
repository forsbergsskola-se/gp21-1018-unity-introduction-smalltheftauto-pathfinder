using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnerScript_ML : MonoBehaviour
{
    private GameObject thePlayerObject;

    public GameObject playerToSpawn; 
    private Vector3 newSpawnPosition;
    public static bool SpawnerReady = false;
    
    //save last spawn point
    [SerializeField] private float x, y, z;
    
    public void Start()
    {
        UIHealthbarScript_ML.OnPlayerDeath += DeathSpawn;
        SaveSystem.OnGatherSaveData += SendSaveDataToSaveSystem;
        SaveSystem.OnSendVector += ReceiveSaveData;
        PlayerInteractions.OnEnterCar += EnterCar;

        SpawnerReady = true;
    }

    private void EnterCar(GameObject theCar)
    {
        
    }
    
    private void SendSaveDataToSaveSystem()
    {
        SaveSystem.x = x;
        SaveSystem.y = y;
        SaveSystem.z = z;
    }

    private void ReceiveSaveData(Vector3 spawnPoint)
    {
        Debug.Log("Vector data got");
        
        thePlayerObject = GameObject.Find("Player");
        thePlayerObject.SetActive(false);
        thePlayerObject.transform.position = spawnPoint;
        thePlayerObject.SetActive(true);
    }
    
    public static Vector3 FindClosestSpawnPoint(Vector3 playerPosition, string pointType)
    {
        float distance = 0;
        GameObject spawnPoint = GameObject.FindWithTag(pointType);
        float currentMin = Vector3.Distance(playerPosition, spawnPoint.transform.position);

        foreach (var el in GameObject.FindGameObjectsWithTag(pointType))
        {
            distance = Vector3.Distance(playerPosition, el.transform.position);
            
            
            if (distance < currentMin)
            {
                currentMin = distance;
                spawnPoint = el;
            }
        
        }

        return spawnPoint.transform.position;
    }
    
    private void DeathSpawn()
    {
       thePlayerObject = GameObject.Find("Player");
       thePlayerObject.SetActive(false);
       newSpawnPosition = FindClosestSpawnPoint(thePlayerObject.transform.position, "SpawnPoint");
       x = newSpawnPosition.x;
       y = newSpawnPosition.y;
       z = newSpawnPosition.z;
       StartCoroutine(DelaySpawn());
    }

    private IEnumerator DelaySpawn()
    {
        yield return new WaitForSeconds(3);
        thePlayerObject.transform.position = newSpawnPosition;
        thePlayerObject.SetActive(true);
    }
    
}
