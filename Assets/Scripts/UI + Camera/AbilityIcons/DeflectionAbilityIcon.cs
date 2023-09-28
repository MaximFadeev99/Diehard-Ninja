public class DeflectionAbilityIcon : AbilityIcon
{
    protected override void Awake()
    {
        base.Awake();
        ResetTime = Player.Data.DeflectionCoolDownTime;
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