using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lizard : MonoBehaviour
{
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _timeToMaxSpeed; // How long it takes to reach max speed.
    [SerializeField] private float _timeToStop; // How long it takes to come to a full stop.
    
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

    private void Knockback(Vector3 force)
    {

    }
}
