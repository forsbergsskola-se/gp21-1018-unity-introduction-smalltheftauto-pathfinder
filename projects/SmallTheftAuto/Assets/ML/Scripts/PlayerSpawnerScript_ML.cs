using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnerScript_ML : MonoBehaviour
{
    [SerializeField] private GameObject thePlayer;
    [SerializeField] private GameObject thePlayerCamera;
    
    public void Start()
    {
        UIHealthbarScript_ML.OnPlayerDeath += DeathSpawn;
    }
    
    private void DeathSpawn()
    {
    //    StartCoroutine(DelaySpawn());

        Vector3 point = GameObject.FindWithTag("SpawnPoint").transform.position;
    //    GameObject.Find("Player").SetActive(false);
    
        GameObject.Find("Player").transform.position += new Vector3(200,0,0);
    //    GameObject.Find("Player").SetActive(true);
    }

    private IEnumerator DelaySpawn()
    {
        yield return new WaitForSeconds(3);
    }
    
}
