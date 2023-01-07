using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : Hoverable
{
    [SerializeField] private string planetName = "Planet";
    [SerializeField] private Resource excessResource;
    [SerializeField] private float outlineWidth = 2f;

    private Material _planetMat;

    private void Start()
    {
        _planetMat = GetComponentInChildren<SpriteRenderer>().material;
    }

    protected override string GetTooltipString()
    {
        string text = "Planet Name: " + planetName + "\n";
        text += "Primary Resource: " + excessResource.resourceName;
        return text;
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
}
