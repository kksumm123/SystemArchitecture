using System.Threading;
using Cysharp.Threading.Tasks;
using PhaseArchitecture;
using PlayerSystem;
using UnityEngine;

public enum AbilityType
{
    None, Dash, Magnetic,
}

public static class AbilityFactory
{
    private static AbiltiyDataContainerSO _abiltiyDataContainerSO;

    public static AbilityBase GetAbility(AbilityType type, Transform owner, Rigidbody2D ownerRigid)
    {
        AbilityParams abilityParams;
        switch (type)
        {
            case AbilityType.Dash:
                abilityParams = new AbilityParams(owner, ownerRigid, GetSO()?.DashData);
                return new DashAbility(abilityParams);
            default:
                Debug.LogError($"예외처리 필요. type = {type}");
                abilityParams = new AbilityParams(owner, ownerRigid, null);
                return new EmptyAbility(abilityParams);
        }
    }

    private static AbiltiyDataContainerSO GetSO()
    {
        if (_abiltiyDataContainerSO == null)
        {
            _abiltiyDataContainerSO = ResourceManager.Instance.SO
                .GetItem(nameof(AbiltiyDataContainerSO)) as AbiltiyDataContainerSO;
        }

        return _abiltiyDataContainerSO;
    }

    public class EmptyAbility : AbilityBase
    {
        public EmptyAbility(AbilityParams abilityParams) : base(abilityParams) { }

        protected override void OnExecute(CancellationToken token) { }

        protected override void OnStopAbility() { }
    }
}