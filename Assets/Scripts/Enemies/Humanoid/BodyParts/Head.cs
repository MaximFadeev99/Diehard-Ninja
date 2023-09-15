using UnityEngine;

public class Head : MonoBehaviour 
{
    private Humanoid _humanoid;
    private Vector3 _localPosition = new Vector3 (-0.02565908f, 0.9213767f, 0f);

    private void Awake()
    {
        _humanoid = GetComponentInParent<Humanoid>();
    }

    private void Update()
    {
        if ( _humanoid.IsDead == false && transform.localPosition != _localPosition ) 
            transform.localPosition = _localPosition;
    }
}