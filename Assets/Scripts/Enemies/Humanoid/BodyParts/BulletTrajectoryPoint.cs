using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrajectoryPoint : BodyPart
{
    public override void Flip(bool isFlippingRight)
    {
        base.Flip(isFlippingRight);
        _localPosition = isFlippingRight ?
            WeaponData._bulletTrajectoryPointPosition :
            new Vector3(-WeaponData._bulletTrajectoryPointPosition.x, WeaponData._bulletTrajectoryPointPosition.y, WeaponData._bulletTrajectoryPointPosition.z);
    }
}
