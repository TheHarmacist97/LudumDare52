using SupanthaPaul;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    public static PlanetManager instance;
    public GameEvent onPlanetSelected;

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
        onPlanetSelected.Raise(this, planet);
    }
}
