using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
#if UNITY_EDITOR
#endif

public static partial class Woony
{
    public static T GetRandomItem<T>(this List<T> list)
    {
        if (list.Count == 0)
        {
            Debug.LogError("비어있는 list의 값을 얻으려고 듬.");
            return default(T);
        }

        return list[Random.Range(0, list.Count)];
    }

    public static T GetLastItem<T>(this List<T> list)
    {
        if (list.Count == 0)
        {
            Debug.LogError("비어있는 list의 값을 얻으려고 듬.");
            return default(T);
        }

        return list[list.Count - 1];
    }

    public static T GetLastItem<T>(this T[] array)
    {
        if (array.Length == 0)
        {
            Debug.LogError("비어있는 list의 값을 얻으려고 듬.");
            return default(T);
        }

        return array[array.Length - 1];
    }

    public static T[] Shuffle<T>(this T[] array)
    {
        return Shuffle(array, array.Length);
    }

    public static T[] Shuffle<T>(this T[] array, int range)
    {
        int n = range;
        int m = 0;
        T tempItem;

        while (n > 1)
        {
            n--;
            m = Random.Range(0, n + 1);
            tempItem = array[m];
            array[m] = array[n];
            array[n] = tempItem;
        }

        return array;
    }

    public static List<T> Shuffle<T>(this List<T> list)
    {
        return Shuffle(list, list.Count);
    }

    public static List<T> Shuffle<T>(this List<T> list, int range)
    {
        int n = range;
        int m = 0;
        T tempItem;

        while (n > 1)
        {
            n--;
            m = Random.Range(0, n + 1);
            tempItem = list[m];
            list[m] = list[n];
            list[n] = tempItem;
        }

        return list;
    }

    public static List<T> RemoveAtUnordered<T>(this List<T> list, int index)
    {
        if (list.Count == 0)
        {
            Debug.LogError("비어있는 list의 값을 얻으려고 듬.");
            return list;
        }

        list[index] = list[list.Count - 1];
        list.RemoveAt(list.Count - 1);
        return list;
    }

    public static bool TryGetItemAtIndex<T>(this IList<T> list, int index, out T result)
    {
        var isValid = list.Count > index;
        result = isValid ? list[index] : default;
        return isValid;
    }

    public static Dictionary<TKey, TValue> CheckItemAndSetDefault<TKey, TValue>(this Dictionary<TKey, TValue> dic, TKey key, TValue defaultValue)
    {
        if (!dic.ContainsKey(key)) dic[key] = defaultValue;
        return dic;
    }

    public static T GetRandomItemByProb<T>(this List<T> list1, List<int> probs)
    {
        int randomProb, totalProb = 0;

        probs.ForEach(x => totalProb += x);
        randomProb = Random.Range(0, totalProb);
        int count = probs.Count;
        for (int i = 0; i < count; i++)
        {
            if (probs[i] < randomProb)
            {
                randomProb -= probs[i];
                continue;
            }

            return list1[i];
        }

        return list1[0];
    }
}