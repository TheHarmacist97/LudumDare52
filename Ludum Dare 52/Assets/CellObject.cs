using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellObject : MonoBehaviour
{
    [SerializeField] private Color unselectedColor, hoverColor, clickColor;
    public SpriteRenderer spriteRen;

    public void OnHover()
    {
        spriteRen.color = hoverColor;
    }

    public void OnExit()
    {
        spriteRen.color = unselectedColor;
    }

    public void OnClick()
    {
        spriteRen.color = clickColor;
    }

}
