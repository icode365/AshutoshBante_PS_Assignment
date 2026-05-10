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
            Vector3 currentGravity =
                CurrentGravityDirection;

            Quaternion rotation =
                Quaternion.identity;

            if (input == Vector2.up)
            {
                rotation =
                    Quaternion.AngleAxis(
                        90f,
                        Vector3.right);
            }
            else if (input == Vector2.down)
            {
                rotation =
                    Quaternion.AngleAxis(
                        -90f,
                        Vector3.right);
            }
            else if (input == Vector2.left)
            {
                rotation =
                    Quaternion.AngleAxis(
                        90f,
                        Vector3.forward);
            }
            else if (input == Vector2.right)
            {
                rotation =
                    Quaternion.AngleAxis(
                        -90f,
                        Vector3.forward);
            }

            PendingGravityDirection =
                rotation * currentGravity;

            PendingGravityDirection =
                SnapToWorldAxis(
                    PendingGravityDirection);

            previewVisualizer.UpdatePreview(
                PendingGravityDirection);
        }

        private Vector3 SnapToWorldAxis(
            Vector3 direction)
        {
            direction.Normalize();

            Vector3[] axes =
            {
                Vector3.up,
                Vector3.down,
                Vector3.left,
                Vector3.right,
                Vector3.forward,
                Vector3.back
            };

            float bestDot = -Mathf.Infinity;

            Vector3 bestAxis = Vector3.down;

            foreach (var axis in axes)
            {
                float dot =
                    Vector3.Dot(
                        direction,
                        axis);

                if (dot > bestDot)
                {
                    bestDot = dot;
                    bestAxis = axis;
                }
            }

            return bestAxis;
        }
        
        public void ApplyPendingGravity()
        {
            CurrentGravityDirection = PendingGravityDirection;
            previewVisualizer.HidePreview();
        }
    }
}