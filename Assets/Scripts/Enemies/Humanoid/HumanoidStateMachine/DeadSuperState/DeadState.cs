using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HumanoidNS 
{
    public class DeadState : HumanoidState
    {
        private Head _head;
        private RightHand _rightHand;
        private CircleCollider2D _circleCollider;
        private BoxCollider2D _boxCollider;
        private Rigidbody2D _rigidbody;
        private Rigidbody2D _headRigidbody;
        private SearchArea _searchArea;
        private ParticleSystem _decapitationEffect;
        private Timer _destroyTimer;
        private float _destroyDelay = 10f;
        
        public DeadState(HumanoidStateMachine humanoidStateMachine, string animationCode) : 
            base(humanoidStateMachine, animationCode) 
        {
            _head = Humanoid.Head;
            _searchArea = Humanoid.SearchArea;
            _rightHand = Humanoid.RightHand;
            _decapitationEffect = Humanoid.DecapitationEffect;
            _circleCollider = _head.GetComponent<CircleCollider2D>();
            _boxCollider = Humanoid.GetComponent<BoxCollider2D>();
            _rigidbody = Humanoid.GetComponent<Rigidbody2D>();
            _headRigidbody = _head.GetComponent<Rigidbody2D>();
            _destroyTimer = new Timer();
        }

        public override State TryChange()
        {
            if (_destroyTimer.IsActive)
                _destroyTimer.Tick();
            
            return this;
        }

        public override void Enter()
        {
            float headFallXVelocity = 3f;
            float headFallYVelocity = 1f;
            Vector2 newBoxColliderOffset = new Vector2 (_boxCollider.offset.x, 0.02f);
            Vector2 newBoxColliderSide = new Vector2(_boxCollider.size.x, 1.1f);


            _searchArea.enabled = false;
            _rightHand.gameObject.SetActive(false);
            _circleCollider.enabled = true;
            _circleCollider.excludeLayers = LayerMask.GetMask("Player");
            _headRigidbody.gravityScale = 1f;
            _headRigidbody.freezeRotation = false;
            _rigidbody.freezeRotation = false;
            _boxCollider.offset = newBoxColliderOffset;
            _boxCollider.size = newBoxColliderSide;
            _boxCollider.excludeLayers = LayerMask.GetMask("Player");
            headFallXVelocity = Humanoid.IsFacingRight ? -headFallXVelocity : headFallXVelocity;
            _headRigidbody.AddForce(new Vector2(headFallXVelocity, headFallYVelocity), ForceMode2D.Impulse);
            _decapitationEffect.Play();
            _destroyTimer.Start(_destroyDelay);
            _destroyTimer.TimeIsUp += CommandKill;
        }

        private void CommandKill() 
        {
            _destroyTimer.TimeIsUp -= CommandKill;
            Humanoid.Destroy(Humanoid.gameObject);
        }
    }
}

