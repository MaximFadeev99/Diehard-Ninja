using DG.Tweening;
using UnityEngine;

public class RightHand : BodyPart
{
    private Weapon _weapon;
    private Tween _recoilShake;
  
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

    public override void Flip(bool isFlippingRight)
    {
        base.Flip(isFlippingRight);
        _localPosition = isFlippingRight ?
            WeaponData.RightHandPosition :
            new Vector3(-WeaponData.RightHandPosition.x,
            WeaponData.RightHandPosition.y, WeaponData.RightHandPosition.z);
    }

    private void AddRecoil() 
    {
        float finalRecoil = Humanoid.IsFacingRight ? WeaponData.Recoil : -WeaponData.Recoil;
        Vector3 startRotation = transform.localEulerAngles;
        Vector3 finalRotation = startRotation + new Vector3(0, 0, finalRecoil);

        _recoilShake = transform.DOLocalRotate (finalRotation, WeaponData.TimeBetweenBullets / 2);
        _recoilShake.SetLoops(1, LoopType.Yoyo).Play();
    }
}