namespace PlayerSystem
{
    using System;
    using UnityEngine;

    [Serializable]
    public class PhysicsSystem
    {
        private Rigidbody2D _rigid2D;
        private BoxCollider2D _boxCol;

        [SerializeField] private float gravityPower = 5;
        [SerializeField] private float acceleratePower = 0.25f;

        private float _footOffset;
        private float _isGroundRayDistance = 0.01f;
        private LayerMask _groundLayer;
        private float _fallFactor;

        public void Initialize(Rigidbody2D rigid2D, BoxCollider2D boxCol)
        {
            _rigid2D = rigid2D;
            _boxCol = boxCol;

            _footOffset = boxCol.size.y * 0.5f - boxCol.offset.y;
            _groundLayer = 1 << LayerMask.NameToLayer("Ground");
        }

        private bool RayCast(Vector2 pos, Vector2 dir)
        {
            var hit = Physics2D.Raycast(pos, dir, _isGroundRayDistance, _groundLayer);
            return hit.transform;
        }

        public bool IsGround()
        {
            return RayCast(_rigid2D.position - new Vector2(0, _footOffset), Vector2.down);
        }

        public bool IsHitWall()
        {
            return RayCast(_rigid2D.position + new Vector2(_boxCol.size.x, 0), Vector2.right);
        }

        public void CustomFixedUpdate()
        {
            if (IsGround())
            {
                _fallFactor = Mathf.Abs(gravityPower);
                return;
            }

            _fallFactor += Mathf.Abs(acceleratePower);
            var gravityFactor = _fallFactor * Time.deltaTime * Vector2.down;
            _rigid2D.MovePosition(_rigid2D.position + gravityFactor);
        }
    }
}