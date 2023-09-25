using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HumanoidNS;

public class Humanoid : MonoBehaviour, IDamagable
{
    [SerializeField] private WeaponData _weaponData;
    [SerializeField] private bool _isFacingRightFirst;
    [SerializeField] private Player _player;
    [SerializeField] private float _maxHealth;

    public Animator Animator { get; private set; }
    public SpriteRenderer SpriteRenderer { get; private set; }
    //public AudioSource AudioSource { get; private set; }
    public Head Head { get; private set; }
    public SpriteRenderer HeadRenderer { get; private set; }
    public HumanoidStateMachine HumanoidStateMachine { get; private set; }
    public HumanoidData HumanoidData { get; private set; }
    public SearchArea SearchArea { get; private set; }
    public ParticleSystem DecapitationEffect { get; private set; }


    public Player Player 
    { 
        get => _player;

        //private set 
        //{
        //    if (_isPlayerChangeAllowed)
        //        _player = value;
        //}
            
    }
    public RightHand RightHand { get; private set; }
    public Weapon Weapon { get; private set; }
    public LeftHand LeftHand { get; private set; }
    public BulletSpawnPoint BulletSpawnPoint { get; private set; }
    public BulletTrajectoryPoint BulletTrajectoryPoint { get; private set; }
    [SerializeField] public bool IsDead;
    public bool IsFacingRight { get; private set; }

    public WeaponData WeaponData => _weaponData;
    public bool IsFacingRightFirst => _isFacingRightFirst;
    public bool IsPlayerInRange { get; private set; } = false;
    public float CurrentHealth { get; private set; }

    private void Awake()
    {
        CurrentHealth = _maxHealth;
        HumanoidData = GetComponent<HumanoidData>();
        Animator = GetComponent<Animator>();
        SearchArea = GetComponentInChildren<SearchArea>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        DecapitationEffect = GetComponentInChildren<ParticleSystem>();
        Head = GetComponentInChildren<Head>();
        RightHand = GetComponentInChildren<RightHand>();
        Weapon = RightHand.GetComponentInChildren<Weapon>();
        BulletSpawnPoint = Weapon.GetComponentInChildren<BulletSpawnPoint>();
        BulletTrajectoryPoint = Weapon.GetComponentInChildren<BulletTrajectoryPoint>();
        LeftHand = Weapon.GetComponentInChildren<LeftHand>();

        HumanoidStateMachine = new HumanoidStateMachine(this);

        //Head.Set(this);
        //RightHand.Set(this);
        //LeftHand.Set(this);
        //Weapon.Set(this);
        Flip(IsFacingRightFirst);      
    }

    private void OnEnable()
    {
        SearchArea.PlayerGotBehind += Flip;
    }

    private void OnDisable()
    {
        SearchArea.PlayerGotBehind -= Flip;
    }


    private void Update()
    {
        //print(Player.name);
        HumanoidStateMachine.DoLogicUpdate();
    }

    private void FixedUpdate()
    {
        HumanoidStateMachine.DoPhysicsUpdate();
    }

    public void SetPlayerInRange(bool isPlayerInRange) 
    {
        IsPlayerInRange = isPlayerInRange;
    }

    private void Flip(bool isFlippingRight)
    {
        SpriteRenderer.flipX = !isFlippingRight;
        IsFacingRight = isFlippingRight;
    }

    public void TakeDamage(float damage) 
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0) 
        {
            IsDead = true;
        }
    }

    public void DestroyMe() 
    {
        Destroy(gameObject);
    }

}
