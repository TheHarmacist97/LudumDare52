using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesManager : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private int startCount;
    [SerializeField] private float percentageIncrease;
    [SerializeField] private float spawnRadius;
    [SerializeField] private float interSpawnDelay;
    private int currentCount;
    private WaitForSeconds delay;

    // Start is called before the first frame update
    void Start()
    {
        currentCount = startCount;
        delay = new WaitForSeconds(interSpawnDelay);
        SpawnWrapper();
    }

    public void SpawnWrapper()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        for (int i = 0; i < currentCount; i++)
        {
            Vector3 position = (UnityEngine.Random.insideUnitCircle.normalized)* spawnRadius;
            AIDestinationSetter ai = Instantiate(enemy, position, Quaternion.identity).GetComponent<AIDestinationSetter>();
            ai.target = transform;
            ai.transform.parent= transform;
            yield return delay;
        }
        currentCount += currentCount * (int)(100 + percentageIncrease / 100);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
