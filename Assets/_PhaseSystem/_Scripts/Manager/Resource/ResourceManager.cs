namespace PhaseArchitecture
{
    using System.Collections.Generic;
    using Cysharp.Threading.Tasks;

    public class ResourceManager : Singleton<ResourceManager>
    {
        public readonly PrefabContainer<BaseUI> UI = new("UI", isLazyLoad: false);

        public async UniTask Initialize()
        {
            var handles = new List<UniTask>();
            handles.Add(UI.Initialize());
            await handles;
        }
    }
}