namespace PhaseArchitecture
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Cysharp.Threading.Tasks;
    using UnityEngine;

    public class PhaseStage : PhaseBase
    {
        private PlayerController _player;
        private CancellationTokenSource _cancellationTokenSource;

        protected override void OnInit() { }

        protected override void OnEnter(EPhaseType prevPhaseType)
        {
            _cancellationTokenSource.SafeCancelTask();
            _cancellationTokenSource = new();
            Process(_cancellationTokenSource.Token).Forget();
        }

        private async UniTask Process(CancellationToken token)
        {
            if (_player == null)
            {
                var playerPrefab = ResourceManager.Instance.Player.GetItem("Player");
                _player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity, transform);
            }

            _player.Initialize();
            MapController.Instance.Initialize();

            var readyUI = UIManager.Instance.GenerateUI<ReadyUI>(UIParentType.Popup);
            readyUI.SetReady(3, token);
            await UniTask.Delay(TimeSpan.FromSeconds(3), cancellationToken: token);

            MapController.Instance.StartMove();
        }

        protected override void OnLeave(EPhaseType nextPhaseType)
        {
            _player.OnLeaveStage();
            MapController.Instance.ClearMap();

            _cancellationTokenSource.SafeCancelTask();
            UIManager.Instance.GenerateUI<ReadyUI>(UIParentType.Popup)
                .ForceStop();
            MapController.Instance.StopMove();
        }
    }
}