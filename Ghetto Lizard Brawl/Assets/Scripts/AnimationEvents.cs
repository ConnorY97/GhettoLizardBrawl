using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    [SerializeField] private Lizard _src;

    public void BeginAttack()
    {
        _src.BeginAttack();
    }

    public void EndAttack()
    {
        _src.EndAttack();
    }

    public void PlayAttackSound()
    {
        _src.PlayAttackSound();
    }
}