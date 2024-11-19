namespace PlayerSystem
{
    using System;
    using DG.Tweening;
    using UnityEngine;

    [Serializable]
    public class JumpSystem
    {
        public bool IsJumpEnd => !_handle?.IsTweenActive() ?? true;

        private Rigidbody2D _rigid2D;
        private Action _onJump;

        [SerializeField] private int maxJumpCount = 2;
        [SerializeField] private float jumpPower = 3;
        [SerializeField] private Woony.CustomEase jumpEase = new(0.3f);

        private int _curJumpCount;
        private Vector2 _orderPos;
        private Tween _handle;

        public void Initialize(Rigidbody2D rigidbody2D, Action onJump)
        {
            _rigid2D = rigidbody2D;
            _onJump = onJump;
        }

        public void Jump()
        {
            if (_curJumpCount >= maxJumpCount) return;

            _curJumpCount++;
            _onJump?.Invoke();

            if (_handle.IsTweenActive()) _handle.Kill();
            _orderPos = _rigid2D.position;
            _handle = DOTween.To(
                    () => 0,
                    x => _rigid2D.position = _orderPos + x * Vector2.up,
                    jumpPower,
                    jumpEase.duration)
                .SetEase(jumpEase.customEase)
                .SetUpdate(UpdateType.Fixed);
        }

        public void OnGround()
        {
            _curJumpCount = 0;
            _handle?.Kill();
        }
    }
}