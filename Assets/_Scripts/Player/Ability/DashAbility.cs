namespace PlayerSystem
{
    using System.Threading;
    using Cysharp.Threading.Tasks;

    public class DashAbility : AbilityBase
    {
        private float DashSpeed => AbilityParams.AbilityData.values.SafeGetItem(0);

        private MapController _mapController;

        public DashAbility(AbilityParams abilityParams) : base(abilityParams)
        {
            _mapController = MapController.Instance;
        }

        protected override void OnExecute(CancellationToken token)
        {
            _mapController.OnExecuteDash(DashSpeed);
        }

        protected override void OnStopAbility()
        {
            _mapController.OnEndDash(DashSpeed);
        }
    }
}