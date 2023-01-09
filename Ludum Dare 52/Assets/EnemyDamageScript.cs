using System;
using CodeMonkey.HealthSystemCM;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class EnemyDamageScript : MonoBehaviour
{
    [SerializeField] Enemy enemy;
    [SerializeField] private float damageRate = 0.5f;

    private HealthSystemComponent _target;
    private float _nextFire = 0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Building"))
        {
            _target = collision.GetComponent<HealthSystemComponent>();
            if (_target.transform == enemy.GetAI().target)
            {
                enemy.GetAI().target = null;
                enemy.GetComponent<AIPath>().canMove = false;
            }
            InvokeRepeating(nameof(Damage), 0f, damageRate);
        }
    }

    private void Damage()
    {
        if (_target == null) return;
        
        _target.GetHealthSystem().Damage(enemy.GetDamageAmount());
    }
    // private void OnTriggerStay(Collider other)
    // {
    //     if (_target == null)
    //         return;
    //     if (Time.time > _nextFire)
    //     {
    //         _nextFire = Time.time + damageRate;
    //         Debug.Log("Damaging");
    //         _target.GetHealthSystem().Damage(enemy.GetDamageAmount());
    //     }
    // }
}
