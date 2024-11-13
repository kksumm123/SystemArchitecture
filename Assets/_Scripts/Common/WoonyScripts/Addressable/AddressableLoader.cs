// Usage 1
// await AddressableLoader.Load<GameObject>(
//     Label,
//     (x, y) =>
//         OnLoadCompleted(TranslateStringKeyToID(x), y.GetComponent<PrefabType>()));
// Usage 2
// await AddressableLoader.Load<Sprite>(LABEL, (x, y) => _images[x] = y);

using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public static class AddressableLoader
{
    public static async UniTask Load<T>(string label, Action<string, T> onLoadCompleted)
    {
        var loadResourcesHandle = Addressables.LoadResourceLocationsAsync(label, typeof(T));
        await loadResourcesHandle.Task.AsUniTask();
        var handles = new List<AsyncOperationHandle>();
        var count = loadResourcesHandle.Result.Count;
        for (int i = 0; i < count; i++)
        {
            var item = loadResourcesHandle.Result[i];
            var handle = Addressables.LoadAssetAsync<T>(item.PrimaryKey);
            handle.Completed += x => onLoadCompleted?.Invoke(item.PrimaryKey, handle.Result);
            handles.Add(handle);
        }

        await Addressables.ResourceManager.CreateGenericGroupOperation(handles).Task.AsUniTask();
    }

    public static async UniTask LazyLoad<T>(string label, Action<string> onLoadCompleted)
    {
        var loadResourcesHandle = Addressables.LoadResourceLocationsAsync(label, typeof(T));
        await loadResourcesHandle.Task.AsUniTask();
        foreach (var item in loadResourcesHandle.Result)
        {
            onLoadCompleted?.Invoke(item.PrimaryKey);
        }
    }
}