using System;
using System.Collections.Generic;
using UnityEngine;

// EX
// animationSystem = new AnimationSystem<NPCAnimationStateType>(GetComponentInChildren<Animator>(true),
//                                                              NPCAnimationStateType.Idle,
//                                                              NPCAnimationStateType.Walk);

// void PlayAnimation(NPCAnimationStateType npcAnimationStateType)
// {
//     switch (npcAnimationStateType)
//     {
//         case NPCAnimationStateType.Idle:
//         case NPCAnimationStateType.Walk:
//             animationSystem.SetTrigger(npcAnimationStateType);
//             break;
//     }
// }

public class AnimatorSystem<T> where T : Enum
{
    private class AnimationMapInfo<U>
    {
        public U customEnum;
        public int hash;
    }

    protected Animator animator;
    private Dictionary<T, int> _animationMap = new Dictionary<T, int>();

    public AnimatorSystem(Animator animator)
    {
        this.animator = animator;
        AnimationMapInfo<T> info;

        var enums = Enum.GetNames(typeof(T));
        for (int i = 0; i < enums.Length; i++)
        {
            info = GetAnimationMapInfo((T)Enum.Parse(typeof(T), enums[i]));
            _animationMap[info.customEnum] = info.hash;
        }
    }

    public AnimatorSystem(Animator animator, params T[] customEnums)
    {
        this.animator = animator;
        AnimationMapInfo<T> info;

        for (int i = 0; i < customEnums.Length; i++)
        {
            info = GetAnimationMapInfo(customEnums[i]);
            _animationMap[info.customEnum] = info.hash;
        }
    }

    private AnimationMapInfo<T> GetAnimationMapInfo(T _customEnum)
    {
        return new AnimationMapInfo<T>()
        {
            customEnum = _customEnum,
            hash = Animator.StringToHash(_customEnum.ToString())
        };
    }

    public void ResetTrigger(T customEnum)
    {
        animator.ResetTrigger(_animationMap[customEnum]);
    }

    public void AllResetTrigger()
    {
        foreach (var item in _animationMap)
        {
            animator.ResetTrigger(item.Value);
        }
    }

    public void SetTrigger(T customEnum)
    {
        animator.SetTrigger(_animationMap[customEnum]);
    }

    public void SetBool(T customEnum, bool value)
    {
        animator.SetBool(_animationMap[customEnum], value);
    }

    public void SetFloat(T customEnum, float value)
    {
        animator.SetFloat(_animationMap[customEnum], value);
    }

    public void SetInteger(T customEnum, int value)
    {
        animator.SetInteger(_animationMap[customEnum], value);
    }

    public bool GetBool(T customEnum)
    {
        return animator.GetBool(_animationMap[customEnum]);
    }
}