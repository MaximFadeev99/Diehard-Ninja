using UnityEngine;

namespace ScumbagNS
{
    public class AttackState : ScumbagState
    {
        private Player _player;
        private Head _head;
        private RightHand _rightHand;
        private Weapon _weapon;
        private LeftHand _leftHand;
        private BulletSpawnPoint _bulletSpawnPoint;
        private BulletTrajectoryPoint _bulletTrajectoryPoint;

        public AttackState(ScumbagStateMachine scumbagStateMachine) : base(scumbagStateMachine)
        {
            _player = Scumbag.Player;
            _rightHand = Scumbag.RightHand;
            _weapon = Scumbag.Weapon;
            _bulletSpawnPoint = Scumbag.BulletSpawnPoint;
            _bulletTrajectoryPoint = Scumbag.BulletTrajectoryPoint;
            _leftHand = Scumbag.LeftHand;
            _head = Scumbag.Head;
        }

        public override void Enter()
        {                     
            ActivateAndFlip(_rightHand);
            ActivateAndFlip(_weapon);
            ActivateAndFlip(_bulletSpawnPoint);
            ActivateAndFlip(_bulletTrajectoryPoint);

            if (Scumbag.WeaponData.IsLeftHandVisible)
            {
                ActivateAndFlip(_leftHand);
            }
            else 
            {
               _leftHand.gameObject.SetActive(false);
            }

            _weapon.StartFireCoroutine();
        }

        public override State TryChange()
        {
            if (Scumbag.IsPlayerInRange && Scumbag.Player.IsDead == false)
            {               
                return this;
            }
            else 
            {
                return ScumbagStateMachine.GroundedSuperState.SearchState;
            }
        }

        public override void UpdatePhysicalMotion()
        {
            float headMaxRotation = 30f;
            float rightHandMaxRotation = 60f;

            RotateBodyPart(_head, headMaxRotation);
            RotateBodyPart(_rightHand, rightHandMaxRotation);
        }

        public override void Exit() => _weapon.EndFireCoroutine();

        private void ActivateAndFlip(BodyPart bodyPart) 
        {
            bodyPart.gameObject.SetActive(true);
            bodyPart.Flip(Scumbag.IsFacingRight);
        }      

        private void RotateBodyPart(BodyPart bodyPart, float maxRotationAngle)
        {
            Vector2 direction = _player.transform.position - bodyPart.transform.position;
            direction = Scumbag.IsFacingRight ? direction : -direction;
            float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (Mathf.Abs(rotation) > maxRotationAngle) 
                rotation = rotation >= 0 ? maxRotationAngle : -maxRotationAngle;

            bodyPart.transform.rotation = Quaternion.Euler(0, 0, rotation);
        }
    }
}