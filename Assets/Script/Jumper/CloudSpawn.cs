using UnityEngine;
using System.Collections;

public class CloudSpawn : MonoBehaviour {
    public float nextSpawnTime = 0;
    public GameObject enemyPrefab;
    public AnimationCurve animCurve;
    public float theXAxisTime = 30f;
    public float startTime = 0f;

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
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);

            float timePassed = Time.time - startTime;
            if (timePassed > theXAxisTime)
            {
                startTime = Time.time;
                timePassed = theXAxisTime;
            }

            nextSpawnTime = Time.time + animCurve.Evaluate(timePassed / theXAxisTime);
            Debug.Log("added Time = " + animCurve.Evaluate(timePassed / theXAxisTime));
        }
    }
}
