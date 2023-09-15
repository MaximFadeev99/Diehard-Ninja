using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponSOtest : MonoBehaviour
{
    private RightHand _rightHand;
    private LeftHand _leftHand;
    private Weapon _weapon;
    [SerializeField] private WeaponData _weaponData;

    private void Awake()
    {
        _rightHand = GetComponentInChildren<RightHand>();
        _leftHand = GetComponentInChildren<LeftHand>();
        _weapon = GetComponentInChildren<Weapon>();
    }

    private void OnEnable()
    {
        _rightHand.transform.localPosition = _weaponData._rightHandPosition;
        _leftHand.gameObject.SetActive(_weaponData._isLeftHandVisible);
        _weapon.transform.localPosition = _weaponData._weaponPosition;
        _weapon.GetComponent<SpriteRenderer>().sprite = _weaponData._weaponSprite;
    }
}
