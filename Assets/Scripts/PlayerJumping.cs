using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerJumping : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    private GroundCheck groundCheck;
    private Rigidbody2D rigidbody2D;

    public bool IsJumping => !groundCheck.IsGrounded;

    private void Awake()
    {
        groundCheck = GetComponentInChildren<GroundCheck>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Jump()
    {
        rigidbody2D.linearVelocityY = jumpForce;
    }
}
