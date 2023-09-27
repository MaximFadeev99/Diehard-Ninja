using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    [SerializeField] private Sprite[] _sprites;

    private int _currentSpriteIndex;
    private int _maxSpriteIndex;
    private Image _image;

    public Action<Heart> HeartDestroyed;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _maxSpriteIndex = _sprites.Length - 1;
        _currentSpriteIndex = 0;
    }

    public void SetNextSprite() 
    {
        if (_currentSpriteIndex <= _maxSpriteIndex) 
        {
            _image.sprite = _sprites[_currentSpriteIndex];
            _currentSpriteIndex++;        
        }
        else 
        {
            HeartDestroyed?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
