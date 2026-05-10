using UnityEngine;

namespace Assignment.Scripts.Player.Animation
{
    public class PlayerAnimationHandler : MonoBehaviour
    {
        [Header("References")] [SerializeField]
        private Animator animator;

        [SerializeField] private GroundDetector groundDetector;
        [SerializeField] private PlayerInputHandler inputHandler;
        [Header("Movement")] [SerializeField] private float fallingVelocityThreshold = -2f;

        private static readonly int IsIdle =
            Animator.StringToHash("IsIdle");

        private static readonly int IsRunning =
            Animator.StringToHash("IsRunning");

        private static readonly int IsFalling =
            Animator.StringToHash("IsFalling");

        private void Update()
        {
            HandleMovementAnimations();
            HandleFallingAnimation();
        }

        private void HandleMovementAnimations()
        {
            Vector2 moveInput =
                inputHandler.MoveInput;

            bool hasMovementInput =
                moveInput.sqrMagnitude > 0.01f;

            bool isGrounded =
                groundDetector.IsGrounded;

            bool isRunning =
                hasMovementInput && isGrounded;

            bool isIdle =
                !hasMovementInput && isGrounded;

            animator.SetBool(IsRunning, isRunning);
            animator.SetBool(IsIdle, isIdle);
        }

        private void HandleFallingAnimation()
        {
            bool isFalling =
                !groundDetector.IsGrounded;

            animator.SetBool(IsFalling, isFalling);
        }
    }
}