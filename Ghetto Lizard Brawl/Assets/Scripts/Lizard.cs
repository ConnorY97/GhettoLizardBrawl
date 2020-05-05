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
    private Vector3 _facing;

    // Knockback.
    [SerializeField] private KnockbackData _knockbackData;
    private Vector3 _finalKnockbackPosition;
    private Vector3 _initKnockbackPosition;
    private bool _knockbackBuffered = false;
    private float _knockbackTime;

    private Rigidbody _rb;
    private Animator _anim;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();

        if (_weapon != null)
            _weapon.Initialise(this.tag);
    }

    private void Update()
    {
        if (_knockbackBuffered)
            LerpKnockback();
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
            _weapon.SetDirection(_facing);
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

    private void LerpKnockback()
    {
        float elapsedTime = Time.time - _knockbackTime;
        float t = elapsedTime / _knockbackData.duration;
        transform.position = Vector3.Lerp(_initKnockbackPosition, _finalKnockbackPosition, t);

        if (t >= 1.0f)
            _knockbackBuffered = false;
    }

    public void Knockback(Vector3 direction)
    {
        direction.y = 0f;
        _initKnockbackPosition = transform.position;
        _finalKnockbackPosition = _initKnockbackPosition + direction * _knockbackData.distance;
        _knockbackTime = Time.time;
        _knockbackBuffered = true;
        
        //_rb.AddForce(force, ForceMode.Impulse);
        //gameObject.SetActive(false);
    }

    public void Knockout()
    {
        if (OnLizardKnockout != null)
            OnLizardKnockout(this);
    }

    public void Orientate(Vector3 direction)
    {
        _facing = direction;

        Quaternion rot = Quaternion.LookRotation(_facing, Vector3.up);
        transform.rotation = rot;
    }
}