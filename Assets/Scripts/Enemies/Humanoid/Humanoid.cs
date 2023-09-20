using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HumanoidNS;

public class Humanoid : MonoBehaviour
{
    [SerializeField] private WeaponData _weaponData;
    [SerializeField] private bool _isFacingRightFirst;
    public Animator Animator { get; private set; }
    public SpriteRenderer SpriteRenderer { get; private set; }
    public Head Head { get; private set; }
    public SpriteRenderer HeadRenderer { get; private set; }
    public HumanoidStateMachine HumanoidStateMachine { get; private set; }
    public HumanoidData HumanoidData { get; private set; }
    public SearchArea SearchArea { get; private set; }

    private Player _player;
    public Player Player 
    { 
        get => _player;

        private set 
        {
            if (_isPlayerChangeAllowed)
                _player = value;
        }
            
    }
    public RightHand RightHand { get; private set; }
    public Weapon Weapon { get; private set; }
    public LeftHand LeftHand { get; private set; }
    public BulletSpawnPoint BulletSpawnPoint { get; private set; }
    public BulletTrajectoryPoint BulletTrajectoryPoint { get; private set; }
    public bool IsDead { get; private set; } = false;
    public bool IsFacingRight { get; private set; }

    public WeaponData WeaponData => _weaponData;
    public bool IsFacingRightFirst => _isFacingRightFirst;
    private bool _isPlayerChangeAllowed;

    private void Awake()
    {
        HumanoidData = GetComponent<HumanoidData>();
        Animator = GetComponent<Animator>();
        SearchArea = GetComponentInChildren<SearchArea>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
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

    public void SetPlayer(Player player) 
    {
        _isPlayerChangeAllowed = true;
        Player = player;
        _isPlayerChangeAllowed = false;
    }

    private void Flip(bool isFlippingRight)
    {
        SpriteRenderer.flipX = !isFlippingRight;
        IsFacingRight = isFlippingRight;
    }

}
