namespace PhaseArchitecture
{
    using System;
    using System.Collections.Generic;
    using Cysharp.Threading.Tasks;
    using UnityEngine;

    public class PhaseManager : MonoSingleton<PhaseManager>
    {
        public static EPhaseType CurrentPhaseType { get; private set; }
        public static EPhaseType LastPhaseType { get; private set; }

        private static Dictionary<EPhaseType, PhaseBase> _phases = new();

        public static async UniTask Initialize()
        {
            RegisterPhase(EPhaseType.Initialize, FindObjectOfType<PhaseInitialize>());
            RegisterPhase(EPhaseType.Title, FindObjectOfType<PhaseTitle>());
            RegisterPhase(EPhaseType.Stage, FindObjectOfType<PhaseStage>());

            List<UniTask> handles = new();
            foreach (var item in _phases)
            {
                handles.Add(item.Value.Init());
            }

            await handles;

            ChangePhase(EPhaseType.Initialize);
        }

        private static void RegisterPhase(EPhaseType type, PhaseBase phase)
        {
            if (phase == null)
            {
                Debug.LogError($"{type} is null");
                return;
            }

            if (_phases.ContainsKey(type)) return;

            _phases[type] = phase;
        }

        public static PhaseBase GetPhase(EPhaseType type)
        {
            return _phases.TryGetValue(type, out var phase) ? phase : null;
        }

        private static void ChangePhase(EPhaseType type, bool cleanUpUI = true)
        {
            var lastPhase = GetPhase(CurrentPhaseType);
            lastPhase?.Leave(type);

            LastPhaseType = CurrentPhaseType;
            CurrentPhaseType = type;

            var currentPhase = GetPhase(CurrentPhaseType);
            currentPhase?.Enter(LastPhaseType);

            GC.Collect();
        }

        public static void EnterPhaseInitialize()
        {
            ChangePhase(EPhaseType.Initialize);
        }

        public static void EnterPhaseTitle()
        {
            ChangePhase(EPhaseType.Title);
        }

        public static void EnterPhaseStage()
        {
            ChangePhase(EPhaseType.Stage);
        }
    }
}