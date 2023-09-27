using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Weapon : BodyPart
{
    private BulletSpawnPoint _bulletSpawnPoint;
    private BulletTrajectoryPoint _bulletTrajectoryPoint;
    private Coroutine _fireCoroutine;
    private AudioSource _audioSource;
    private int _bulletsFired;

    public Action BulletFired;   

    protected override void Awake()
    {
        base.Awake(); 
        _audioSource = GetComponent<AudioSource>();
        _bulletSpawnPoint = GetComponentInChildren<BulletSpawnPoint>();
        _bulletTrajectoryPoint = GetComponentInChildren<BulletTrajectoryPoint>();
        SpriteRenderer.sprite = WeaponData.MainSprite;
        _audioSource.clip = WeaponData.AudioClip;
        _audioSource.volume = WeaponData.AudioClipVolume;
        _audioSource.loop = false;
    }

    public override void Flip(bool isFlippingRight)
    {
        base.Flip(isFlippingRight);
        _localPosition = isFlippingRight ?
            WeaponData.WeaponPosition :
            new Vector3(-WeaponData.WeaponPosition.x,
            WeaponData.WeaponPosition.y, WeaponData.WeaponPosition.z);
    }

    public void StartFireCoroutine() =>
        _fireCoroutine = StartCoroutine(Fire());

    public void EndFireCoroutine() 
    {
        if( _fireCoroutine != null )
            StopCoroutine( _fireCoroutine );
    }  


    private IEnumerator Fire()
    {
        var aimTime = new WaitForSeconds(WeaponData.AimTime);
        var timeBetweenBullers = new WaitForSeconds(WeaponData.TimeBetweenBullets);
        bool isFiring = true;

        _bulletsFired = 0;

        while (isFiring) 
        {
            yield return aimTime;

            while (_bulletsFired < WeaponData.BulletsPerShot) 
            {
                Vector2 flyDirection = _bulletSpawnPoint.transform.position - _bulletTrajectoryPoint.transform.position;
                Vector2 rotationDirection = Humanoid.IsFacingRight ? -flyDirection : flyDirection;
                float bulletRotation = Mathf.Atan2(rotationDirection.y, rotationDirection.x) * Mathf.Rad2Deg;
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