using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HumanoidNS;

public class Humanoid : MonoBehaviour
{
    [SerializeField] private WeaponData _weaponData;
    public Animator Animator { get; private set; }
    public HumanoidStateMachine HumanoidStateMachine { get; private set; }
    public HumanoidData HumanoidData { get; private set; }
    public SearchArea SearchArea { get; private set; }
    public Player Player { get; private set; }
    public RightHand RightHand { get; private set; }
    public Weapon Weapon { get; private set; }
    public LeftHand LeftHand { get; private set; }
    public bool IsDead { get; private set; } = false;

    public WeaponData WeaponData => _weaponData;

    private void Awake()
    {
        HumanoidData = GetComponent<HumanoidData>();
        Animator = GetComponent<Animator>();
        SearchArea = GetComponentInChildren<SearchArea>();
        RightHand = GetComponentInChildren<RightHand>();
        Weapon = RightHand.GetComponentInChildren<Weapon>();
        LeftHand = Weapon.GetComponentInChildren<LeftHand>();

        HumanoidStateMachine = new HumanoidStateMachine(this);
    }

    private void Update()
    {
        HumanoidStateMachine.DoLogicUpdate();
    }

    public void SetPlayer(Player player) 
    {
        Player = player;
    }

    private void Rotate() 
    {
        
    
    }
  
}
