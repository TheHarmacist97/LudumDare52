using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float damage= 2f;
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

}
