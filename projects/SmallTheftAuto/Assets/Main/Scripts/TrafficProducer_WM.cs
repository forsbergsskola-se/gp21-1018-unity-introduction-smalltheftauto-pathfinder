using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficProducer_WM : MonoBehaviour
{
    // why do you have this script twice? should be tidied up
    
    public bool shouldSpawn;
    public Traffic_WM[] trafficPrefabs;
    public float[] moveSpeedRange;

    private Bounds spawnArea;
    private GameObject player;

    public void SpawnTraffic(bool shouldSpawn) {
        if(shouldSpawn) {
            player = GameObject.FindGameObjectWithTag("TTarget");
        }
        this.shouldSpawn = shouldSpawn;
    }

    void Start () {
        spawnArea = this.GetComponent<BoxCollider>().bounds;
        SpawnTraffic(shouldSpawn);
        InvokeRepeating("spawnTraffic", 0.5f, 1.0f);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    Vector3 randomSpawnPosition() {
        float x = Random.Range(spawnArea.min.x, spawnArea.max.x);
        float z = Random.Range(spawnArea.min.z, spawnArea.max.z);
        float y = 7.8f;

        return new Vector3(x, y, z);
    }

    void spawnTraffic() {
        if(shouldSpawn == false || player == null) {
            return;
        }

        int index = Random.Range(0, trafficPrefabs.Length);
        var newTraffic = Instantiate(trafficPrefabs[index], randomSpawnPosition(), Quaternion.identity) as Traffic_WM;
        newTraffic.Initialize(player.transform, 
            Random.Range(moveSpeedRange[0], moveSpeedRange[1]));
    }

}
