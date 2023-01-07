using System.Collections;
using System.Collections.Generic;
using MoreMountains.InventoryEngine;
using UnityEngine;

[CreateAssetMenu(menuName = "Resource")]
public class Resource : InventoryItem
{
    [Header("Resource Properties")]
    public bool isInfiniteResource = false;
    [Tooltip("Max amount if it's not infinite")]
    public int maxResourceAmount = 100;

    [Header("Harvesting")] 
    [Tooltip("Time to harvest in seconds")]
    public int harvestTime = 1;
    [Tooltip("Resource given on every harvest")]
    public int resourcePerHarvest = 5;
}
