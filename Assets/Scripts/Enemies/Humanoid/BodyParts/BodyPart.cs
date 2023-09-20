using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BodyPart : MonoBehaviour
{
    protected Humanoid Humanoid;
    private SearchArea _searchArea;
    protected WeaponData WeaponData;
    protected Vector3 _localPosition;

    public SpriteRenderer SpriteRenderer { get; protected set; }

    protected virtual void Awake()
    {
        Humanoid = GetComponentInParent<Humanoid>();
        _searchArea = Humanoid.GetComponentInChildren<SearchArea>();
        WeaponData = Humanoid.WeaponData;
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Flip(Humanoid.IsFacingRightFirst);
    }

    protected virtual void OnEnable()
    {
        _searchArea.PlayerGotBehind += Flip;
    }


    protected virtual void OnDisable()
    {
        _searchArea.PlayerGotBehind -= Flip;
    }

    protected virtual void Start() 
    {
        if (Humanoid.Player == null)
            gameObject.SetActive(false);
    }

    protected virtual void Update()
    {
        if (Humanoid.IsDead == false && transform.localPosition != _localPosition)
            transform.localPosition = _localPosition;
    }

    public virtual void Flip(bool isFlippingRight) 
    {
        SpriteRenderer.flipX = !isFlippingRight;
    }
}
