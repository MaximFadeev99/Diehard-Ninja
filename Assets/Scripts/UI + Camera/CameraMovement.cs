using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Player _player;

    private float _defaultZPosition = -10;

    private void Start()
    {
        transform.position = new Vector3 
            (_player.transform.position.x, _player.transform.position.y, _defaultZPosition);
    }

    private void Update()
    {
        float distanceTolerance = 2f;

        if (Mathf.Abs(Mathf.Abs(_player.transform.position.x) - Mathf.Abs(transform.position.x)) > distanceTolerance
            || Mathf.Abs(Mathf.Abs(_player.transform.position.y - transform.position.y)) > distanceTolerance)
        {
            MoveCamera();
        }
    }

    private void MoveCamera() 
    {
        float moveDistance = Mathf.Abs(_player.Rigidbody.velocity.x) > 10 
            || Mathf.Abs(_player.Rigidbody.velocity.y) > 10 ? 0.2f : 0.07f;
        float newXPosition = Mathf.MoveTowards(transform.position.x, _player.transform.position.x, moveDistance);
        float newYPosition = Mathf.MoveTowards(transform.position.y, _player.transform.position.y, moveDistance);
        
        transform.position = new Vector3(newXPosition, newYPosition, _defaultZPosition); 
    }
}
