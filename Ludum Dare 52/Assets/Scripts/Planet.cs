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
    public Resource excessResource;
    [SerializeField] private float outlineWidth = 2f;

    [Header("Planet Harvester properties")]
    [SerializeField] private int harvestPerSecond = 1;
    [SerializeField] private int maxStoredAmount = 200;
    [SerializeField] private GameObject harvesterVisual;

    private string _tooltipText;
    private Material _planetMat;
    private PlanetState _state = PlanetState.Default;

    private void Start()
    {
        _planetMat = GetComponentInChildren<SpriteRenderer>().material;
        _tooltipText = "Planet Name: <color=\"yellow\">" + planetName + "</color>\n" + "Primary Resource: <color=\"yellow\">" + excessResource.ItemName + "</color>";
    }

    protected override string GetTooltipString()
    {
        if (PlanetManager.instance.SelectedPlanet == this)
        {
            return "<b>YOU ARE HERE</b>\n" + _tooltipText;
        }
        return _tooltipText;
    }

    protected override void OnMouseEnterFunc()
    {
        
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
    }
}
