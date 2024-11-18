namespace PlayerSystem
{
    using System;
    using UnityEngine;

    [Serializable]
    public class JumpSystem
    {
        private Rigidbody2D _rigid2D;
        private Action _onJump;

        [SerializeField] private int maxJumpCount = 2;
        [SerializeField] private float jumpPower = 3;

        private int _curJumpCount;
        private bool _isJumping;

        public void Initialize(Rigidbody2D rigidbody2D, Action onJump)
        {
            _rigid2D = rigidbody2D;
            _onJump = onJump;
        }

        public void Jump()
        {
            if (_curJumpCount >= maxJumpCount) return;

            _isJumping = true;
            _curJumpCount++;
            _onJump?.Invoke();
        }

        public void OnGround()
        {
            _isJumping = false;
            _curJumpCount = 0;
        }

        public void CustomFixedUpdate()
        {
            if (!_isJumping) return;

            var factor = jumpPower * Time.deltaTime * Vector2.up;
            _rigid2D.position += factor;
        }
    }
}