using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] int mapCount = 20;
    [SerializeField] Transform mapParent;
    [SerializeField] float mapGapValue = 18;
    [SerializeField] List<Transform> maps = new List<Transform>();
    [SerializeField] Transform mapEnd;
    void Awake()
    {
        ClearMap();
        InitMap();
    }

    List<Transform> generatedMaps = new List<Transform>();
    [ContextMenu("¸Ê »ý¼º")]
    void InitMap()
    {
        if (maps.Count == 0)
            Debug.LogError("¸ÊÀÌ ¾ø¾î!");
        else if (mapEnd == null)
            Debug.LogError("¸¶Áö¸· ¸ÊÀÌ ¾÷¼­!");

        Vector3 mapPos;
        mapPos = Vector3.zero;
        for (int i = 0; i < mapCount; i++)
        {
            // ¸Ê ·£´ýÀ¸·Î ¼±ÅÃ, º¹»ç & ¹èÄ¡
            var newMapGo = Instantiate(maps[Random.Range(0, maps.Count)], mapParent);
            mapPos.x += mapGapValue;
            newMapGo.position = mapPos;
            generatedMaps.Add(newMapGo);
        }
        var mapEndGo = Instantiate(mapEnd, mapParent);
        mapPos.x += mapGapValue;
        mapEndGo.position = mapPos;
        generatedMaps.Add(mapEndGo);
    }
    [ContextMenu("¸Ê Á¦°Å")]
    void ClearMap()
    {
        if (Application.isPlaying) 
            generatedMaps.ForEach(x => Destroy(x.gameObject));
        else
            generatedMaps.ForEach(x => DestroyImmediate(x.gameObject));

        generatedMaps.Clear();
    }
}
