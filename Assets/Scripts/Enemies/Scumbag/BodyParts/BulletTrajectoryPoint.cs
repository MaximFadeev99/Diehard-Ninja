using UnityEngine;

public class BulletTrajectoryPoint : BodyPart
{
    public override void Flip(bool isFlippingRight)
    {
        base.Flip(isFlippingRight);
        _localPosition = isFlippingRight ?
            WeaponData.BulletTrajectoryPointPosition :
            new Vector3(-WeaponData.BulletTrajectoryPointPosition.x, 
            WeaponData.BulletTrajectoryPointPosition.y, WeaponData.BulletTrajectoryPointPosition.z);
    }
}