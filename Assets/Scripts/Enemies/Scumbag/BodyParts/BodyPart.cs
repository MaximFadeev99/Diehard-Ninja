using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public abstract class BodyPart : MonoBehaviour
{
    protected Scumbag Humanoid;
    protected WeaponData WeaponData;
    protected Vector3 _localPosition;

    private SearchArea _searchArea;

    public SpriteRenderer SpriteRenderer { get; protected set; }

    protected virtual void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Humanoid = GetComponentInParent<Scumbag>();
        _searchArea = Humanoid.GetComponentInChildren<SearchArea>();
        WeaponData = Humanoid.WeaponData;
        Flip(Humanoid.IsFacingRightFirst);
    }

    protected virtual void OnEnable() => 
        _searchArea.PlayerGotBehind += Flip;

    protected virtual void OnDisable() =>
        _searchArea.PlayerGotBehind -= Flip;

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

    public virtual void Flip(bool isFlippingRight) =>
        SpriteRenderer.flipX = !isFlippingRight;
}