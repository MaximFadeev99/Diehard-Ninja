using UnityEngine;

public interface IDeflectable
{
    public Vector2 CurrentVelocity { get;}
    
    public void ChangeDirection(Vector2 newVector2, int newLayer);
}