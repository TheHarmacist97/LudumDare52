using System.Collections;
using System.Collections.Generic;
using CodeMonkey.HealthSystemCM;
using UnityEngine;

public enum ConstructionState
{
    BUILDABLE,
    COLLIDING,
    BUILT
}
public class Building : MonoBehaviour
{
    public Vector2 size;
    [SerializeField] private SpriteRenderer spriteRen;
    [SerializeField] private Collider2D coll;

    private int _sortingOrder;
    private ConstructionState _state;
    void Start()
    {
        _sortingOrder = spriteRen.sortingOrder;
    }

    public void ChangeConstructionState(ConstructionState state)
    {
        _state = state;
        if(state==ConstructionState.BUILDABLE)
        {
            spriteRen.color = Color.green;
            spriteRen.sortingOrder = 999;
        }
        else if(state==ConstructionState.COLLIDING)
        {
            spriteRen.color = Color.red;
            spriteRen.sortingOrder = 999;
        }
        else
        {
            spriteRen.color = Color.white;
            spriteRen.sortingOrder = _sortingOrder;
            coll.enabled = true;
        }
    }

    public ConstructionState GetConstructionState()
    {
        return _state;
    }
    
}
