using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : Hoverable
{
    public enum PlanetState
    {
        Default,
        Active, 
        Harvestable,
        Harvesting
    }
    [SerializeField] private string planetName = "Planet";
    [SerializeField] private Resource excessResource;
    [SerializeField] private float outlineWidth = 2f;

    [Header("Planet Harvester properties")] 
    [SerializeField] private RequiredResource[] requiredResources;
    [SerializeField] private float harvestTime = 10f;
    [SerializeField] private int resourcePerHarvest = 10;
    [SerializeField] private int maxStoredAmount = 200;
    [SerializeField] private GameObject harvesterVisual;

    private int _harvestedAmount = 0;
    private float _harvestingTimer = 0f;
    private HarvestProgressor _harvestProgressor;
    private string _tooltipText;
    private Material _planetMat;
    private PlanetState _state = PlanetState.Default;

    private void Start()
    {
        _planetMat = GetComponentInChildren<SpriteRenderer>().material;
        _harvestProgressor = GetComponentInChildren<HarvestProgressor>();
        harvesterVisual.SetActive(false);
        _tooltipText = "Planet Name: <color=\"yellow\">" + planetName + "</color>\n" + "Primary Resource: <color=\"yellow\">" + excessResource.ItemName + "</color>";
    }

    private void Update()
    {
        if (_state == PlanetState.Harvesting)
        {
            // start harvesting
            _harvestingTimer += Time.deltaTime;
            _harvestProgressor.ShowProgressor(_harvestingTimer/harvestTime);
            if (_harvestingTimer >= harvestTime)
            {
                Debug.Log("Planet Harvested " + resourcePerHarvest + " of " + excessResource.ItemName);
                _harvestedAmount += resourcePerHarvest;
                if (_harvestedAmount > maxStoredAmount)
                {
                    _harvestedAmount = maxStoredAmount;
                }
                // reset timer
                _harvestingTimer = 0f;
            }
        }
    }

    protected override string GetTooltipString()
    {
        switch (_state)
        {
            case PlanetState.Active:
                return "<b>YOU ARE HERE</b>\n" + _tooltipText;
            case PlanetState.Harvestable:
                string resourceText = "";
                foreach (var requiredResource in requiredResources)
                {
                    resourceText += "<color=\"yellow\">" + requiredResource.requiredAmount + "</color> " + requiredResource.resource.ItemName + "\n";
                }
                return _tooltipText + "\nRequired Resources for attaching " + excessResource.ItemName +
                       " Harvestor: \n" + resourceText + "<Left Click> To Attach Harvester";
            case PlanetState.Harvesting:
                return _tooltipText + "\nHarvester Details:\n<color=\"yellow\">" + _harvestedAmount + "</color> " + excessResource.ItemName +
                       " Harvested!\n<Left Click> To EXTRACT Harvested Resources";
        }
        if (PlanetManager.instance.SelectedPlanet == this)
        {
            return "<b>YOU ARE HERE</b>\n" + _tooltipText;
        }
        return _tooltipText;
    }

    protected override void OnMouseEnterFunc()
    {
        if (PlanetManager.instance.SelectedPlanet != null  && _state == PlanetState.Default)
        {
            _state = PlanetState.Harvestable;
        }
        _planetMat.SetFloat("_OutlinePixelWidth", outlineWidth);
        _planetMat.SetFloat("_Glow", 1f);
    }

    protected override void OnMouseExitFunc()
    {
        _planetMat.SetFloat("_OutlinePixelWidth", 0f);
        _planetMat.SetFloat("_Glow", 0f);
    }

    protected override void OnMouseClick()
    {
        if (PlanetManager.instance.SelectedPlanet == null)
        {
            PlanetManager.instance.SelectPlanet(this);
            _state = PlanetState.Active;
            SceneSwitcher.instance.SwitchScene(SceneSwitcher.ScenesEnum.Planet);
        }
        else if (PlanetManager.instance.SelectedPlanet == this)
        {
            SceneSwitcher.instance.SwitchScene(SceneSwitcher.ScenesEnum.Planet);
        }
        else if(_state == PlanetState.Harvestable)
        {
            AttachHarvester();
        }
        else if (_state == PlanetState.Harvesting)
        {
            ClaimHarvestedResources();
        }
    }

    private void AttachHarvester()
    {
        bool hasEnoughResources = true;
        foreach (var requiredResource in requiredResources)
        {
            if (!InventoryManager.instance.InventoryContains(requiredResource.resource,
                    requiredResource.requiredAmount))
            {
                TooltipWarning.ShowTooltip_Static(() => "Not Enough Resources!");
                hasEnoughResources = false;
                return;
            }
        }

        foreach (var requiredResource in requiredResources)
        {
            InventoryManager.instance.RemoveFromInventory(requiredResource.resource, requiredResource.requiredAmount);
        }

        _state = PlanetState.Harvesting;
        harvesterVisual.SetActive(true);
    }

    private void ClaimHarvestedResources()
    {
        if (_harvestedAmount == 0)
        {
            TooltipWarning.ShowTooltip_Static(() => "Nothing harvested yet!");
            return;
        }
        InventoryManager.instance.AddToInventory(excessResource, _harvestedAmount);
        _harvestedAmount = 0;
    }
}
