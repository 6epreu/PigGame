using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public float nextSpawnTime = 0;
    public float spawnTimeFrequency = 1;
    public float randomDelay = 1;
    public GameObject enemyPrefab;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time > nextSpawnTime)
        {   
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            nextSpawnTime = Time.time + spawnTimeFrequency +  Random.Range(0, randomDelay);
            Debug.Log("nextSpawnTime = " + nextSpawnTime);
        }
	}
}
