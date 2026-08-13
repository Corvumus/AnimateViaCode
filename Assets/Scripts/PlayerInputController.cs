using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerAttackController))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerJumping))]
public class PlayerInputController : MonoBehaviour
{
    private bool IsCanMove => !attackController.IsAttacking;
    private bool IsCanAttack => !jumping.IsJumping && !attackController.IsAttacking;
    private bool IsCanJump => !jumping.IsJumping && !attackController.IsAttacking;

    private InputAction attackIA;
    private InputAction jumpIA;

    private PlayerAttackController attackController;
    private PlayerMovement movement;
    private PlayerJumping jumping;

    void Awake()
    {
        attackIA = InputSystem.actions.FindAction("Player/Attack");
        attackIA.Enable();

        jumpIA = InputSystem.actions.FindAction("Player/Jump");
        jumpIA.Enable();

        attackController = GetComponent<PlayerAttackController>();
        movement = GetComponent<PlayerMovement>();
        jumping = GetComponent<PlayerJumping>();
    }
    #region Enable/Disable
    private void OnEnable()
    {
        attackIA.performed += AttackIA_performed;
        jumpIA.performed += JumpIA_performed;
    }
    private void OnDisable()
    {
        attackIA.performed -= AttackIA_performed;
        jumpIA.performed -= JumpIA_performed;
    }
    #endregion
    private void JumpIA_performed(InputAction.CallbackContext context)
    {
        if (IsCanJump)
            jumping.Jump();
    }

    private void AttackIA_performed(InputAction.CallbackContext context)
    {
        if (IsCanAttack)
        {
            movement.Stop();
            attackController.Attack();
        }
    }

    void FixedUpdate()
    {
        if (!IsCanMove) return;

        movement.Move();
    }
}
