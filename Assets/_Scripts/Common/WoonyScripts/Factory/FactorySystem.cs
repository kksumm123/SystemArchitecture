namespace FactorySystem
{
    using System.Collections.Generic;
    using System.Linq;
    using Cysharp.Threading.Tasks;
    using UnityEngine;

    public abstract class FactorySystem<PrefabType, ID> where PrefabType : RecycleObject
    {
        protected abstract string Label { get; }

        protected Transform factoryManager;
        protected Dictionary<ID, Factory> factories = new();
        private bool _setDefaultFactory = false;
        private Factory _defaultFactory;
        private Factory _tempFactory;

        public async UniTask Initialize(Transform factoryManager)
        {
            this.factoryManager = factoryManager;

            await AddressableLoader.Load<GameObject>(
                Label,
                (x, y) =>
                    OnLoadCompleted(TranslateStringKeyToID(x), y.GetComponent<PrefabType>()));

            if (factories.Count != 0) return;
            Debug.LogError($"{this}: label에 정의된 항목이 없어 factory를 생성하지 않았습니다.");
        }

        private void OnLoadCompleted(ID id, PrefabType prefab)
        {
            if (prefab == null)
            {
                Debug.LogError($"{this}: id={id}, 해당 키값의 오브젝트에서 {typeof(PrefabType)} 컴포넌트를 찾을 수 없습니다.");
            }
            else
            {
                InitializeFactory(id, prefab, prefab.PoolSize, prefab.useDynamicPool);
            }
        }

        protected abstract ID TranslateStringKeyToID(string primaryKey);

        protected void InitializeFactory(ID id, PrefabType prefabType, int size = 1, bool useDynamicPool = true)
        {
            factories[id] = new Factory(prefabType, size, factoryManager, useDynamicPool);

            if (_setDefaultFactory) return;
            _defaultFactory = factories.ElementAt(0).Value;
        }

        private Factory GetFactory(ID id)
        {
            if (factories.TryGetValue(id, out _tempFactory))
            {
                return _tempFactory;
            }

            Debug.LogError($"{this}:존재하지 않는 프리팹 ID입니다. id = {id}");
            return _defaultFactory;
        }

        public PrefabType GetObject(ID id)
        {
            _tempFactory = GetFactory(id);
            if (_tempFactory == null)
            {
                Debug.LogError($"{this}:존재하지 않는 프리팹 ID입니다. id = {id}");
                return _defaultFactory == null ? null : _defaultFactory.Get() as PrefabType;
            }

            return _tempFactory.Get() as PrefabType;
        }
    }
}