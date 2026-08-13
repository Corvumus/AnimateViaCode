using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerAttackController))]
public class PlayerAnimationController : MonoBehaviour
{
    private readonly int run = Animator.StringToHash("Run");
    private readonly int idle = Animator.StringToHash("Idle");
    private readonly int jump = Animator.StringToHash("Jump");
    private readonly int fall = Animator.StringToHash("Fall");
    private readonly int attack = Animator.StringToHash("Attack");

    private PlayerAttackController attackController;
    private GroundCheck groundCheck;
    private Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private float HorizontalSpeed => rigidbody2D.linearVelocityX;
    private float VerticalSpeed => rigidbody2D.linearVelocityY;
    private bool IsOnGround => groundCheck.IsGrounded;
    private bool IsAttacking => attackController.IsAttacking;

    //Длительность анимации атаки
    private float AttackTime => attackController.AttackTime;

    private int currentState;
    //Время, до наступления которого переключение анимаций заблокировано
    private float lockedTill;

    private void Awake()
    {
        attackController = GetComponent<PlayerAttackController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        groundCheck = GetComponentInChildren<GroundCheck>();
    }

    private void Update()
    {
        Flip();

        int state = GetState();

        if (state == currentState) return;
        //Переключаем состояние
        animator.CrossFade(state, 0);
        currentState = state;
    }

    private int GetState()
    {
        if (Time.time < lockedTill) return currentState;

        if (IsAttacking) return LockState(attack, AttackTime);
        if (IsOnGround) return Mathf.Abs(HorizontalSpeed) < 0.01f ? idle : run;
        return VerticalSpeed > 0 ? jump : fall;
    }

    private int LockState(int state, float time)
    {
        lockedTill = Time.time + time;
        return state;
    }

    private void Flip()
    {
        if (Mathf.Abs(HorizontalSpeed) < 0.01f) return;

        if (HorizontalSpeed < 0)
            spriteRenderer.flipX = true;
        else spriteRenderer.flipX = false;
    }
}
