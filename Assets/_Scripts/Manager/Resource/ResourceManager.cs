namespace PhaseArchitecture
{
    using System.Collections.Generic;
    using Cysharp.Threading.Tasks;
    using UnityEngine;

    public class ResourceManager : Singleton<ResourceManager>
    {
        public readonly PrefabContainer<BaseUI> UI = new("UI", isLazyLoad: false);
        public readonly PrefabContainer<PlayerController> Player = new("Player", isLazyLoad: false);
        public readonly StringKeyContainer<ScriptableObject> SO = new("ScriptableObjects", isLazyLoad: false);

        public async UniTask Initialize()
        {
            var handles = new List<UniTask>();
            handles.Add(UI.Initialize());
            handles.Add(Player.Initialize());
            handles.Add(SO.Initialize());
            await handles;
        }
    }
}