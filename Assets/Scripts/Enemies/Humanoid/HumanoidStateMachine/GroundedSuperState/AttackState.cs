using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

namespace HumanoidNS
{
    public class AttackState : HumanoidState
    {
        private Player _player;
        private Head _head;
        private RightHand _rightHand;
        private Weapon _weapon;
        private LeftHand _leftHand;
        private BulletSpawnPoint _bulletSpawnPoint;
        private BulletTrajectoryPoint _bulletTrajectoryPoint;

        public AttackState(HumanoidStateMachine humanoidStateMachine, string animationCode) : base(humanoidStateMachine, animationCode)
        {
            _rightHand = Humanoid.RightHand;
            _weapon = Humanoid.Weapon;
            _bulletSpawnPoint = Humanoid.BulletSpawnPoint;
            _bulletTrajectoryPoint = Humanoid.BulletTrajectoryPoint;
            _leftHand = Humanoid.LeftHand;
            _head = Humanoid.Head;
        }

        public override void Enter()
        {          
            //base.Enter();
            _player = Humanoid.Player;
            ActivateAndFlip(_rightHand);
            ActivateAndFlip(_weapon);
            ActivateAndFlip(_bulletSpawnPoint);
            ActivateAndFlip(_bulletTrajectoryPoint);

            if (Humanoid.WeaponData._isLeftHandVisible)
                ActivateAndFlip(_leftHand);

            _weapon.StartFireCoroutine();
        }

        public override State TryChange()
        {
            if (Humanoid.Player != null)
            {               
                return this;
            }
            else 
            {
                return HumanoidStateMachine.GroundedSuperState.SearchState;
            }
        }

        public override void Exit()
        {
            _weapon.EndFireCoroutine();
        }

        private void ActivateAndFlip(BodyPart bodyPart) 
        {
            bodyPart.gameObject.SetActive(true);
            bodyPart.Flip(Humanoid.IsFacingRight);
        }

        public override void UpdatePhysicalMotion()
        {
            float headMaxRotation = 30f;
            float rightHandMaxRotation = 60f;
            
            RotateBodyPart(_head, headMaxRotation);
            RotateBodyPart(_rightHand, rightHandMaxRotation);
        }

        private void RotateBodyPart(BodyPart bodyPart, float maxRotationAngle)
        {
            Vector2 direction = _player.transform.position - bodyPart.transform.position;
            direction = Humanoid.IsFacingRight ? direction : -direction;
            float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (Mathf.Abs(rotation) > maxRotationAngle) 
                rotation = rotation >= 0 ? maxRotationAngle : maxRotationAngle * -1;

            bodyPart.transform.rotation = Quaternion.Euler(0, 0, rotation);
        }
    }
}
