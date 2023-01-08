using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GridSystem grid;
    void Start()
    {
        grid = GridCreator.instance.grid;       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
