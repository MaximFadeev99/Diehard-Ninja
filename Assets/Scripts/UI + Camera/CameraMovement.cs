using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject _target;

    private float _defaultZPosition = -10;

    private void Start()
    {
        transform.position = new Vector3 
            (_target.transform.position.x, _target.transform.position.y, _defaultZPosition);
    }

    private void Update()
    {
        float distanceTolerance = 2f;

        if (Mathf.Abs(Mathf.Abs(_target.transform.position.x) - Mathf.Abs(transform.position.x)) > distanceTolerance
            || Mathf.Abs(Mathf.Abs(_target.transform.position.y - transform.position.y)) > distanceTolerance)
        {
            MoveCamera();
        }
    }

    private void MoveCamera() 
    {
        float moveDistance = 0.03f;
        float newXPosition = Mathf.MoveTowards(transform.position.x, _target.transform.position.x, moveDistance);
        float newYPosition = Mathf.MoveTowards(transform.position.y, _target.transform.position.y, moveDistance);
        
        transform.position = new Vector3(newXPosition, newYPosition, _defaultZPosition); 
    }
}
