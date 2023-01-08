using System.Collections;
using System.Collections.Generic;
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChangeConstructionState(ConstructionState state)
    {
        if(state==ConstructionState.BUILDABLE)
        {
            spriteRen.color = Color.green;
        }
        else if(state==ConstructionState.COLLIDING)
        {
            spriteRen.color = Color.red;
        }
        else
        {
            spriteRen.color = Color.white;
            coll.enabled = true;
        }
    }
    
}
