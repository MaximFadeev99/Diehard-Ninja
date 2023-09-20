using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnPoint : BodyPart
{
    public override void Flip(bool isFlippingRight)
    {
        base.Flip(isFlippingRight);
        _localPosition = isFlippingRight ?
            WeaponData._bulletSpawnPointPosition :
            new Vector3(-WeaponData._bulletSpawnPointPosition.x, WeaponData._bulletSpawnPointPosition.y, WeaponData._bulletSpawnPointPosition.z);
    }

    public void GenerateBullet(Vector2 flyDirection, float rotation) 
    {      
        Bullet newBullet = Instantiate
            (WeaponData._bullet, transform.position, Quaternion.Euler(0, 0, rotation));
        newBullet.SetFlyDirection(flyDirection);
    }

}
