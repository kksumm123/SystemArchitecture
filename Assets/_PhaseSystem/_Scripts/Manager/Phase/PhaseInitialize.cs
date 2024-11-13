namespace PhaseArchitecture
{
    using Cysharp.Threading.Tasks;
    using UnityEngine;

    public class PhaseInitialize : PhaseBase
    {
        protected override async UniTask OnInitAsync()
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

            Application.targetFrameRate = 60;
        }

        protected override void OnEnter(EPhaseType prevPhaseType)
        {
            PhaseManager.EnterPhaseTitle();
        }
    }
}