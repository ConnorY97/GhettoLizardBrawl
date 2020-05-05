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
    [SerializeField] private float _knockbackForce;
    [SerializeField] private Hitbox _hitbox;

    public void Initialise(string ownerTag)
    {
        _hitbox.Initialise(ownerTag);
    }

    private void OnEnable()
    {
        _hitbox.OnHitboxEnter += OnHitboxEnter;
    }

    private void OnDisable()
    {
        _hitbox.OnHitboxEnter -= OnHitboxEnter;
    }

    public void ToggleHitbox(bool state)
    {
        _hitbox.ToggleTriggers(state);
    }

    private void OnHitboxEnter(Lizard other)
    {
        other.Knockback(Vector3.right * _knockbackForce);
        Debug.Log(other.name);
    }
}