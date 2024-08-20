using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Enemy : MonoBehaviour
{
    public GameObject enemy;
    public float timeBetweenSpawn;
    public bool justSpawned = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!justSpawned && CameraManager.instance.inFight)
        {
            Instantiate(enemy, transform.position, Quaternion.identity);
            justSpawned = true;
            Invoke("ResetSpawnTimer",timeBetweenSpawn);
        }
    }
    void ResetSpawnTimer()
    {
        justSpawned = false;
    }
}
