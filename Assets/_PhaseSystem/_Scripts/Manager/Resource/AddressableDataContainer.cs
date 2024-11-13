namespace PhaseArchitecture
{
    using System.Collections.Generic;
    using Cysharp.Threading.Tasks;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    public abstract class AddressableDataContainer<KEY, DATA_TYPE>
    {
        protected AddressableDataContainer(string label, bool isLazyLoad)
        {
            this.label = label;
#if UNITY_EDITOR
            this.isLazyLoad = false;
#else
            this.isLazyLoad = isLazyLoad;
#endif
        }

        protected string label;
        protected bool isLazyLoad;

        protected Dictionary<KEY, DATA_TYPE> data = new();
        protected Dictionary<KEY, string> originalKeys = new();

        public virtual async UniTask Initialize()
        {
            if (isLazyLoad)
            {
                await AddressableLoader.LazyLoad<DATA_TYPE>(label, key => originalKeys[ConvertKey(key)] = key);
            }
            else
            {
                await AddressableLoader.Load<DATA_TYPE>(label, OnLoaded);
            }
        }

        protected abstract KEY ConvertKey(string stringKey);

        protected virtual void OnLoaded(string stringKey, DATA_TYPE type)
        {
            data[ConvertKey(stringKey)] = type;
            originalKeys[ConvertKey(stringKey)] = stringKey;
        }

        public virtual DATA_TYPE GetItem(KEY key)
        {
            if (key == null)
            {
                Debug.LogError($"{this}.{nameof(GetItem)}: key is null");
                return default;
            }

            if (originalKeys.TryGetValue(key, out var originalKey))
            {
                if (data.TryGetValue(key, out var result))
                {
                    return result;
                }

                var asset = Addressables.LoadAssetAsync<DATA_TYPE>(originalKey).WaitForCompletion();
                OnLoaded(originalKey, asset);
                return GetItem(key);
            }

            Debug.LogError($"{this}: 해당 키를 가진 대상 탐색 실패. key = {key}");
            return default;
        }
    }
}