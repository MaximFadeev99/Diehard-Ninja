using UnityEngine;

public class LeftHand : BodyPart
{
    public override void Flip(bool isFlippingRight)
    {
        base.Flip(isFlippingRight);
        _localPosition = isFlippingRight ?
            WeaponData.LeftHandPosition :
            new Vector3(-WeaponData.LeftHandPosition.x, 
            WeaponData.LeftHandPosition.y, WeaponData.LeftHandPosition.z);
    }
}