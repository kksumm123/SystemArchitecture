namespace PhaseArchitecture
{
    using Cysharp.Threading.Tasks;

    public class PhaseStage : PhaseBase
    {
        private PlayerController _player;

        protected override void OnInit()
        {
            _player = ResourceManager.Instance.Player.GetItem("Player");
        }

        protected override void OnEnter(EPhaseType prevPhaseType)
        {
            _player.Initialize();
        }

        protected override void OnLeave(EPhaseType nextPhaseType)
        {
            _player.OnLeaveStage();
        }
    }
}