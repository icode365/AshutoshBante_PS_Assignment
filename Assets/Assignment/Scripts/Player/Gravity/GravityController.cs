using UnityEngine;

namespace Assignment.Scripts.Player.Gravity
{
    public class GravityController : MonoBehaviour
    {
        [SerializeField] private float gravityStrength = 20f;
        [SerializeField] private GravityPreviewVisualizer previewVisualizer;

        public Vector3 CurrentGravityDirection { get; private set; } = Vector3.down;
        public Vector3 PendingGravityDirection { get; private set; } = Vector3.down;

        public Vector3 GravityForce => CurrentGravityDirection * gravityStrength;
        
        public void SetPendingGravity(Vector2 input)
        {
            if (input == Vector2.up)
                PendingGravityDirection = Vector3.forward;

            else if (input == Vector2.down)
                PendingGravityDirection = Vector3.back;

            else if (input == Vector2.left)
                PendingGravityDirection = Vector3.left;

            else if (input == Vector2.right)
                PendingGravityDirection = Vector3.right;
            
            previewVisualizer.UpdatePreview(PendingGravityDirection);
        }

        public void ApplyPendingGravity()
        {
            CurrentGravityDirection = PendingGravityDirection;
            previewVisualizer.HidePreview();
        }
    }
}