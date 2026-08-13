using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    private InputAction moveIA;
    private Rigidbody2D rigidbody2D;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        moveIA = InputSystem.actions.FindAction("Player/Move");
        moveIA.Enable();
    }

    public void Move()
    {
        float horizontalVelocity = moveIA.ReadValue<float>() * speed;
        rigidbody2D.linearVelocityX = horizontalVelocity;
    }

    public void Stop() => rigidbody2D.linearVelocityX = 0;
}
