namespace PhaseArchitecture
{
    using System;
    using Cysharp.Threading.Tasks;
    using UnityEngine;

    public enum EPhaseType
    {
        Initialize,
        Title,
        Stage,
    }

    public abstract class PhaseBase : MonoBehaviour
    {
        public bool IsActive { get; private set; }
        public event Action OnEnterEvent;
        public event Action OnLeaveEvent;

        public async UniTask Init()
        {
            OnEnterEvent = null;
            OnLeaveEvent = null;

            OnInit();
            await OnInitAsync();
            gameObject.SafeSetActive(false);
        }

        public void Enter(EPhaseType prevPhaseType)
        {
            IsActive = true;
            gameObject.SafeSetActive(true);
            OnEnter(prevPhaseType);
            OnEnterEvent?.Invoke();
        }

        public void Leave(EPhaseType nextPhaseType)
        {
            OnLeaveEvent?.Invoke();
            OnLeave(nextPhaseType);
            gameObject.SafeSetActive(false);
            IsActive = false;
        }

        protected virtual void OnInit() { }
        protected virtual UniTask OnInitAsync() { return new(); }
        protected virtual void OnEnter(EPhaseType prevPhaseType) { }
        protected virtual void OnLeave(EPhaseType nextPhaseType) { }
    }
}