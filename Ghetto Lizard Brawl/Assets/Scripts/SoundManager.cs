using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] private float _coinsVolume;
    [SerializeField] private float _cheersVolume;
    [SerializeField] private float _selectionVolume;



    [SerializeField] private AudioSource _src;
    [SerializeField] private AudioClip[] _coins;
    [SerializeField] private AudioClip[] _cheers;
    [SerializeField] private AudioClip[] _selection;

    private void Awake()
    {
        instance = this;
    }

    public void PlayCoinsOneshot()
    {
        _src.PlayOneShot(_coins[Random.Range(0, _coins.Length)], _coinsVolume);
    }

    public void PlayCheersOneshot()
    {
        //_src.PlayOneShot(_cheers[Random.Range(0, _cheers.Length)], _cheersVolume);
    }

    public void PlaySelectionOneshot()
    {
        _src.PlayOneShot(_selection[Random.Range(0, _selection.Length)], _selectionVolume);
    }
}
