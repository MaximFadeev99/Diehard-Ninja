using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public float RunSpeed { get; private set; } = 5f;
    public float WallSlideSpeed { get; private set; } = 1f;
    public float GroundDistanceCheck { get; private set; } = 0.7f;
    public float WallDistanceCheck { get; private set; } = 0.6f;
    public float JumpVelocity { get; private set; } = 3f;

    public float JumpDuration { get; private set; } = 0.5f;
    public LayerMask WallLayerMask { get; private set; } 

    private void Awake()
    {
        WallLayerMask = LayerMask.GetMask("Walls");
    }
}
