namespace PhaseArchitecture
{
    using Cysharp.Threading.Tasks;

    public class GameManager : MonoSingleton<GameManager>
    {
        private void Awake()
        {
            PhaseManager.Initialize().Forget();
        }

        public void AddScore(int value)
        {
             UIManager.Instance.GetUI<ScoreUI>(UIParentType.Main)?.AddScore(value);
        }
    }
}