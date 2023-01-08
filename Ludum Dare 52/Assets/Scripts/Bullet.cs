using System;
using CodeMonkey.HealthSystemCM;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float damage= 10f;
    [SerializeField] private float speed= 2f;
    [SerializeField] private float autoDestroy= 5f;

    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, autoDestroy);
    }

    private void LateUpdate()
    {
        _rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            HealthSystemComponent healthSystemComponent = col.GetComponent<HealthSystemComponent>();
            healthSystemComponent.GetHealthSystem().Damage(damage);
            
            Destroy(gameObject);
        }
    }

}
