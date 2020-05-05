using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public delegate void HitboxEventHandler(Lizard other);
    public event HitboxEventHandler OnHitboxEnter;

    private Lizard _owner;
    private Collider[] _triggers;

    public void Initialise(Lizard owner)
    {
        _triggers = GetComponents<Collider>();
        _owner = owner;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (OnHitboxEnter != null)
        {
            Lizard otherLizard = other.GetComponentInParent<Lizard>();

            if (otherLizard == _owner && otherLizard == null)
                return;

            OnHitboxEnter(otherLizard);
        }
    }

    public void ToggleTriggers(bool state)
    {
        for (int i = 0; i < _triggers.Length; i++)
        {
            _triggers[i].enabled = state;
        }
    }
}