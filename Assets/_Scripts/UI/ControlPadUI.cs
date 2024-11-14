using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ControlPadUI : BaseUI
{
    [SerializeField] private Button jumpButton;
    [SerializeField] private Button downButton;

    [Header("어빌리티"), SerializeField] private Button dashButton;
    [SerializeField] private Button magnetButton;

    protected override void OnInitialize()
    {
        Woony.Asserts(this,
            (jumpButton, nameof(jumpButton)),
            (downButton, nameof(downButton)),
            (dashButton, nameof(dashButton)),
            (magnetButton, nameof(magnetButton)));
    }

    public void AddJumpClickAction(UnityAction action)
    {
        jumpButton?.onClick.AddListener(action);
    }

    public void AddDownClickAction(UnityAction action)
    {
        downButton?.onClick.AddListener(action);
    }

    public void AddDashClickAction(UnityAction action)
    {
        dashButton?.onClick.AddListener(action);
    }

    public void AddMagnetClickAction(UnityAction action)
    {
        magnetButton?.onClick.AddListener(action);
    }
}