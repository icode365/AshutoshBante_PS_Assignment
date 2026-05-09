using UnityEngine;
using System;

namespace Assignment.Scripts.Player
{
    public class PlayerInputHandler : MonoBehaviour
    {
        public Vector2 MoveInput { get; private set; }
        public Vector2 GravityInput { get; private set; }

        public event Action OnJumpPressed;
        public event Action<Vector2> OnGravityDirectionChanged;
        public event Action OnApplyGravity;

        public Vector2 CurrentLookInput { get; private set; }
        public event Action<Vector2> OnLookInputChanged;
        
        private PlayerControls controls;

        private void Awake()
        {
            controls = new PlayerControls();

            controls.Gameplay.Move.performed += ctx =>
                MoveInput = ctx.ReadValue<Vector2>();

            controls.Gameplay.Move.canceled += ctx =>
                MoveInput = Vector2.zero;

            controls.Gameplay.Jump.performed += _ =>
                OnJumpPressed?.Invoke();

            controls.Gameplay.GravityDirection.performed += ctx =>
            {
                GravityInput = ctx.ReadValue<Vector2>();
                OnGravityDirectionChanged?.Invoke(GravityInput);
            };

            controls.Gameplay.GravityDirection.canceled += _ =>
                GravityInput = Vector2.zero;

            controls.Gameplay.ApplyGravity.performed += _ =>
                OnApplyGravity?.Invoke();

            controls.Gameplay.Look.performed += ctx =>
            {
                CurrentLookInput = ctx.ReadValue<Vector2>();
                OnLookInputChanged?.Invoke(CurrentLookInput);
            };

            controls.Gameplay.Look.canceled += _ =>
            {
                CurrentLookInput = Vector2.zero;
                OnLookInputChanged?.Invoke(CurrentLookInput);
            };
        }

        private void OnEnable()
        {
            controls.Enable();
        }

        private void OnDisable()
        {
            controls.Disable();
        }
    }
}