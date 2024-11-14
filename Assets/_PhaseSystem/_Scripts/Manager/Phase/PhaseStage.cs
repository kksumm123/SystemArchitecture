namespace PhaseArchitecture
{
    using UnityEngine;

    public class PhaseStage : PhaseBase
    {
        private PlayerController _player;

        protected override void OnInit() { }

        protected override void OnEnter(EPhaseType prevPhaseType)
        {
            if (_player == null)
            {
                var playerPrefab = ResourceManager.Instance.Player.GetItem("Player");
                _player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity, transform);
            }

            _player.Initialize();
            MapController.Instance.Initialize();
        }

        protected override void OnLeave(EPhaseType nextPhaseType)
        {
            _player.OnLeaveStage();
            MapController.Instance.ClearMap();
        }
    }
}