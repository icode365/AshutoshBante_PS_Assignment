using Assignment.Scripts.Player.Camera;
using UnityEngine;
using Assignment.Scripts.Player.Gravity;

namespace Assignment.Scripts.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerCharacterController : MonoBehaviour
    {
        [Header("References")] [SerializeField]
        private Transform cameraTransform;

        private ThirdPersonCameraController _cameraController;
        private CharacterController _characterController;
        private GravityController _gravityController;
        private PlayerInputHandler _inputHandler;
        private GroundDetector _groundDetector;

        [Header("Movement")] [SerializeField] private float moveSpeed = 5f;

        [SerializeField] private float jumpForce = 8f;
        [SerializeField] private float rotationSpeed = 10f;

        private Vector3 _velocity;

        private void Awake()
        {
            _cameraController = GetComponentInChildren<ThirdPersonCameraController>();
            _characterController = GetComponent<CharacterController>();
            _gravityController = GetComponent<GravityController>();
            _inputHandler = GetComponent<PlayerInputHandler>();
            _groundDetector = GetComponent<GroundDetector>();
        }


        private void Update()
        {
            HandleMovement();
            ApplyGravity();
            AlignToGravity();
        }

        public void LateUpdate()
        {
            UpdateCameraPosition();
        }

        private void OnEnable()
        {
            AddMovementInputListners();
            AddGravityInputListeners();
        }

        private void OnDisable()
        {
            RemoveMovementInputListners();
            RemoveGravityInputListeners();
        }

        private void AddGravityInputListeners()
        {
            _inputHandler.OnGravityDirectionChanged += _gravityController.SetPendingGravity;
            _inputHandler.OnApplyGravity += _gravityController.ApplyPendingGravity;
        }

        private void AddMovementInputListners()
        {
            _inputHandler.OnJumpPressed += Jump;
        }

        private void RemoveGravityInputListeners()
        {
            _inputHandler.OnJumpPressed -= Jump;
        }

        private void RemoveMovementInputListners()
        {
            _inputHandler.OnGravityDirectionChanged -= _gravityController.SetPendingGravity;
            _inputHandler.OnApplyGravity -= _gravityController.ApplyPendingGravity;
        }

        public void HandleMovement()
        {
            Vector2 input = _inputHandler.MoveInput;

            Vector3 camForward = cameraTransform.forward;
            Vector3 camRight = cameraTransform.right;

            camForward.y = 0;
            camRight.y = 0;

            camForward.Normalize();
            camRight.Normalize();

            Vector3 moveDirection =
                camForward * input.y +
                camRight * input.x;

            _characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

            if (moveDirection.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation =
                    Quaternion.LookRotation(moveDirection);

                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    targetRotation,
                    rotationSpeed * Time.deltaTime);
            }
        }

        private void ApplyGravity()
        {
            _velocity += _gravityController.GravityForce * Time.deltaTime;

            _characterController.Move(_velocity * Time.deltaTime);

            if (_groundDetector.IsGrounded)
            {
                _velocity = Vector3.zero;
            }
        }

        private void Jump()
        {
            if (!_groundDetector.IsGrounded)
                return;

            _velocity = -_gravityController.CurrentGravityDirection * jumpForce;
        }

        private void AlignToGravity()
        {
            Quaternion targetRotation =
                Quaternion.FromToRotation(
                    transform.up,
                    -_gravityController.CurrentGravityDirection
                ) * transform.rotation;

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime);
        }
        
        private void UpdateCameraPosition()
        {
            _cameraController.RotateCamera(_inputHandler.CurrentLookInput);
        }
    }
}