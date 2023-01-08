using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Building))]
public class Tower : MonoBehaviour
{
    [SerializeField] private Transform turretTransform;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform bulletSpawnPosition;
    
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private float rangeRadius = 5f;
    [SerializeField] LayerMask enemyLayer = default;
    private Transform _target;

    private float _nextFire = 0f;
    private Collider2D[] _enemiesInRange;
    private Building _building;

    private void Start()
    {
        _building = GetComponent<Building>();
    }

    void Update()
    {
        if (_building.GetConstructionState() != ConstructionState.BUILT)
            return;
        
        if (_target == null)
        {
            _enemiesInRange = Physics2D.OverlapCircleAll(transform.position, rangeRadius, enemyLayer);
            if (_enemiesInRange.Length <= 0)
                return;
            
            _target = TargetRandomEnemy();
        }
        RotateTurretTowardsTarget(_target.position, 20f, 0f);
        if (Time.time > _nextFire)
        {
            _nextFire = Time.time + fireRate;
            Fire();
        }
    }

    private void Fire()
    {
        Bullet newBullet = Instantiate(bulletPrefab, bulletSpawnPosition.position, Quaternion.identity);
        
        //rotate the bullet as the gameobject
        newBullet.transform.right = turretTransform.right.normalized;
    }

    private Transform TargetRandomEnemy()
    {
        return _enemiesInRange[Random.Range(0, _enemiesInRange.Length)].transform;
    }
    private void RotateTurretTowardsTarget(Vector3 target, float RotationSpeed, float offset)
    {
        Vector3 dir = target - turretTransform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle + offset));
        turretTransform.rotation = Quaternion.Slerp(turretTransform.rotation, rotation, RotationSpeed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        UnityEditor.Handles.color = Color.yellow;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.back, rangeRadius);
    }
}
