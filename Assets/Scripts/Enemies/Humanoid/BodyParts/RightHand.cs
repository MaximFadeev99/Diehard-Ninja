using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHand : BodyPart
{
    public override void Flip(bool isFlippingRight)
    {
        base.Flip(isFlippingRight);
        //_localPosition = isFlippingRight ? 
        //    new Vector3(-WeaponData._rightHandPosition.x, WeaponData._rightHandPosition.y, WeaponData._rightHandPosition.z) 
        //    : WeaponData._rightHandPosition;

        _localPosition = isFlippingRight ?
            WeaponData._rightHandPosition :
            new Vector3(-WeaponData._rightHandPosition.x, WeaponData._rightHandPosition.y, WeaponData._rightHandPosition.z);
            
    }

    protected override void Awake()
    {
        base.Awake();
    }

    //public override void Set(Humanoid humanoid)
    //{
    //    base.Set(humanoid);
    //    if (Humanoid.Player == null)
    //        gameObject.SetActive(false);
    //}

    protected override void OnEnable()
    {
        base.OnEnable();

    }
}
