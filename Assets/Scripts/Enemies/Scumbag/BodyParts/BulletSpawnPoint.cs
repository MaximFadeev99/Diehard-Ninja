using UnityEngine;

public class BulletSpawnPoint : BodyPart
{
    public override void Flip(bool isFlippingRight)
    {
        base.Flip(isFlippingRight);
        _localPosition = isFlippingRight ?
            WeaponData.BulletSpawnPointPosition :
            new Vector3(-WeaponData.BulletSpawnPointPosition.x, 
            WeaponData.BulletSpawnPointPosition.y, WeaponData.BulletSpawnPointPosition.z);
    }

    public void GenerateBullet(Vector2 flyDirection, float rotation) 
    {      
        Bullet newBullet = Instantiate
            (WeaponData.Bullet, transform.position, Quaternion.Euler(0, 0, rotation));
        newBullet.SetFlyDirection(flyDirection);
    }
}