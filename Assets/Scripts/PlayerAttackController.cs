using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    [SerializeField] private float attackTime;
    public bool IsAttacking { get; private set; }
    public float AttackTime => attackTime;

    public async void Attack()
    {
        IsAttacking = true;

        await Awaitable.WaitForSecondsAsync(attackTime);

        IsAttacking = false;
    }
}
