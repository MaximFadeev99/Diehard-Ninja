using System;
using UnityEngine;

public class Timer 
{
    private float _timeLag;
    private float _elapsedTime;

    public Action TimeIsUp;

    public bool IsActive { get; private set; } = false;

    public void Start(float timeLag) 
    {
        _timeLag = timeLag;
        _elapsedTime = 0f;
        IsActive = true;
    }

    public void Tick() 
    {
        if (_elapsedTime < _timeLag)
        {
            _elapsedTime += Time.deltaTime;
        }
        else 
        {
            IsActive = false;
            TimeIsUp?.Invoke();
        }
    }
}