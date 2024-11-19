using PhaseArchitecture;
using PlayerSystem;
using UnityEngine;

enum PlayerAniParams
{
    None,
    MoveFactor,
    Jump, Fall, OnHitGround,
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private Rigidbody2D rigid2D;

    [SerializeField] private Vector3 startPosition = new Vector3(-7.1f, 0, 0);

    private AnimatorSystem<PlayerAniParams> _animatorSystem;
    [SerializeField] private MoveSystem moveSystem = new();
    [SerializeField] private PhysicsSystem physicsSystem = new();
    [SerializeField] private JumpSystem jumpSystem = new();

    public void Initialize()
    {
        Woony.Asserts(this,
            (animator, nameof(animator)),
            (boxCollider2D, nameof(boxCollider2D)),
            (rigid2D, nameof(rigid2D)));

        transform.position = rigid2D.position = startPosition;

        // Init Systems
        _animatorSystem = new(animator);
        moveSystem.Initialize(
            rigid2D,
            IsMovable,
            x => _animatorSystem.SetFloat(PlayerAniParams.MoveFactor, x));
        physicsSystem.Initialize(
            rigid2D,
            boxCollider2D,
            () =>
            {
                jumpSystem.OnGround();
                _animatorSystem.SetTrigger(PlayerAniParams.OnHitGround);
            },
            () => _animatorSystem.SetTrigger(PlayerAniParams.Fall));
        jumpSystem.Initialize(
            rigid2D,
            () =>
            {
                physicsSystem.ClearFallFactor();
                _animatorSystem.SetTrigger(PlayerAniParams.Jump);
            });

        // Bind control key
        var controlPad = UIManager.Instance.GetUI<ControlPadUI>(UIParentType.Under);
        controlPad.ShowUI();
        controlPad.AddJumpClickAction(jumpSystem.Jump);

        enabled = true;
    }

    private bool IsMovable()
    {
        return physicsSystem.IsGround();
    }

    private void FixedUpdate()
    {
        if (PhaseManager.CurrentPhaseType != EPhaseType.Stage) return;

        moveSystem.CustomFixedUpdate();
        physicsSystem.CustomFixedUpdate(jumpSystem.IsJumpEnd);
    }

    public void OnLeaveStage()
    {
        enabled = false;
    }
}