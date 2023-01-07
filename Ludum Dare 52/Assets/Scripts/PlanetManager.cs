using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    public static PlanetManager instance;

    public Planet SelectedPlanet { get; private set; } = null;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void SelectPlanet(Planet planet)
    {
        if(SelectedPlanet != null) return;
        
        SelectedPlanet = planet;
    }
}
