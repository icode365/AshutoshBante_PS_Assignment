using Assignment.Scripts.Gameplay;
using UnityEngine;

namespace Assignment.Scripts.Player.HUD
{
    public class TargetDirectionSolver : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera mainCamera;

        private BoxCollectible currentTarget;

        public bool HasTarget => currentTarget != null;

        public bool IsTargetVisible { get; private set; }

        public Vector3 ViewportPosition { get; private set; }
        public Vector3 ScreenPosition { get; private set; }

        private void OnEnable()
        {
            GlobalEventHandler.OnTargetChanged += SetTarget;
        }

        private void OnDisable()
        {
            GlobalEventHandler.OnTargetChanged -= SetTarget;
        }

        private void Update()
        {
            if (currentTarget == null)
                return;

            SolveTargetVisibility();
        }

        private void SetTarget(BoxCollectible target)
        {
            currentTarget = target;
        }

        private void SolveTargetVisibility()
        {
            ViewportPosition =
                mainCamera.WorldToViewportPoint(
                    currentTarget.transform.position);
            
            ScreenPosition =
                mainCamera.WorldToScreenPoint(
                    currentTarget.transform.position);

            IsTargetVisible =
                ViewportPosition.z > 0 &&
                ViewportPosition.x > 0 &&
                ViewportPosition.x < 1 &&
                ViewportPosition.y > 0 &&
                ViewportPosition.y < 1;
        }

        public Vector3 GetViewportPosition()
        {
            return ViewportPosition;
        }
    }
}