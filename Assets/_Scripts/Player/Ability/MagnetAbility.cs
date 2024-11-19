namespace PlayerSystem
{
    using System.Collections.Generic;
    using System.Threading;
    using Cysharp.Threading.Tasks;
    using Data;
    using Unity.VisualScripting;
    using UnityEngine;

    namespace Data
    {
        public class MagneticData
        {
            public float power;
        }
    }

    public class MagnetAbility : AbilityBase
    {
        private float MagneticRadius => AbilityParams.AbilityData.values.SafeGetItem(0);
        private float MagneticPower => AbilityParams.AbilityData.values.SafeGetItem(1);

        private LayerMask _coinLayer;
        private ContactFilter2D _contactFilter2D;
        private Dictionary<Transform, MagneticData> _coins = new();
        private Collider2D[] _overlapResults = new Collider2D[50];

        public MagnetAbility(AbilityParams abilityParams) : base(abilityParams)
        {
            _contactFilter2D = new();
            _contactFilter2D.layerMask = 1 << LayerMask.NameToLayer("Coin");
            _contactFilter2D.useTriggers = true;
        }

        protected override void OnExecute(CancellationToken token)
        {
            _coins.Clear();
            Magnetic(token).Forget();
        }

        private async UniTaskVoid Magnetic(CancellationToken token)
        {
            var isTrue = true;
            while (isTrue)
            {
                var resultCount = Physics2D.OverlapCircle(
                    AbilityParams.Owner.transform.position,
                    MagneticRadius,
                    _contactFilter2D,
                    _overlapResults);

                for (int i = 0; i < resultCount; i++)
                {
                    var coin = _overlapResults.SafeGetItem(i).GetComponent<Coin>();
                    if (coin == null || _coins.ContainsKey(coin.transform))
                    {
                        continue;
                    }

                    _coins[coin.transform] = new MagneticData();
                }

                foreach (var item in _coins)
                {
                    if (item.Key == null)
                        continue;

                    var dir = AbilityParams.Owner.transform.position - item.Key.position;
                    dir.Normalize();
                    item.Value.power += MagneticPower;

                    item.Key.Translate(item.Value.power * Time.deltaTime * dir);
                }

                await UniTask.Yield(token);
            }
        }

        protected override void OnStopAbility()
        {
            _coins.Clear();
        }
    }
}