using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Player _player;

    private float _defaultZPosition = -10;
    private float _moveDistance;

    private void Start()
    {
        transform.position = new Vector3 
            (_player.transform.position.x, _player.transform.position.y, _defaultZPosition);
    }

    private void LateUpdate()
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
        SetMoveDistance();
        float newXPosition = Mathf.MoveTowards
            (transform.position.x, _player.transform.position.x, _moveDistance);
        float newYPosition = Mathf.MoveTowards
            (transform.position.y, _player.transform.position.y, _moveDistance);
        
        transform.position = new Vector3(newXPosition, newYPosition, _defaultZPosition); 
    }

    private void SetMoveDistance()    
    {
        float velocityThreshold = 10f;

        if (_player.StateMachine.CurrentState == _player.StateMachine.WallTouchingSuperState.WallSlidingState)
        {
            _moveDistance = 0.05f;
        }
        else if (_player.StateMachine.CurrentState == _player.StateMachine.AirbornSuperState.FallState &&
            (Mathf.Abs(_player.Rigidbody.velocity.x) > velocityThreshold || 
            Mathf.Abs(_player.Rigidbody.velocity.y) > velocityThreshold))
        {
            _moveDistance = 0.2f;
        }
        else 
        {
            _moveDistance = 0.07f;
        }   
    }
}