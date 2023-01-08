using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SupanthaPaul;

public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] private HarvestableResource commonResource1, commonResource2;
    private HarvestableResource uniqueresource;
    [SerializeField, Range(0,50f)] private float spawnRate;

    private GridSystem grid;
    Resource sessionPlanetResource;
    // Start is called before the first frame update
    void Start()
    {
        DayNightSystem.instance.onDayStartedEvent.AddListener(RepopulateResources);
        grid = GridCreator.instance.grid;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            RepopulateResources();
        }
    }

    public void RepopulateResources()
    {
        DestroyChildren();

        sessionPlanetResource = PlanetManager.instance.SelectedPlanet.excessResource;
        uniqueresource = sessionPlanetResource.Prefab.GetComponent<HarvestableResource>();
        foreach (CellObject cell in grid.cellValues)
        {
            if(Random.Range(0,100)<spawnRate)
            {
                cell.occupied = true;
                Instantiate(RandomizeResource(), cell.transform.position, Quaternion.identity).transform.parent = transform;
            }
        }
    }

    private void DestroyChildren()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    public HarvestableResource RandomizeResource()
    {
        int random = Mathf.CeilToInt(Random.Range(0f, 3f));
        if ( random > 2)
        {
            return commonResource1;
        }
        else if(random>1)
        {
            return commonResource2;
        }    
        else
        {
            return uniqueresource;
        }
    }
}
