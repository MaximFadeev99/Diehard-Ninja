using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHand : BodyPart
{
    public override void Flip(bool isFlippingRight)
    {
        base.Flip(isFlippingRight);
        //_localPosition = isFlippingRight ? 
        //    new Vector3(-WeaponData._leftHandPosition.x, WeaponData._leftHandPosition.y, WeaponData._leftHandPosition.z)
        //    : WeaponData._leftHandPosition;

        _localPosition = isFlippingRight ?
            WeaponData._leftHandPosition :
            new Vector3(-WeaponData._leftHandPosition.x, WeaponData._leftHandPosition.y, WeaponData._leftHandPosition.z);
    }

    //public override void Set(Humanoid humanoid)
    //{
    //    base.Set(humanoid);
    //    if (Humanoid.Player == null)
    //        gameObject.SetActive(false);
    //}

    protected override void Awake()
    {
        base.Awake();
    }
}


