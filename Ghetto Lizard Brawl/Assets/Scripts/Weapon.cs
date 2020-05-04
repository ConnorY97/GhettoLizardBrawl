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
    [SerializeField] private Hitbox[] _hitboxes;

    public void ToggleHitbox(bool state)
    {
        for (int i = 0; i < _hitboxes.Length; i++)
        {
            
        }
    }
}