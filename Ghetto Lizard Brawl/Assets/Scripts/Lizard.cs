using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Description:    Contains all the functionality of a lizard. Not responsible for any decision making.
/// Requirements:   N/A
/// Author:         Reyhan Rishard, Connor Young
/// Date created:   04/05/20
/// Date modified:  04/05/20
/// </summary>

[RequireComponent(typeof(Rigidbody))]
public class Lizard : MonoBehaviour
{
    public float MaxSpeed => _maxSpeed;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _timeToMaxSpeed; // How long it takes to reach max speed.
    
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Accelerate(Vector3 direction)
    {
        // vf = a*t equation to work out the acceleration given the time.
        Vector3 acceleration = direction * (_maxSpeed / _timeToMaxSpeed);
        _rb.AddForce(acceleration, ForceMode.Impulse);
        _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, _maxSpeed);
    }

    public void Attack()
    {
        
    }

    public void Knockback(Vector3 force)
    {

    }
}