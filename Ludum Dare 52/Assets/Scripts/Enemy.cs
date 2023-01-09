using System;

using CodeMonkey.HealthSystemCM;
using Pathfinding;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float damageAmount;
    [SerializeField] private float chanceToTargetTurret;
    [SerializeField] private AIDestinationSetter ai;
    private HealthSystem _healthSystem;
    [SerializeField] private LayerMask friendlyLayer;

    void Start()
    {
        if(UnityEngine.Random.Range(0,100)>chanceToTargetTurret)
        {
            ai.target = Ship.instance.transform;
        }
        else
        {
            ai.target = BuildingHelper.instance.GetRandomTurret();
        }
        
        _healthSystem = GetComponent<HealthSystemComponent>().GetHealthSystem();
        _healthSystem.OnDead += OnDead;
    }

    private void OnDead(object data, EventArgs args)
    {
        WavesManager.instance.KillCounter();
        Debug.Log(gameObject.name + " died");
        Destroy(gameObject);
    }

    public AIDestinationSetter GetAI()
    {
        return ai;
    }

    public float GetDamageAmount()
    {
        return damageAmount;
    }
}
