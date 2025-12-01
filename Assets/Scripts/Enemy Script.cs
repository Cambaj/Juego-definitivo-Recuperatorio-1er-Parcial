using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class EnemyScript : MonoBehaviour
{
   
        [Header("Health Stats")]
        [SerializeField] private float maxHealth = 100f;
        private float currentHealth;

        [Header("Visual Feedback")]
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Color flashColor = Color.red;
        [SerializeField] private float flashDuration = 0.1f;

        private Color originalColor;
        private Coroutine flashCoroutine;
        private Animator animator; 

        private void Start()
        {
            animator = GetComponent<Animator>();

            if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
            originalColor = spriteRenderer.color;

            currentHealth = maxHealth;
        }

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;

            FlashEffect();

            if (currentHealth <= 0)
            {
                Die();

            }
        }

        private void FlashEffect()
        {
            if (flashCoroutine != null) StopCoroutine(flashCoroutine);
            flashCoroutine = StartCoroutine(FlashRoutine());
        }

        private IEnumerator FlashRoutine()
        {

            spriteRenderer.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            spriteRenderer.color = originalColor;
        }

        private void Die()
        {
            Debug.Log("Enemy killed");
            Destroy(gameObject);

        }

    
}
