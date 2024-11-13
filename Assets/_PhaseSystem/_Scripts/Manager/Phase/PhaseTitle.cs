namespace PhaseArchitecture
{
    using Cysharp.Threading.Tasks;

    public class PhaseTitle : PhaseBase
    {
        protected override void OnEnter(EPhaseType prevPhaseType)
        {
            Process().Forget();
        }

        private async UniTask Process()
        {
            PhaseManager.EnterPhaseStage();
        }
    }
}