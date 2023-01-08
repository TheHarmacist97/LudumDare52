using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SupanthaPaul;

public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] List<Resource> resources;
    [SerializeField] HarvestableResource resource;
    [SerializeField, Range(0,50f)] float spawnRate;

    private GridSystem grid;
    Planet sessionPlanet;
    // Start is called before the first frame update
    void Start()
    {

        grid = GridCreator.instance.grid;
        //RepopulateResources();
    }

    public void RepopulateResources()
    {
        sessionPlanet = PlanetManager.instance.SelectedPlanet;
        foreach(CellObject cell in grid.cellValues)
        {
            if(Random.Range(0,100)<spawnRate)
            {
                cell.occupied = true;
                Instantiate(resource, cell.transform.position, Quaternion.identity).transform.parent = transform;
            }
        }
    }
}
