using UnityEngine;
using System.Collections;

public class MountainSpawner : MonoBehaviour {

    public float nextSpawnTime = 0;
    public GameObject enemyPrefab;
    public GameObject enemyPrefab2;
    public GameObject enemyPrefab3;
    public AnimationCurve animCurve;
    public float theXAxisTime = 30f;
    public float startTime = 0f;
    public int objectCount;

    // Use this for initialization
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time > nextSpawnTime)
        {
            int rand = objectCount % 3;
            if (rand == 0)
            {
                Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            }
            else if (rand == 1)
            {
                Instantiate(enemyPrefab2, transform.position, Quaternion.identity);
            }
            else if (rand == 2)
            {
                Instantiate(enemyPrefab3, transform.position, Quaternion.identity);
            }
            
                

            float timePassed = Time.time - startTime;
            if (timePassed > theXAxisTime)
            {
                startTime = Time.time;
                timePassed = theXAxisTime;
            }

            nextSpawnTime = Time.time + animCurve.Evaluate(timePassed / theXAxisTime);
            Debug.Log("added Time = " + animCurve.Evaluate(timePassed / theXAxisTime));

            objectCount++;
        }
    }
}
