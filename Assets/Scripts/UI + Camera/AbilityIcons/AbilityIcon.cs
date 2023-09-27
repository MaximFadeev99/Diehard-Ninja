using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public abstract class AbilityIcon : MonoBehaviour
{
    [SerializeField] protected Player Player;

    private Image _backgroundImage;
    private Image _abilityImage;
    private string _abilityImageName = "AbilityImage";
    protected float ResetTime;

    protected virtual void Awake()
    {
        _backgroundImage = GetComponent<Image>();
        _abilityImage = transform.Find(_abilityImageName).GetComponentInChildren<Image>();
    }

    protected abstract void OnEnable();

    protected abstract void OnDisable();

    protected void OnAbilityStarted() 
    {
        _abilityImage.fillAmount = 0;
        _backgroundImage.fillAmount = 0;
    }

    protected void OnAbilityEnded() 
    {
        _abilityImage.DOFillAmount(1, ResetTime).SetEase(Ease.Linear);
        _backgroundImage.DOFillAmount(1,ResetTime).SetEase(Ease.Linear);        
    }
}
