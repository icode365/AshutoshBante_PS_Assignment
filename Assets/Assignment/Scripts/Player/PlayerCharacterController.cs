using Assignment.Scripts.Player.Camera;
using UnityEngine;
using Assignment.Scripts.Player.Gravity;

namespace Assignment.Scripts.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerCharacterController : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private Transform visuals;
        [SerializeField] private Transform cameraTransform;

        private ThirdPersonCameraController _cameraController;
        private CharacterController _characterController;
        private GravityController _gravityController;
        private PlayerInputHandler _inputHandler;
        private GroundDetector _groundDetector;

        [Header("Movement")] 
        [SerializeField] private float jumpForce = 8f;
        [SerializeField] private float rotationSpeed = 10f;
        [SerializeField] private float groundedGravityForce = 2f;
        [SerializeField] private float moveSpeed = 5f;
        
        private bool _isJumping;
        private Vector3 _velocity;
        private Vector3 _moveVelocity;
        private Vector3 _gravityVelocity;

        private void Awake()
        {
            _cameraController =
                GetComponentInChildren<ThirdPersonCameraController>();

            _characterController =
                GetComponent<CharacterController>();

            _gravityController =
                GetComponent<GravityController>();

            _inputHandler =
                GetComponent<PlayerInputHandler>();

            _groundDetector =
                GetComponentInChildren<GroundDetector>();
        }

        private void Update()
        {
            HandleMovement();
            ApplyGravity();
            MoveCharacter();
            AlignToGravity();
            RotateVisuals();
        }

        private void LateUpdate()
        {
            UpdateCameraPosition();
        }

        private void OnEnable()
        {
            AddMovementInputListeners();

            AddGravityInputListeners();
        }

        private void OnDisable()
        {
            RemoveMovementInputListeners();

            RemoveGravityInputListeners();
        }

        private void AddGravityInputListeners()
        {
            _inputHandler.OnGravityDirectionChanged +=
                _gravityController.SetPendingGravity;

            _inputHandler.OnApplyGravity +=
                _gravityController.ApplyPendingGravity;
        }

        private void AddMovementInputListeners()
        {
            _inputHandler.OnJumpPressed += Jump;
        }

        private void RemoveGravityInputListeners()
        {
            _inputHandler.OnGravityDirectionChanged -=
                _gravityController.SetPendingGravity;

            _inputHandler.OnApplyGravity -=
                _gravityController.ApplyPendingGravity;
        }

        private void RemoveMovementInputListeners()
        {
            _inputHandler.OnJumpPressed -= Jump;
        }

        private void HandleMovement()
        {
            Vector2 input =
                _inputHandler.MoveInput;

            Vector3 gravityDirection =
                _gravityController.CurrentGravityDirection;

            Vector3 cameraForward =
                Vector3.ProjectOnPlane(
                    cameraTransform.forward,
                    gravityDirection).normalized;

            Vector3 cameraRight =
                Vector3.ProjectOnPlane(
                    cameraTransform.right,
                    gravityDirection).normalized;

            Vector3 moveDirection =
                cameraForward * input.y +
                cameraRight * input.x;

            moveDirection.Normalize();

            _moveVelocity =
                moveDirection * moveSpeed;
        }

        private void ApplyGravity()
        {
            if (_groundDetector.IsGrounded && !_isJumping)
            {
                _gravityVelocity =
                    _gravityController.CurrentGravityDirection *
                    groundedGravityForce;
            }
            else
            {
                _gravityVelocity +=
                    _gravityController.GravityForce *
                    Time.deltaTime;
            }
            
            CheckJumpExit();
        }
        
        private void CheckJumpExit()
        {
            Vector3 gravityDirection =
                _gravityController.CurrentGravityDirection;

            float verticalVelocity =
                Vector3.Dot(
                    _gravityVelocity,
                    -gravityDirection);

            if (verticalVelocity <= 0f)
            {
                _isJumping = false;
            }
        }

        private void MoveCharacter()
        {
            Vector3 finalVelocity =
                _moveVelocity + _gravityVelocity;

            _characterController.Move(
                finalVelocity * Time.deltaTime);
        }

        private void Jump()
        {
            if (!_groundDetector.IsGrounded)
                return;

            _isJumping = true;
            
            _gravityVelocity =
                -_gravityController.CurrentGravityDirection *
                jumpForce;
        }

        private void AlignToGravity()
        {
            Vector3 gravityUp =
                -_gravityController.CurrentGravityDirection;

            Quaternion targetRotation =
                Quaternion.FromToRotation(
                    transform.up,
                    gravityUp) * transform.rotation;

            transform.rotation =
                Quaternion.Slerp(
                    transform.rotation,
                    targetRotation,
                    rotationSpeed * Time.deltaTime);
        }

        private void RotateVisuals()
        {
            Vector3 planarMove =
                Vector3.ProjectOnPlane(
                    _moveVelocity,
                    _gravityController.CurrentGravityDirection);

            if (planarMove.sqrMagnitude < 0.01f)
                return;

            Quaternion targetRotation =
                Quaternion.LookRotation(
                    planarMove,
                    transform.up);

            visuals.rotation =
                Quaternion.Slerp(
                    visuals.rotation,
                    targetRotation,
                    rotationSpeed * Time.deltaTime);
        }

        private void UpdateCameraPosition()
        {
            _cameraController.RotateCamera(
                _inputHandler.CurrentLookInput);
        }
    }
}