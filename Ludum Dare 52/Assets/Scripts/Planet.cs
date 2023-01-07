using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : Hoverable
{
    [SerializeField] private string planetName = "Planet";
    [SerializeField] private Resource excessResource;
    [SerializeField] private float outlineWidth = 2f;

    private string _tooltipText;
    private Material _planetMat;

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
            SceneSwitcher.instance.SwitchScene(SceneSwitcher.ScenesEnum.Planet);
        }
        else if (PlanetManager.instance.SelectedPlanet == this)
        {
            SceneSwitcher.instance.SwitchScene(SceneSwitcher.ScenesEnum.Planet);
        }
    }
}
