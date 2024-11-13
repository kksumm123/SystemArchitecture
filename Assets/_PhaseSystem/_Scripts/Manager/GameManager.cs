namespace PhaseArchitecture
{
    using Cysharp.Threading.Tasks;
    using UnityEngine;

    public class GameManager : MonoBehaviour
    {
        private void Awake()
        {
            PhaseManager.Initialize().Forget();
        }
    }
}