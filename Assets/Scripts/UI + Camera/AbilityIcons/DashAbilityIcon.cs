using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbilityIcon : AbilityIcon
{
    protected override void Awake()
    {
        base.Awake();
        ResetTime = Player.PlayerData.DashCoolDownTime;
    }

    protected override void OnEnable()
    {
        Player.DashStarted += OnAbilityStarted;
        Player.DashEnded += OnAbilityEnded;
    }

    protected override void OnDisable()
    {
        Player.DashStarted -= OnAbilityStarted;
        Player.DashEnded -= OnAbilityEnded;
    }
}
