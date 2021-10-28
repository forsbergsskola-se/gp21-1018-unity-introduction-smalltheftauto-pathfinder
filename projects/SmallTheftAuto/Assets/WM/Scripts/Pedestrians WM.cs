using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestriansWM : MonoBehaviour
{
    public bool shouldSpawn;
    public float[] moveSpeedRange;
    public int[] healthRange;

    private Bounds spawnArea;
    private GameObject player;

    // Start is called before the first frame update
    public void SpawnEnemies(bool shouldSpawn) {
        if(shouldSpawn) {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        this.shouldSpawn = shouldSpawn;
    }

    void Start () {
        spawnArea = this.GetComponent<BoxCollider>().bounds;
        SpawnEnemies(shouldSpawn);
        InvokeRepeating("spawnEnemy", 0.5f, 1.0f);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    
}
