using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingMenu : MonoBehaviour
{
    [SerializeField] private List<CraftableItemSO> craftableItems;
    [SerializeField] private CraftableItemBtn craftButtonPrefab;

    private void Start()
    {
        foreach (var craftableItem in craftableItems)
        {
            CraftableItemBtn craftButton = Instantiate(craftButtonPrefab, transform);
            craftButton.Init(craftableItem);
        }
    }
}
