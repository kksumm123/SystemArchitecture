namespace PlayerSystem
{
    using System;
    using System.Threading;
    using Cysharp.Threading.Tasks;
    using UnityEngine;

    public struct AbilityParams
    {
        public Transform Owner { get; }
        public Rigidbody2D Rigid2D { get; }
        public AbilityData AbilityData { get; }

        public AbilityParams(Transform owner, Rigidbody2D rigid2D, AbilityData abilityData)
        {
            Owner = owner;
            Rigid2D = rigid2D;
            AbilityData = abilityData;
        }
    }

    public abstract class AbilityBase
    {
        public float CooldownTime => AbilityParams.AbilityData.cooldownTime;

        protected AbilityParams AbilityParams { get; private set; }

        private CancellationTokenSource _cancellationTokenSource;
        private bool _isExecute;

        protected AbilityBase(AbilityParams abilityParams)
        {
            AbilityParams = abilityParams;
        }

        public void Execute()
        {
            _Execute().Forget();
        }

        private async UniTaskVoid _Execute()
        {
            if (_isExecute)
            {
                StopAbility();
            }

            _isExecute = true;
            _cancellationTokenSource = new();
            OnExecute(_cancellationTokenSource.Token);
            await UniTask.Delay(TimeSpan.FromSeconds(AbilityParams.AbilityData.duration), cancellationToken: _cancellationTokenSource.Token);
            StopAbility();
        }

        protected abstract void OnExecute(CancellationToken token);

        public void StopAbility()
        {
            _isExecute = false;
            _cancellationTokenSource.SafeCancelTask();
            OnStopAbility();
        }

        protected abstract void OnStopAbility();
    }
}