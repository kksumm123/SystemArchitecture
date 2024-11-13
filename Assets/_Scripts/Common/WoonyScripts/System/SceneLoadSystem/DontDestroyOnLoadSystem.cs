using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DontDestroyOnLoadSystem
{
    private static List<GameObject> dontDestroyOnLoadObjects = new List<GameObject>();

    public static void AddDontDestroyOnLoad(GameObject gameObject)
    {
        dontDestroyOnLoadObjects.Add(gameObject);
        GameObject.DontDestroyOnLoad(gameObject);
    }

    public static void DestroyObject(GameObject gameObject)
    {
        if (dontDestroyOnLoadObjects.Contains(gameObject))
        {
            GameObject.Destroy(gameObject);
            dontDestroyOnLoadObjects.Remove(gameObject);
        }
    }

    public static void DestroyAll()
    {
        for (int i = 0; i < dontDestroyOnLoadObjects.Count; i++)
        {
            GameObject.Destroy(dontDestroyOnLoadObjects[i]);
        }
        dontDestroyOnLoadObjects.Clear();
    }
}
