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
    public delegate void LizardEventHandler(Lizard lizard);
    public static event LizardEventHandler OnLizardKnockout;

    public float MaxSpeed => _maxSpeed;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _timeToMaxSpeed; // How long it takes to reach max speed.

    [SerializeField] private Weapon _weapon;
    private Rigidbody _rb;
    private Animator _anim;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();

        if (_weapon != null)
            _weapon.Initialise(this.tag);
    }

    public void Accelerate(Vector3 direction)
    {
        // vf = a*t equation to work out the acceleration given the time.
        Vector3 acceleration = direction * (_maxSpeed / _timeToMaxSpeed);
        _rb.AddForce(acceleration, ForceMode.Impulse);
        _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, _maxSpeed);
    }

    public void BeginAttack()
    {
        if (_weapon != null)
        {
            _anim.SetTrigger("Attack");
            _weapon.ToggleHitbox(true);
        }
    }

    private void EndAttack()
    {
        if (_weapon != null)
        {
            _weapon.ToggleHitbox(false);
        }
    }

    public void Knockback(Vector3 force)
    {

    }

    private void Knockout()
    {
        if (OnLizardKnockout != null)
            OnLizardKnockout(this);
    }
}