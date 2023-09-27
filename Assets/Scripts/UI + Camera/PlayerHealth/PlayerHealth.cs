using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Player _player;

    private HeartHolder _heartHolder;

    public Action PlayerDied;

    private void Awake()
    {
        _heartHolder = GetComponentInChildren<HeartHolder>();
        _heartHolder.GenerateHearts(_player.PlayerData.StartHeartCount);
    }

    private void OnEnable()
    {
        _player.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged() 
    {
        _heartHolder.ReduceLifeAmount();

        if(_heartHolder.AreHeartsLeft == false)
            PlayerDied?.Invoke();
    }

}