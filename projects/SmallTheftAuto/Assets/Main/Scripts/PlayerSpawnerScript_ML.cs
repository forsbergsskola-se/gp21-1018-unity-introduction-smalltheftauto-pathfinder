using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnerScript_ML : MonoBehaviour
{
    private GameObject thePlayerObject;
    private Vector3 newSpawnPosition;
    public void Start()
    {
        UIHealthbarScript_ML.OnPlayerDeath += DeathSpawn;
    }

    public static Vector3 FindClosestsSpawnPoint(Vector3 playerPosition, string pointType)
    {
        float distance = 0;
        GameObject spawnPoint = GameObject.FindWithTag(pointType);
        float currentMin = Vector3.Distance(playerPosition, spawnPoint.transform.position);

        foreach (var el in GameObject.FindGameObjectsWithTag(pointType))
        {
            distance = Vector3.Distance(playerPosition, el.transform.position);
            
            if (distance > 3)
            {
                if (distance < currentMin)
                {
                    spawnPoint = el;
                }
            }
        }

        return spawnPoint.transform.position;
    }
    
    private void DeathSpawn()
    {
       thePlayerObject = GameObject.Find("Player");
       thePlayerObject.SetActive(false);
       newSpawnPosition = FindClosestsSpawnPoint(thePlayerObject.transform.position, "SpawnPoint");
       StartCoroutine(DelaySpawn());
    }

    private IEnumerator DelaySpawn()
    {
        yield return new WaitForSeconds(3);
        thePlayerObject.transform.position = newSpawnPosition;
        thePlayerObject.SetActive(true);
    }
    
}
