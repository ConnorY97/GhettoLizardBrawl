using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Description:    Contains all the functionality of a lizard. Not responsible for any decision making.
/// Requirements:   N/A
/// Author:         Reyhan Rishard, Connor Young
/// Date created:   04/05/20
/// Date modified:  06/05/20
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
    private Vector3 _finalKnockbackPosition;
    private Vector3 _initKnockbackPosition;
    private bool _knockbackBuffered = false;
    private float _currentKnockbackDuration;
    private float _knockbackTime;

    private Rigidbody _rb;
    private Animator _playerAnim;
    [SerializeField] private Animator _meshAnim;
    [SerializeField] private Transform _anchor;

    private bool _forcesEnabled = true;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _playerAnim = GetComponent<Animator>();

        if (_weapon != null)
            _weapon.Initialise(this);
    }

    private void Update()
    {
        if (_knockbackBuffered)
            LerpKnockback();

        _meshAnim.SetFloat("Speed", _rb.velocity.magnitude);
    }

    private void OnEnable()
    {
        if (_weapon != null)
            _weapon.OnWeaponHit += OnWeaponHit;
    }

    private void OnDisable()
    {
        if (_weapon != null)
            _weapon.OnWeaponHit -= OnWeaponHit;
    }

    public void Accelerate(Vector3 direction)
    {
        if (_forcesEnabled)
        {
            // vf = a*t equation to work out the acceleration given the time.
            Vector3 acceleration = direction * (_maxSpeed / _timeToMaxSpeed);
            _rb.AddForce(acceleration, ForceMode.Impulse);
            _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, _maxSpeed);
        }
    }

    public void Stop()
    {
        _rb.velocity = Vector3.zero;
    }

    public void BeginAttack()
    {
        if (_weapon != null)
        {
            _playerAnim.SetTrigger("Attack");
            _weapon.SetDirection(_facing);
            _weapon.ToggleHitbox(true);
        }
    }

    public void PlayAttackSound()
    {
        SoundManager.instance.PlaySlashOneshot();
    }

    private void OnWeaponHit(Lizard other)
    {
        SoundManager.instance.PlayCheersOneshot();
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
        float t = elapsedTime / _currentKnockbackDuration;
        transform.position = Vector3.Lerp(_initKnockbackPosition, _finalKnockbackPosition, t);

        if (t >= 1.0f)
        {
            _knockbackBuffered = false;
            _forcesEnabled = true;
        }
    }

    public void Knockback(Vector3 direction, KnockbackData data)
    {
        direction.y = 0f;
        _initKnockbackPosition = transform.position;
        _finalKnockbackPosition = _initKnockbackPosition + direction * data.distance;
        _currentKnockbackDuration = data.duration;
        _knockbackTime = Time.time;
        _knockbackBuffered = true;
        _forcesEnabled = false;
        Stop();
        
        //_rb.AddForce(force, ForceMode.Impulse);
        //gameObject.SetActive(false);
    }

    public void Knockout()
    {
		SoundManager.instance.PlayCoinsOneshot();

        if (OnLizardKnockout != null)
            OnLizardKnockout(this);
    }

    public void Orientate(Vector3 direction)
    {
        _facing = direction;

        Quaternion rot = Quaternion.LookRotation(_facing, Vector3.up);
        _anchor.rotation = rot;
    }
}