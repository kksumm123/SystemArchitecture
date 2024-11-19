using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AbiltyButton : MonoBehaviour
{
    [SerializeField] private Image cooldownIcon;
    [SerializeField] private Button button;

    private float _cooldownTime;

    private void Awake()
    {
        Woony.Asserts(this,
            (cooldownIcon, nameof(cooldownIcon)),
            (button, nameof(button)));

        button?.onClick.AddListener(() => ProcessCooldown().Forget());
    }

    public void AddAbilityAction(UnityAction action, float cooldownTime)
    {
        _cooldownTime = cooldownTime;
        button?.onClick.AddListener(action);
    }

    private async UniTaskVoid ProcessCooldown()
    {
        button?.SafeInteractable(false);
        
        cooldownIcon.DOKill();
        cooldownIcon.fillAmount = 0;
        cooldownIcon.DOFillAmount(1, _cooldownTime);
        await UniTask.Delay(TimeSpan.FromSeconds(_cooldownTime));
        button?.SafeInteractable(true);
    }
}