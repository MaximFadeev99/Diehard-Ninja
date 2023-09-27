using UnityEngine;

public static class AnimationData
{
    #region Player Related
    public readonly static int Idle = Animator.StringToHash(nameof(Idle));
    public readonly static int Jump = Animator.StringToHash(nameof(Jump));
    public readonly static int Run = Animator.StringToHash(nameof(Run));
    public readonly static int Fall = Animator.StringToHash(nameof(Fall));
    public readonly static int WallSlide = Animator.StringToHash(nameof(WallSlide));
    public readonly static int Die = Animator.StringToHash(nameof(Die));
    public readonly static int Deflect = Animator.StringToHash(nameof(Deflect));
    public readonly static int Dash = Animator.StringToHash(nameof(Dash));
    public readonly static int Awake = Animator.StringToHash(nameof(Awake));
    public readonly static int Block = Animator.StringToHash(nameof(Block));
    public readonly static int Attack1 = Animator.StringToHash(nameof(Attack1));
    public readonly static int Attack2 = Animator.StringToHash(nameof(Attack2));
    public readonly static int Attack3 = Animator.StringToHash(nameof(Attack3));
    #endregion 
}