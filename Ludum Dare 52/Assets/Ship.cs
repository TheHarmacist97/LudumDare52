using System;
using System.Collections;
using System.Collections.Generic;
using CodeMonkey.HealthSystemCM;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public static Ship instance;

    private HealthSystem _healthSystem;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this && instance != null)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _healthSystem = GetComponent<HealthSystemComponent>().GetHealthSystem();
        _healthSystem.OnDead += OnDead;
    }
    
    private void OnDead(object data, EventArgs args)
    {
        Destroy(gameObject);
        GameManager.instance.ShowGameOver();
    }
    
    
}
