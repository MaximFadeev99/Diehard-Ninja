using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Data / Create WeaponData", order = 51)]
public class WeaponData: ScriptableObject
{
    [Header("Positions")]
    [SerializeField] private Vector3 _rightHandPosition;
    [SerializeField] private Vector3 _weaponPosition;
    [SerializeField] private Vector3 _leftHandPosition;
    [SerializeField] private Vector3 _bulletSpawnPointPosition;
    [SerializeField] private Vector3 _bulletTrajectoryPointPosition;
    [SerializeField] private bool _isLeftHandVisible;
    public Vector3 RightHandPosition => _rightHandPosition;
    public Vector3 WeaponPosition => _weaponPosition;
    public Vector3 LeftHandPosition => _leftHandPosition;
    public Vector3 BulletSpawnPointPosition => _bulletSpawnPointPosition;
    public Vector3 BulletTrajectoryPointPosition => _bulletTrajectoryPointPosition;
    public bool IsLeftHandVisible => _isLeftHandVisible;

    [Header("Depiction")]
    [SerializeField] private Sprite _mainSprite;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private float _audioClipVolume;
    [SerializeField] private Bullet _bullet;
    public Sprite MainSprite => _mainSprite;
    public AudioClip AudioClip => _audioClip;
    public float AudioClipVolume => _audioClipVolume;
    public Bullet Bullet => _bullet;

    [Header("Shooting")]
    [SerializeField] private float _aimTime;
    [SerializeField] private int _bulletsPerShot;
    [SerializeField] private float _timeBetweenBullets;
    [SerializeField] private float _recoil;

    public float AimTime => _aimTime;
    public int BulletsPerShot => _bulletsPerShot;
    public float TimeBetweenBullets => _timeBetweenBullets;
    public float Recoil => _recoil;
}