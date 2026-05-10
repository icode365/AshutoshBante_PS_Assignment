using UnityEngine;

namespace Assignment.Scripts.Player.Gravity
{
    public class GravityController : MonoBehaviour
    {
        [SerializeField] private float gravityStrength = 20f;
        [SerializeField] private GravityPreviewVisualizer previewVisualizer;
        [SerializeField] private Transform playerVisuals;
        public Vector3 CurrentGravityDirection { get; private set; } = Vector3.down;
        public Vector3 PendingGravityDirection { get; private set; } = Vector3.down;

        public Vector3 GravityForce => CurrentGravityDirection * gravityStrength;

        public void SetPendingGravity(Vector2 input)
        {
            Vector3 currentUp =
                -CurrentGravityDirection;

            Vector3 currentRight =
                Vector3.Cross(
                    playerVisuals.forward,
                    currentUp).normalized;

            Vector3 currentForward =
                Vector3.Cross(
                    currentUp,
                    currentRight).normalized;

            if (input == Vector2.up)
            {
                PendingGravityDirection =
                    -currentForward;
            }
            else if (input == Vector2.down)
            {
                PendingGravityDirection =
                    currentForward;
            }
            else if (input == Vector2.left)
            {
                PendingGravityDirection =
                    -currentRight;
            }
            else if (input == Vector2.right)
            {
                PendingGravityDirection =
                    currentRight;
            }

            PendingGravityDirection =
                PendingGravityDirection.normalized;

            previewVisualizer.UpdatePreview(
                PendingGravityDirection);
        }

        public void ApplyPendingGravity()
        {
            CurrentGravityDirection = PendingGravityDirection;
            previewVisualizer.HidePreview();
        }
    }
}