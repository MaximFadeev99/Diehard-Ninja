using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public float RunSpeed { get; private set; } = 5f;
    public float GroundCheck { get; private set; } = 0.7f;
    public float JumpVelocity { get; private set; } = 3f;

    public float JumpUpHeight { get; private set; } = 3.5f;
    public float JumpHeight { get; private set; } = 2.5f;
    public LayerMask WallLayerMask { get; private set; } 

    private void Awake()
    {
        WallLayerMask = LayerMask.GetMask("Walls");
    }
}
