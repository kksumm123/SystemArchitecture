using PhaseArchitecture;
using PlayerSystem;
using UnityEngine;

enum PlayerAniParams
{
    None,
    MoveFactor,
    Jump, Fall,
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

    public void Initialize()
    {
        Woony.Asserts(this,
            (animator, nameof(animator)),
            (boxCollider2D, nameof(boxCollider2D)),
            (rigid2D, nameof(rigid2D)));

        rigid2D.position = startPosition;

        _animatorSystem = new(animator);
        moveSystem.Initialize(
            rigid2D,
            IsMovable,
            x => _animatorSystem.SetFloat(PlayerAniParams.MoveFactor, x));
        physicsSystem.Initialize(rigid2D, boxCollider2D);

        enabled = true;
    }

    private bool IsMovable()
    {
        return physicsSystem.IsGround();
    }

    private void Update()
    {
        if (PhaseManager.CurrentPhaseType != EPhaseType.Stage) return;

        moveSystem.CustomUpdate();
        physicsSystem.CustomUpdate();
    }

    public void OnLeaveStage()
    {
        enabled = false;
    }
}