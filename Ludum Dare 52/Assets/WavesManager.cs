using Pathfinding;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class WavesManager : MonoBehaviour
{
    public static WavesManager instance;

    [SerializeField] private Enemy enemy;
    [SerializeField] private int startCount;
    [SerializeField] private float percentageIncrease;
    [SerializeField] private float spawnRadius;
    [SerializeField] private float interSpawnDelay;

    private static int currentCount;
    private static int currentKills;
    private WaitForSeconds delay;
    public delegate void AllDead(float delay);
    public AllDead AllDeadEvent;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this && instance != null)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentCount = startCount;
        delay = new WaitForSeconds(interSpawnDelay);
    }

    public void SpawnWrapper()
    {
        Debug.Log("SpawnWrapper is being called "+ currentCount);
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        for (int i = 0; i < currentCount; i++)
        {
            Vector3 position = (Random.insideUnitCircle.normalized) * spawnRadius;
            Instantiate(enemy, position, Quaternion.identity);
            yield return delay;
        }

    }

    public void KillCounter()
    {
        currentKills++;
        if(currentKills==currentCount)
        {
            currentKills = 0;
            currentCount += currentCount * Mathf.CeilToInt((100 + percentageIncrease) / 100);
            AllDeadEvent(0);
        }
    }
   
}
