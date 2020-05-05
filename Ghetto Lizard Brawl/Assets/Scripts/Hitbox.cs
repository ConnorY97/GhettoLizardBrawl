using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public delegate void HitboxEventHandler(Lizard other);
    public event HitboxEventHandler OnHitboxEnter;

    private string _ownerTag;
    private Collider[] _triggers;

    public void Initialise(string ownerTag)
    {
        _triggers = GetComponents<Collider>();
        _ownerTag = ownerTag;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (OnHitboxEnter != null)
        {
            if (!other.CompareTag(_ownerTag))
                OnHitboxEnter(other.GetComponentInParent<Lizard>());
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