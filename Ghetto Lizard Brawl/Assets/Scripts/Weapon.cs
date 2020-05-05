using UnityEngine;

/// <summary>
/// Description:    Represents a weapon and contains data knockback and hitbox data.
/// Requirements:   N/A
/// Author:         Reyhan Rishard, Connor Young
/// Date created:   04/05/20
/// Date modified:  04/05/20
/// </summary>

public class Weapon : MonoBehaviour
{
    public delegate void WeaponEventHandler(Lizard other);
    public event WeaponEventHandler OnWeaponHit;

    [SerializeField] private KnockbackData _knockbackData;
    [SerializeField] private Hitbox _hitbox;
    private Vector3 _direction;

    public void Initialise(Lizard owner)
    {
        _hitbox.Initialise(owner);
        ToggleHitbox(false);
    }

    private void OnEnable()
    {
        _hitbox.OnHitboxEnter += OnHitboxEnter;
    }

    private void OnDisable()
    {
        _hitbox.OnHitboxEnter -= OnHitboxEnter;
    }

    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }

    public void ToggleHitbox(bool state)
    {
        _hitbox.ToggleTriggers(state);
    }

    private void OnHitboxEnter(Lizard other)
    {
        other.Knockback(_direction, _knockbackData);

        if (OnWeaponHit != null)
            OnWeaponHit(other);
        
        Debug.Log(other.name);
    }
}