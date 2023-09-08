using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject _target;

    private void Start()
    {
        transform.position = _target.transform.position;
    }

    private void Update()
    {
        float newXPosition = transform.position.x;
        float newYPosition = transform.position.y;
        float offsetTolerance = 1;
        
        if (Mathf.Abs(_target.transform.position.x) - Mathf.Abs(newXPosition) > offsetTolerance) 
        {
            newXPosition = Mathf.MoveTowards(newXPosition, _target.transform.position.x, 0.1f);
        }

        if (Mathf.Abs(_target.transform.position.y - newYPosition) > offsetTolerance) 
        {
            newXPosition = Mathf.MoveTowards(newYPosition, _target.transform.position.y, 0.1f);
        }

        transform.position = new Vector3 (newXPosition, newYPosition, -10);
    }
}
