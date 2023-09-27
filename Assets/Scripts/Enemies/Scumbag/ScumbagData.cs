using UnityEngine;

[CreateAssetMenu(fileName = "ScumbagData", menuName = "Data / Create ScumbagData", order = 51)]
public class ScumbagData: ScriptableObject 
{
    [SerializeField] private float _startHealth;
    [SerializeField] private float _searchAreaWidth;
    [SerializeField] private float _searchAreaHeight;
    [SerializeField] private LayerMask _playerLayerMask;

    public float StartHealth => _startHealth;
    public float SearchAreaWidth => _searchAreaWidth;
    public float SearchAreaHeight => _searchAreaHeight; 
    public LayerMask PlayerLayerMask => _playerLayerMask;
}