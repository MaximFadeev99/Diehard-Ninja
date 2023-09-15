using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Vector3 _localPosition;
    private Humanoid _humanoid;
    private WeaponData _weaponData;
    private LeftHand _leftHand;
    private SpriteRenderer _spriteRenderer;
    private Sprite _sprite;

    private void Awake()
    {
        _humanoid = GetComponentInParent<Humanoid>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _leftHand = _humanoid.LeftHand;
        _weaponData = _humanoid.WeaponData;
        _sprite = _weaponData._weaponSprite;
        _spriteRenderer.sprite = _sprite;
        _localPosition = _weaponData._weaponPosition;
        //_leftHand.gameObject.SetActive(_weaponData._isLeftHandVisible);
    }

    private void Update()
    {
        if (_humanoid.IsDead == false && transform.localPosition != _localPosition)
            transform.localPosition = _localPosition;
    }

}
