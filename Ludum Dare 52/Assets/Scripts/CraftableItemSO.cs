using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class RequiredResource
{
    public Resource resource;
    public int requiredAmount = 1;
}

[CreateAssetMenu(menuName = "Craftable Item")]
public class CraftableItemSO : ScriptableObject
{
    public string itemName = "Item";
    public Sprite itemSprite;
    public GameObject itemPrefab;
    public RequiredResource[] requiredResources;
}
