using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AbilityData
{
    public float duration;
    public float cooldownTime;
    public List<float> values = new();
}

[CreateAssetMenu(fileName = nameof(AbiltiyDataContainerSO), menuName = "SO" + nameof(AbiltiyDataContainerSO))]
public class AbiltiyDataContainerSO : ScriptableObject
{
    public AbilityData DashData;
    public AbilityData MagneticData;
}