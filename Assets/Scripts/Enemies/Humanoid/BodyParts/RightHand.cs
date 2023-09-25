using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHand : BodyPart
{
    private Weapon _weapon;
    private Tween _recoilShake;

    public override void Flip(bool isFlippingRight)
    {
        base.Flip(isFlippingRight);
        _localPosition = isFlippingRight ?
            WeaponData._rightHandPosition :
            new Vector3(-WeaponData._rightHandPosition.x, WeaponData._rightHandPosition.y, WeaponData._rightHandPosition.z);
    }

    protected override void Awake()
    {
        base.Awake();
        _weapon = GetComponentInChildren<Weapon>();       
    }


    protected override void OnEnable()
    {
        base.OnEnable();
        _weapon.BulletFired += AddRecoil;

    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _weapon.BulletFired -= AddRecoil;
    }

    private void AddRecoil() 
    {
        float finalRecoil;
        Vector3 finalRotation;
        Vector3 startRotation = transform.localEulerAngles;
      
        finalRecoil = Humanoid.IsFacingRight ? WeaponData._recoil : -WeaponData._recoil;
        finalRotation = startRotation + new Vector3(0, 0, finalRecoil);
        _recoilShake = transform.DOLocalRotate (finalRotation, WeaponData._timeBetweenBullets / 2);
        _recoilShake.SetLoops(1, LoopType.Yoyo).Play();
    }
}
