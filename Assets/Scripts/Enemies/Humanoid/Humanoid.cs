using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Humanoid : MonoBehaviour
{
    public HumanoidStateMachine HumanoidStateMachine { get; private set; }
    public bool IsDead { get; private set; } = false;

    private void Awake()
    {
        HumanoidStateMachine = new HumanoidStateMachine(this);
    }
}
