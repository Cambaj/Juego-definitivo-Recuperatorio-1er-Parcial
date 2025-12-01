using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;



public class PlayerAttack : MonoBehaviour
{
  
[Header("References")]
    [SerializeField] private InputActionReference attackAction;
   
    [SerializeField] private Rigidbody2D playerRigidbody;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private PlayerAnimator playerAnimator;

    [Header("Attack Settings")]
    [SerializeField] private Transform attackController;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackDamage;
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private float attackDuration = 0.3f;

    [SerializeField] private float timeToHit = 0.1f;

    [SerializeField] private LayerMask enemyLayers;


    [Header("Player State")]
    public bool Is_attacking { get; private set; }
    private bool canAttack = true;

   
    private void OnEnable()
    {
        if (attackAction != null)

            attackAction.action.Enable();
        attackAction.action.started += OnAttack;

    }

    private void OnDisable()
    {
        if (attackAction != null)
            attackAction.action.Disable();
        attackAction.action.started -= OnAttack;
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        Debug.Log("Attack input received");
        if (!canAttack) return;
        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {

        canAttack = false;
        Is_attacking = true;

        if (playerAnimator != null)
        {
            playerAnimator.PlayAttack();
        }
        yield return new WaitForSeconds(timeToHit);
        try
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackController.position, attackRange, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy.TryGetComponent(out EnemyScript enemyHealth))
                {
                    enemyHealth.TakeDamage(attackDamage);
                    Debug.Log("You hit" + enemy.name); 
                }

            }
        }

        catch (System.Exception)
        {
            Debug.LogError("Error calculating damage");
        }

        yield return new WaitForSeconds(attackDuration);

        Is_attacking = false;

        if (playerAnimator != null)
        {
            playerAnimator.StopAttack();

        }

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackController == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackController.position, attackRange);
    }

}
