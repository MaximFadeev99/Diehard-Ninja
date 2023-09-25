using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapons / Create new weapon", order = 51)]
public class WeaponData: ScriptableObject
{
    [SerializeField] public Vector3 _rightHandPosition;
    [SerializeField] public Vector3 _weaponPosition;
    [SerializeField] public Vector3 _leftHandPosition;
    [SerializeField] public Vector3 _bulletSpawnPointPosition;
    [SerializeField] public Vector3 _bulletTrajectoryPointPosition;

    [SerializeField] public Sprite _mainSprite;
    [SerializeField] public AudioClip _audioClip;
    [SerializeField] public float _audioClipVolume;
    [SerializeField] public bool _isLeftHandVisible;
    [SerializeField] public Bullet _bullet;
    [SerializeField] public float _aimTime;
    [SerializeField] public int _bulletsPerShot;
    [SerializeField] public float _timeBetweenBullets;
    [SerializeField] public float _recoil;
}
