namespace PlayerSystem
{
    using System;
    using UnityEngine;

    [Serializable]
    public class MoveSystem
    {
        private Rigidbody2D _rigid2D;
        private Func<bool> _isMovable;
        private Action<float> _setMoveFactor;

        private Transform _camTr;
        private float _offsetX;

        [SerializeField] private float moveSpeed = 8f;

        public void Initialize(Rigidbody2D rigid2D, Func<bool> isMovable, Action<float> setMoveFactor)
        {
            _rigid2D = rigid2D;
            _isMovable = isMovable;
            _setMoveFactor = setMoveFactor;

            if (Camera.main == null)
            {
                Debug.LogError($"Camera.main is null");
                return;
            }

            _camTr = Camera.main.transform;
            _offsetX = _camTr.position.x - _rigid2D.position.x;
        }

        public void CustomFixedUpdate()
        {
            if (_camTr == null || !_isMovable.Invoke())
            {
                _setMoveFactor.Invoke(0f);
                return;
            }

            _setMoveFactor.Invoke(1);

            var moveFactor = _camTr.position.x - _rigid2D.position.x - _offsetX;
            _rigid2D.position += moveFactor * moveSpeed * Time.deltaTime * Vector2.right;
        }
    }
}