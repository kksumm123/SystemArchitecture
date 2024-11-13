using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum LoadSceneType
{
    None, LoadScene, DestroyAndLoadScene,
}

public static class SceneLoadSystem
{
    private static AsyncOperation process;
    private static bool isAbleToLoadScene = true;
    public static string currentScene;

    private static void ReleaseDontDestroyOnLoad(LoadSceneType loadSceneType)
    {
        switch (loadSceneType)
        {
            case LoadSceneType.DestroyAndLoadScene:
                DontDestroyOnLoadSystem.DestroyAll();
                break;
        }
    }

    private static void OnUnloadScene() { }

    private static void OnLoadedSceneEvent(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnLoadedSceneEvent;
        SceneManager.SetActiveScene(scene);
        isAbleToLoadScene = true;
    }

    /// <summary>
    /// Awake에서 LoadScene 시작시 OnLoadedScene이벤트에는
    /// 새로 로드된 씬이 아닌, 현재 시작한 씬의 정보가 담김.
    /// 2022.07.13 Woony
    /// </summary>
    /// <returns></returns>
    private static async UniTask AsyncLoadScene(string sceneName, LoadSceneType loadSceneType, Action callback)
    {
        if (!isAbleToLoadScene) return;

        ReleaseDontDestroyOnLoad(loadSceneType);

        isAbleToLoadScene = false;

        if (IsNeedToUnloadScene())
        {
            OnUnloadScene();
            process = SceneManager.UnloadSceneAsync(currentScene);
            await UniTask.WaitUntil(() => process.isDone);
        }

        SceneManager.sceneLoaded += OnLoadedSceneEvent;
        process = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        await UniTask.WaitUntil(() => process.isDone);

        currentScene = sceneName;

        process.allowSceneActivation = true;
        callback?.Invoke();

        bool IsNeedToUnloadScene() => !string.IsNullOrEmpty(currentScene);
    }

    public static async UniTask LoadScene(string sceneName, LoadSceneType loadSceneType, Action callback)
    {
        await AsyncLoadScene(sceneName, loadSceneType, callback);
    }

    public static void UnloadSceneOptions(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }
}