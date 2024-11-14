using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public static class AbilityTable
{
    public static Dictionary<AbilityType, AbilityBase> abilityMap = new Dictionary<AbilityType, AbilityBase>();
    public static AbilityBase GetAblity(this AbilityType abilityType)
    {
        abilityMap[AbilityType.Magnetic] = MagneticAbility.Instance;
        abilityMap[AbilityType.Dash] = DashAbility.Instance;
        return abilityMap[abilityType];
    }
}

public abstract class AbilityBase : MonoBehaviour
{
    public virtual AbilityType GetAbilityType()
    {
        return AbilityType.None;
    }

    public abstract void Activate();
    public abstract void DeActivate();
}
