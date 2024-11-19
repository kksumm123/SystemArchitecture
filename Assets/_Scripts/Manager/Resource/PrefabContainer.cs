namespace PhaseArchitecture
{
    using Cysharp.Threading.Tasks;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    public class PrefabContainer<T> : StringKeyContainer<T> where T : MonoBehaviour
    {
        public PrefabContainer(string label, bool isLazyLoad) : base(label, isLazyLoad) { }

        public override async UniTask Initialize()
        {
            if (isLazyLoad)
            {
                await AddressableLoader.LazyLoad<GameObject>(label, key => originalKeys[ConvertKey(key)] = key);
            }
            else
            {
                await AddressableLoader.Load<GameObject>(label, _OnLoaded);
            }
        }

        private void _OnLoaded(string stringKey, GameObject gameObject)
        {
            OnLoaded(stringKey, gameObject.GetComponent<T>());
        }

        public override T GetItem(string key)
        {
            if (key == null)
            {
                Debug.LogError($"{this}.{nameof(GetItem)}: key is null");
                return default;
            }

            if (originalKeys.TryGetValue(GetLowerKey(key), out var originalKey))
            {
                if (data.TryGetValue(GetLowerKey(key), out var result))
                {
                    return result;
                }
                else
                {
                    var asset = Addressables.LoadAssetAsync<GameObject>(originalKey).WaitForCompletion();
                    _OnLoaded(originalKey, asset);
                    return GetItem(GetLowerKey(key));
                }
            }

            Debug.LogError($"{this}: 해당 키를 가진 대상 탐색 실패. key = {key}");
            return default;
        }
    }
}