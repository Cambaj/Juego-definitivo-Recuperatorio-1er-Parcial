using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


public class PlayerMovment : MonoBehaviour
{
        [Header("References")]
        
        [SerializeField] private InputActionReference moveAction;
        [SerializeField] private InputActionReference jumpAction;
       
        
        [SerializeField] private Rigidbody2D playerRigidbody;
        [SerializeField] private SpriteRenderer spriteRenderer;

        [Header("Animator Controller")]
        [SerializeField] private PlayerAnimator playerAnimator;
        [SerializeField] private PlayerAttack playerAttack;  

        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 5f;              
        [SerializeField] private float jumpForce = 10f;

        [Header("Ground Checking")]
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundCheckRadius = 0.3f;
        [SerializeField] private LayerMask groundLayer;

        [Header("Inputs")]
        private Vector2 moveInput;

        
        [Header("Player States")]
        public bool Is_moving;
        public bool Is_idle;
        public bool Is_attacking;
        public bool Is_jumping;
        public bool Is_falling;


        private void OnEnable()
        {
            moveAction.action.Enable();
            jumpAction.action.Enable();

            jumpAction.action.started += HandleJumpInput;
        }

        private void OnDisable()
        {
            moveAction.action.Disable();
            jumpAction.action.Disable();

            jumpAction.action.started -= HandleJumpInput;

        }
        


        private void Update() 
        {
           
          
            moveInput = moveAction.action.ReadValue<Vector2>();

            bool grounded = IsGrounded();

            
            Is_moving = Mathf.Abs(moveInput.x) > 0.1f && !Is_attacking;
            Is_idle = !Is_moving && !Is_jumping && !Is_attacking;

            if (grounded && Is_jumping)
                Is_jumping = false;

           
            if (moveInput.x > 0)
                spriteRenderer.flipX = false;
            else if (moveInput.x < 0)
                spriteRenderer.flipX = true;

            playerAnimator.UpdateAnimation(Is_moving, Is_idle, Is_jumping, Is_falling);

        }
        private void FixedUpdate()
        {
            if (!Is_attacking)
                playerRigidbody.linearVelocity = new Vector2(moveInput.x * moveSpeed, playerRigidbody.linearVelocity.y);
        }


        private void HandleJumpInput(InputAction.CallbackContext context)
        {
            Debug.Log("Jump input detected");
            if (IsGrounded())
            {
                playerRigidbody.linearVelocity = new Vector2(playerRigidbody.linearVelocity.x, jumpForce);

                Is_jumping = true;
                Is_idle = false;
                Is_moving = false;
            }

        }
        private bool IsGrounded()
        {
            return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        }


    }
