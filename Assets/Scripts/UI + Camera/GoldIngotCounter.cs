using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldIngotCounter : MonoBehaviour
{
    [SerializeField] private Player _player;

    private TextMeshProUGUI _count;
    private AudioSource _audioSource;

    private void Awake()
    {
        _count = GetComponentInChildren<TextMeshProUGUI>();
        _audioSource = GetComponent<AudioSource>();
        _count.text = "0";
    }

    private void OnEnable()
    {
        _player.GoldIngotNumberChanged += ChangeCount;
    }

    private void OnDisable()
    {
        _player.GoldIngotNumberChanged -= ChangeCount;
    }


    private void ChangeCount(int newValue) 
    {
        _count.text = newValue.ToString();
        _audioSource.Play();
    }
}
