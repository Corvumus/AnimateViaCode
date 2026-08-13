using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private float checkRadius = 0.05f;
    public bool IsGrounded { get; private set; }

    void Update()
    {
        IsGrounded = Physics2D.OverlapCircle(transform.position, checkRadius, groundLayerMask);
    }
}
