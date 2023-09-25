using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : BodyPart
{
    private BulletSpawnPoint _bulletSpawnPoint;
    private BulletTrajectoryPoint _bulletTrajectoryPoint;
    private Coroutine _fireCoroutine;
    private int _bulletsFired;
    private AudioSource _audioSource;

    public Action BulletFired;

    public override void Flip(bool isFlippingRight)
    {
        base.Flip(isFlippingRight);
        //_localPosition = isFlippingRight ? 
        //    new Vector3(-WeaponData._weaponPosition.x, WeaponData._weaponPosition.y, WeaponData._weaponPosition.z)
        //    : WeaponData._weaponPosition;

        _localPosition = isFlippingRight ?
            WeaponData._weaponPosition :
            new Vector3(-WeaponData._weaponPosition.x, WeaponData._weaponPosition.y, WeaponData._weaponPosition.z);
    }

    protected override void OnEnable()
    {
        base.OnEnable();       
    }

    protected override void Awake()
    {
        base.Awake(); 
        _bulletSpawnPoint = GetComponentInChildren<BulletSpawnPoint>();
        _bulletTrajectoryPoint = GetComponentInChildren<BulletTrajectoryPoint>();
        _audioSource = GetComponent<AudioSource>();
        SpriteRenderer.sprite = WeaponData._mainSprite;
        _audioSource.clip = WeaponData._audioClip;
        _audioSource.volume = WeaponData._audioClipVolume;
        _audioSource.loop = false;
    }

    public void StartFireCoroutine() 
    {
        _fireCoroutine = StartCoroutine(Fire());
    }

    public void EndFireCoroutine() 
    {
        if( _fireCoroutine != null )
            StopCoroutine( _fireCoroutine );
    }

    private IEnumerator Fire()
    {
        var aimTime = new WaitForSeconds(WeaponData._aimTime);
        var timeBetweenBullers = new WaitForSeconds(WeaponData._timeBetweenBullets);
        Vector2 flyDirection;
        Vector2 rotationDirection;
        float bulletRotation;

        _bulletsFired = 0;

        while (true) 
        {
            yield return aimTime;

            while (_bulletsFired < WeaponData._bulletsPerShot) 
            {
                flyDirection = _bulletSpawnPoint.transform.position - _bulletTrajectoryPoint.transform.position;
                rotationDirection = Humanoid.IsFacingRight ? -flyDirection : flyDirection;
                bulletRotation = Mathf.Atan2(rotationDirection.y, rotationDirection.x) * Mathf.Rad2Deg;
                _bulletSpawnPoint.GenerateBullet(flyDirection, bulletRotation);
                _audioSource.Play();
                _bulletsFired++;
                BulletFired?.Invoke();
                yield return timeBetweenBullers;
            }

            _bulletsFired = 0;       
        }
    }
}
