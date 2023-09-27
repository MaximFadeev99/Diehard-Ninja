using UnityEngine;

namespace ScumbagNS 
{
    public class DeadState : ScumbagState
    {
        private ScumbagData _scumbagData;
        private Head _head;
        private RightHand _rightHand;
        private CircleCollider2D _circleCollider;
        private BoxCollider2D _boxCollider;
        private Rigidbody2D _rigidbody;
        private Rigidbody2D _headRigidbody;
        private SearchArea _searchArea;
        private ParticleSystem _decapitationEffect;
        private Timer _destroyTimer = new ();
        
        public DeadState(ScumbagStateMachine scumbagStateMachine) : base(scumbagStateMachine) 
        {
            _scumbagData = Scumbag.ScumbagData;
            _head = Scumbag.Head;
            _searchArea = Scumbag.SearchArea;
            _rightHand = Scumbag.RightHand;
            _decapitationEffect = Scumbag.DecapitationEffect;
            _circleCollider = _head.GetComponent<CircleCollider2D>();
            _boxCollider = Scumbag.GetComponent<BoxCollider2D>();
            _rigidbody = Scumbag.GetComponent<Rigidbody2D>();
            _headRigidbody = _head.GetComponent<Rigidbody2D>();
        }

        public override State TryChange()
        {
            if (_destroyTimer.IsActive)
                _destroyTimer.Tick();
            
            return this;
        }

        public override void Enter()
        {                      
            _searchArea.enabled = false;
            _rightHand.gameObject.SetActive(false);
            AdjustHeadSettings();
            AdjustVodySettings();
            _decapitationEffect.Play();
            _destroyTimer.TimeIsUp += Kill;
            _destroyTimer.Start(_decapitationEffect.main.duration);
        }

        private void AdjustHeadSettings() 
        {
            float headFallXVelocity = Scumbag.IsFacingRight ? -3f : 3f;
            float headFallYVelocity = 1f;

            _circleCollider.enabled = true;
            _circleCollider.excludeLayers = _scumbagData.PlayerLayerMask;
            _headRigidbody.gravityScale = 1f;
            _headRigidbody.freezeRotation = false;
            _headRigidbody.AddForce(new Vector2(headFallXVelocity, headFallYVelocity), ForceMode2D.Impulse);
        }

        private void AdjustVodySettings() 
        {
            Vector2 newBoxColliderOffset = new (_boxCollider.offset.x, 0.02f);
            Vector2 newBoxColliderSide = new (_boxCollider.size.x, 1.1f);

            _rigidbody.freezeRotation = false;
            _boxCollider.offset = newBoxColliderOffset;
            _boxCollider.size = newBoxColliderSide;
            _boxCollider.excludeLayers = _scumbagData.PlayerLayerMask;
        }

        private void Kill() 
        {
            _destroyTimer.TimeIsUp -= Kill;
            Scumbag.gameObject.SetActive(false);
        }
    }
}