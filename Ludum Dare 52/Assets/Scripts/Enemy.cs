using System;
using System.Collections;
using System.Collections.Generic;
using CodeMonkey.HealthSystemCM;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private HealthSystem _healthSystem;
    void Start()
    {
        _healthSystem = GetComponent<HealthSystemComponent>().GetHealthSystem();
        _healthSystem.OnDead += OnDead;
    }

    private void OnDead(object data, EventArgs args)
    {
        Debug.Log(gameObject.name + " died");
        Destroy(gameObject);
    }
}
