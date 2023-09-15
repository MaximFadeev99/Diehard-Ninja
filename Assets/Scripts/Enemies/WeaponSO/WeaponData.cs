using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapons / Create new weapon", order = 51)]
public class WeaponData: ScriptableObject
{
    [SerializeField] public Vector3 _rightHandPosition;
    [SerializeField] public Vector3 _weaponPosition;
    [SerializeField] public Sprite _weaponSprite;
    [SerializeField] public bool _isLeftHandVisible;
    //[SerializeField] public Vector3 _leftHandPosition;
    [SerializeField] public string _animationCode;
}
