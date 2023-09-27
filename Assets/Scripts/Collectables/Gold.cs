using UnityEngine;

public abstract class Gold : MonoBehaviour
{
    protected int YeildingAmount;

    protected abstract void Awake();

    private void OnTriggerEnter2D (Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out Player player)) 
        {
            player.AddGoldIngots(YeildingAmount);
            gameObject.SetActive(false);
        }
    }
}