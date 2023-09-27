using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeflectionAbilityIcon : AbilityIcon
{
    protected override void Awake()
    {
        base.Awake();
        ResetTime = Player.PlayerData.DeflectionCoolDownTime;
    }

    protected override void OnEnable()
    {
        Player.DeflectionStarted += OnAbilityStarted;
        Player.DeflectionEnded += OnAbilityEnded;
    }

    protected override void OnDisable()
    {
        Player.DeflectionStarted -= OnAbilityStarted;
        Player.DeflectionEnded -= OnAbilityEnded;
    }
}
