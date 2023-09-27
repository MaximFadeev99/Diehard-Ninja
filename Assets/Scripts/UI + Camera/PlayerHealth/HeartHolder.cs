using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeartHolder : MonoBehaviour
{
    [SerializeField] private Heart _heart;

    private List<Heart> _hearts = new();

    public bool AreHeartsLeft { get; private set; } = true;

    public void GenerateHearts(int heartCount) 
    {
        Heart newHeart;

        for (int i = 0; i < heartCount; i++) 
        {
            newHeart = Instantiate(_heart, transform);
            _hearts.Add(newHeart);
            newHeart.HeartDestroyed += OnHeartDestroyed;
        }
    }

    public void ReduceLifeAmount() 
    {
        _hearts[_hearts.Count - 1].SetNextSprite();         
    }

    private void OnHeartDestroyed(Heart heart) 
    {
        _hearts.Remove(heart); 
        heart.HeartDestroyed -= OnHeartDestroyed;

        if (_hearts.Count == 0 )
            AreHeartsLeft = false;
    }
}
