using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class Scumbag : MonoBehaviour, IDamagable
{
    #region Serialized Fields
    [SerializeField] private ScumbagData _data;
    [SerializeField] private WeaponData _weaponData;
    [SerializeField] private bool _isFacingRightFirst;
    [SerializeField] private Player _player;

    public ScumbagData ScumbagData => _data;
    public WeaponData WeaponData => _weaponData;
    public bool IsFacingRightFirst => _isFacingRightFirst;
    public Player Player => _player;
    #endregion

    #region Components & Children
    public SpriteRenderer SpriteRenderer { get; private set; }
    public Head Head { get; private set; }
    public RightHand RightHand { get; private set; }
    public Weapon Weapon { get; private set; }
    public LeftHand LeftHand { get; private set; }
    public SearchArea SearchArea { get; private set; }
    public ParticleSystem DecapitationEffect { get; private set; }
    public BulletSpawnPoint BulletSpawnPoint { get; private set; }
    public BulletTrajectoryPoint BulletTrajectoryPoint { get; private set; }
    #endregion

    #region State Control Booleans
    public bool IsDead { get; private set; } = false;
    public bool IsPlayerInRange { get; private set; } = false;
    #endregion

    #region Others
    public ScumbagStateMachine StateMachine { get; private set; }
    public float CurrentHealth { get; private set; }
    public bool IsFacingRight { get; private set; }
    #endregion

    private void Awake()
    {
        GetRequiredComponents();
        CurrentHealth = ScumbagData.StartHealth;
        StateMachine = new (this);
        Flip(IsFacingRightFirst);      
    }

    private void OnEnable() => SearchArea.PlayerGotBehind += Flip;

    private void OnDisable() => SearchArea.PlayerGotBehind -= Flip;

    private void Update() => StateMachine.DoLogicUpdate();

    private void FixedUpdate() => StateMachine.DoPhysicsUpdate();

    public void SetPlayerInRange(bool isPlayerInRange) => IsPlayerInRange = isPlayerInRange;

    public void TakeDamage(float damage) 
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0) 
            IsDead = true;
    }

    private void Flip(bool isFlippingRight)
    {
        SpriteRenderer.flipX = !isFlippingRight;
        IsFacingRight = isFlippingRight;
    }

    private void GetRequiredComponents() 
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        SearchArea = GetComponentInChildren<SearchArea>();
        DecapitationEffect = GetComponentInChildren<ParticleSystem>();
        Head = GetComponentInChildren<Head>();
        RightHand = GetComponentInChildren<RightHand>();
        Weapon = RightHand.GetComponentInChildren<Weapon>();
        BulletSpawnPoint = Weapon.GetComponentInChildren<BulletSpawnPoint>();
        BulletTrajectoryPoint = Weapon.GetComponentInChildren<BulletTrajectoryPoint>();
        LeftHand = Weapon.GetComponentInChildren<LeftHand>();
    }
}