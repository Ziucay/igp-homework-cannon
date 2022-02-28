using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public float RotationSpeed;
    public float ProjectileSpeed;

    public float ProjectileTimeout;
    public float CurrentTimeout;
    
    public float ViewAngle;
    public float ShootAngle;

    public GameObject Projectile;
    
    private Transform _target;
    private const float _offset = 0.05f;

    private void Start()
    {
        CurrentTimeout = -1;
    }

    private void ChooseNextTarget()
    {
        float distance = 0;
        
        foreach (var target in TargetManager.targets)
        {
            if (CompareWithOffset(distance, 0, 0.001f) == 0 && CheckTarget(target.Value))
            {
                _target = target.Value.gameObject.transform;
                distance = 
                    Vector3.Distance(gameObject.transform.position, _target.transform.position);
                continue;
            }

            if (CheckTarget(target.Value))
            {
                var currentDistance = 
                    Vector3.Distance(gameObject.transform.position, target.Value.transform.position);
                
                if (CompareWithOffset(distance, currentDistance, _offset) == 1)
                {
                    _target = target.Value.transform;
                    distance = currentDistance;
                }
            }
            
        }
    }

    private bool CheckTarget(Transform target)
    {
        if (Vector3.Angle(transform.forward.normalized, (target.position - transform.position).normalized) < ViewAngle)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private int CompareWithOffset(float firstNum, float secondNum, float offset)
    {
        float difference = firstNum - secondNum;
        if (Math.Abs(difference) <= offset)
            return 0;
        if (difference < 0)
            return -1;
        return 1;
    }

    private void FixedUpdate()
    {
        if (CurrentTimeout > 0)
            CurrentTimeout -= Time.fixedDeltaTime;
        
        if (_target != null && !_target.GetComponent<Target>().IsDead)
        {
            if (!CheckTarget(_target))
            {
                _target = null;
                return;
            }
            if (Vector3.Angle(transform.forward.normalized, (_target.position - transform.position).normalized) < ShootAngle)
            {
                if (CurrentTimeout <= 0)
                {
                    CurrentTimeout = ProjectileTimeout;
                    Shoot();    
                }
            }
            //Debug.Log("Moving at target");
            Vector3 targetDirection = (_target.position - transform.position);
            float step = RotationSpeed * Time.fixedDeltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, step, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
        else
        {
            ChooseNextTarget();
        }
    }

    private void Shoot()
    {
        GameObject projectile = Instantiate(Projectile, transform.position,Quaternion.identity);
        Rigidbody rigidbodyProjectile = projectile.GetComponent<Rigidbody>();
        rigidbodyProjectile.AddForce(transform.forward * ProjectileSpeed,ForceMode.Impulse);
    }
}
