using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [Header("Animator Reference")]
    [SerializeField] private string Is_movingParameter = "Is_moving";
    [SerializeField] private string Is_idleParameter = "Is_idle";
    [SerializeField] private string Is_attackingParameter = "Is_attacking";
    [SerializeField] private string Is_jumpingParameter = "Is_jumping";
    [SerializeField] private string Is_fallingParameter = "Is_falling";




    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D playerRigidbody;
    [SerializeField] private PlayerAttack PlayerAttack;
    

    private int Is_movingHash;
    private int Is_idleHash;
    private int Is_attackingHash;
    private int Is_jumpingHash;
    private int Is_fallingHash;




    private void Awake()
    {

        Is_movingHash = Animator.StringToHash(Is_movingParameter);
        Is_idleHash = Animator.StringToHash(Is_idleParameter);
        Is_attackingHash = Animator.StringToHash(Is_attackingParameter);
        Is_jumpingHash = Animator.StringToHash(Is_jumpingParameter);
        Is_fallingHash = Animator.StringToHash(Is_fallingParameter);

    }

    public void UpdateAnimation(bool Is_moving, bool Is_idle, bool Is_jumping, bool Is_falling)
    {
        if (PlayerAttack != null && PlayerAttack.Is_attacking)
        {
            animator.SetBool(Is_movingHash, false);
            animator.SetBool(Is_idleHash, false);
            animator.SetBool(Is_jumpingHash, false);
            animator.SetBool(Is_fallingHash, false);
            return;
        }
        animator.SetBool(Is_movingHash, Is_moving);
        animator.SetBool(Is_idleHash, Is_idle);
        animator.SetBool(Is_jumpingHash, Is_jumping);

        bool isFalling = playerRigidbody.linearVelocity.y < -0.1f && !Is_jumping && !Is_moving;
        animator.SetBool(Is_fallingHash, isFalling);
    }

    public void PlayAttack()
    {
        if (animator == null) return;
        animator.SetBool(Is_attackingHash, true);

    }
    public void StopAttack()
    {
        if (animator == null) return;
        animator.SetBool(Is_attackingHash, false);
    }


}
