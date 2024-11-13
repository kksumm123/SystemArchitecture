namespace PhaseArchitecture
{
    using Cysharp.Threading.Tasks;

    public class PhaseStage : PhaseBase
    {
        protected override void OnInit()
        {
            enabled = false;
        }

        protected override void OnEnter(EPhaseType prevPhaseType)
        {
            Process().Forget();
        }

        private async UniTask Process()
        {
            enabled = true;
            // ResourceHandler.Battle.Init();
            // UIManager.Instance.OpenUI<MainHudUI>(EUI.MainHudUI, EUIParentPanel.MainRoot);
            // // todo: stageUI 만들고 나면, 유저가 선택한 월드 / 스테이지 정보 넣어줘야 함.
            // CCDebug.LogError("stageUI 만들고 나면, 유저가 선택한 월드 / 스테이지 정보 넣어줘야 함.");
            // await ResourceHandler.Battle.ChangeStage(1, 1, 1); 
        }

        protected override void OnLeave(EPhaseType nextPhaseType)
        {
            // UIManager.Instance.CleanUp();
            // ResourceHandler.Battle.CleanUp();
        }
    }
}