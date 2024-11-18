using System.Collections.Generic;
using UnityEngine;

public enum MapType
{
    MapStart,
    Map1, Map2, Map3, Map4, Map5,
    MapEnd,
}

public class MapController : MonoSingleton<MapController>
{
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private int mapCount = 20;
    [SerializeField] private Transform mapParent;
    [SerializeField] private float mapGapValue = 18;

    private List<RecycleObject> _maps = new();

    public void Initialize()
    {
        enabled = false;
        transform.position = Vector3.zero;
        ClearMap();
        Generate();
    }

    private void Generate()
    {
        var mapStart = FactoryManager.Instance.Map.GetObject(MapType.MapStart);
        SetMap(0, mapStart);

        for (int i = 0; i < mapCount; i++)
        {
            var newMap = FactoryManager.Instance.Map.GetObject((MapType)Random.Range(0, 5));
            SetMap(i + 1, newMap);
        }

        var mapEnd = FactoryManager.Instance.Map.GetObject(MapType.MapEnd);
        SetMap(mapCount + 1, mapEnd);

        void SetMap(int i, RecycleObject newMap)
        {
            if (newMap == null) return;

            newMap.transform.SetParent(mapParent);
            newMap.transform.localPosition = new Vector3(i * mapGapValue, 0);
            _maps.Add(newMap);
        }
    }

    public void ClearMap()
    {
        _maps?.ForEach(x => x.Restore());
        _maps?.Clear();
    }

    private void Update()
    {
        transform.position += Mathf.Abs(moveSpeed) * Time.deltaTime * Vector3.left;
    }

    public void StartMove()
    {
        enabled = true;
    }

    public void StopMove()
    {
        enabled = false;
    }
}