
using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.InventoryEngine;
using UnityEngine;

public class HarvestableResource : Hoverable
{
    [SerializeField] private Resource resource;
    [SerializeField] private float outlineWidth = 2f;
    [SerializeField] private HarvestProgressor harvestProgressor;
    
    private string _tooltipText;
    private Material _planetMat;
    private float _harvestingTimer = 0f;
    private int _harvestableAmount;
    private bool _mouseOver = false;
    private Inventory _playerInventory;
    
    private void Start()
    {
        _planetMat = GetComponentInChildren<SpriteRenderer>().material;
        _harvestableAmount = resource.maxResourceAmount;
        _playerInventory = Inventory.FindInventory("MainInventory", "Player1");
    }

    private void Update()
    {
        if (_harvestableAmount <= 0)
        {
            Destroy(gameObject);
        };
        
        if (_mouseOver && Input.GetMouseButton(1))
        {
            // start harvesting
            _harvestingTimer += Time.deltaTime;
            // show progressor
            harvestProgressor.ShowProgressor(_harvestingTimer / resource.harvestTime);
            if (_harvestingTimer >= resource.harvestTime)
            {
                int harvestedAmt = _harvestableAmount >= resource.resourcePerHarvest
                    ? resource.resourcePerHarvest
                    : _harvestableAmount;
                Debug.Log("Harvested " + harvestedAmt + " of " + resource.ItemName);
                _harvestableAmount -= harvestedAmt;
                // add to inventory
                _playerInventory.AddItem(resource, harvestedAmt);
                _playerInventory.SaveInventory();
                // reset timer
                _harvestingTimer = 0f;
            }
        }
        else
        {
            _harvestingTimer = 0f;
            harvestProgressor.HideProgressor();
        }
    }

    private void OnDestroy()
    {
        Tooltip.HideTooltip_Static();
    }

    protected override string GetTooltipString()
    {
        return "Resource Deposit: <color=\"yellow\">" + resource.ItemName + "</color>\n" + 
               "Available to harvest: <color=\"yellow\">" + _harvestableAmount + "/" + resource.maxResourceAmount + "</color>\n" +
               "HOLD <Right Click> to Harvest";
    }
    
    protected override void OnMouseEnterFunc()
    {
        _mouseOver = true;
        _planetMat.SetFloat("_OutlinePixelWidth", outlineWidth);
        _planetMat.SetFloat("_Glow", 1f);
    }

    protected override void OnMouseExitFunc()
    {
        _mouseOver = false;
        _planetMat.SetFloat("_OutlinePixelWidth", 0f);
        _planetMat.SetFloat("_Glow", 0f);
    }
}
