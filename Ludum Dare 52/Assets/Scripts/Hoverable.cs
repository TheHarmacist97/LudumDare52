using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class Hoverable : MonoBehaviour
{
    private void OnMouseEnter()
    {
        Tooltip.ShowTooltip_Static(GetTooltipString);
        OnMouseEnterFunc();
    }

    private void OnMouseExit()
    {
        Tooltip.HideTooltip_Static();
        OnMouseExitFunc();
    }

    protected virtual string GetTooltipString()
    {
        return "Tooltip";
    }
    
    protected virtual void OnMouseEnterFunc()
    {
        
    }
    
    protected virtual void OnMouseExitFunc()
    {
        
    }
}
