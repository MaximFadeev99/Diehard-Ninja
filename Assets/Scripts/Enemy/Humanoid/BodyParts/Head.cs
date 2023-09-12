using UnityEngine;

public class Head : MonoBehaviour 
{
    private Humanoid _humanoid;
    private float _yPositionOffest;

    private void Awake()
    {
        _humanoid = GetComponentInParent<Humanoid>();
    }

    private void OnEnable()
    {
        _yPositionOffest = transform.position.y - _humanoid.transform.position.y;
    }

    private void Update()
    {
        UpholdPosition();
    }

    private void UpholdPosition()
    {
        if (_humanoid.IsDead == false)
        {
            transform.position = new Vector2
                (_humanoid.transform.position.x, _humanoid.transform.position.y + _yPositionOffest);
        }
    }

}