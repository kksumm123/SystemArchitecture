using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
#if UNITY_EDITOR
using UnityEditor;
#endif

public static partial class Woony
{
    [System.Serializable]
    public class PunchScaleInfo
    {
        public float duration = 0.5f;
        public Vector3 punch = Vector3.one;
        public int vibrato = 10;
        public int elasticity = 1;
    }

    [System.Serializable]
    public class DOShakeInfo
    {
        public float duration = 0.3f;
        public float strength = 1f;
        public int vibrato = 10;
        public float randomness = 90;
    }

    [System.Serializable]
    public class CustomEase
    {
        public float duration;
        public AnimationCurve customEase = AnimationCurve.Linear(0, 0, 1, 1);

        public CustomEase(float duration)
        {
            this.duration = duration;
            customEase = AnimationCurve.Linear(0, 0, 1, 1);
        }
    }

    public static void AlertError(string v)
    {
        Debug.LogError($"우니 : {v}");
#if UNITY_EDITOR
        EditorUtility.DisplayDialog("우니 알림", $"우니 : {v}", "오케이 ~");
#endif
    }

    public static void AlertError(string v, Transform transform)
    {
        Debug.LogError($"우니 : {v}", transform);
#if UNITY_EDITOR
        EditorUtility.DisplayDialog("우니 알림", $"우니 : {v}", "오케이 ~");
#endif
    }

    public static bool IsEmpty(this string variable)
    {
        return variable == null || variable == string.Empty;
    }

    public static bool IsTweenActive(this Tween tween)
    {
        return tween != null && tween.IsActive() && tween.IsPlaying();
    }

    public static void ExecuteTrueFalseLogic(this bool boolValue, Action onTrue, Action onFalse)
    {
        if (boolValue) onTrue?.Invoke();
        else onFalse?.Invoke();
    }

    public static string GetPath(this Transform current)
    {
        if (current.parent == null)
        {
            return current.name;
        }

        return current.parent.GetPath() + "/" + current.name;
    }

    public static Tween AppearLikeFountain(this GameObject childObject,
        GameObject targetParent,
        Vector3 destPosition,
        float duration,
        float jumpHeight,
        AnimationCurve jumpEase)
    {
        bool isAutoAttacted = false;
        var distance = destPosition - childObject.transform.position;
        var agent = targetParent.GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            agent = targetParent.AddComponent<NavMeshAgent>();
            isAutoAttacted = true;
        }

        var moveFactor = distance / duration;

        var sequence = DOTween.Sequence();
        sequence.Append(childObject.transform.DOLocalMoveY(jumpHeight, duration)
            .SetLink(childObject)
            .SetEase(jumpEase)
            .OnUpdate(() => agent.Move(moveFactor * Time.deltaTime))
            .OnComplete(() =>
            {
                if (isAutoAttacted)
                {
                    GameObject.Destroy(agent);
                }
            }));
        return sequence;
    }

    public static List<T1> GetAllObjectsOnlyInScene<T1>() where T1 : Component
    {
        List<T1> result = new List<T1>();
        var components = Resources.FindObjectsOfTypeAll(typeof(T1));
        foreach (UnityEngine.Object co in components)
        {
            Component component = co as Component;
            GameObject go = component.gameObject;
            if (go.scene.name == null) // 씬에 있는 오브젝트가 아니므로 제외한다.
                continue;

            // HideFlags 이용하여 씬에 있는 오브젝트가 아닌경우 제외
            if (go.hideFlags == HideFlags.NotEditable || go.hideFlags == HideFlags.HideAndDontSave || go.hideFlags == HideFlags.HideInHierarchy)
                continue;

            result.Add(component as T1);
        }

        return result;
    }

    public static void StopCo<T>(this T t, Coroutine handle) where T : MonoBehaviour
    {
        if (handle != null)
        {
            t.StopCoroutine(handle);
        }
    }

    public static Coroutine StopAndStartCo<T>(this T t, Coroutine handle, IEnumerator CoroutineFn) where T : MonoBehaviour
    {
        StopCo(t, handle);
        return t.StartCoroutine(CoroutineFn);
    }

    public static void SafeCancelTask(this CancellationTokenSource source)
    {
        if (source == null || source.IsCancellationRequested) return;
        source?.Cancel();
        source?.Dispose();
    }
}