using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.InventoryEngine;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    private Inventory _playerInventory;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        _playerInventory = Inventory.FindInventory("MainInventory", "Player1");
    }

    public void AddToInventory(InventoryItem item, int quantity)
    {
        _playerInventory.AddItem(item, quantity);
    }

    public void RemoveFromInventory(InventoryItem item, int quantity)
    {
        _playerInventory.RemoveItemByID(item.ItemID, quantity);
    }

    public bool InventoryContains(InventoryItem item, int quantity)
    {
        return _playerInventory.GetQuantity(item.ItemID) >= quantity;
    }
}
