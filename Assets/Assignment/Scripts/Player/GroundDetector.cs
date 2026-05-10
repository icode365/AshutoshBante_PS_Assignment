using Assignment.Scripts.Player.Gravity;
using UnityEngine;

namespace Assignment.Scripts.Player
{
    public class GroundDetector : MonoBehaviour
    {
        [Header("References")] [SerializeField]
        private Transform groundCheckOrigin;

        [SerializeField] private GravityController gravityController;

        [Header("Ground Check")] [SerializeField]
        private float sphereRadius = 0.3f;

        [SerializeField] private float checkDistance = 0.6f;

        [SerializeField] private LayerMask groundLayer;

        public bool IsGrounded { get; private set; }

        public Vector3 GroundNormal { get; private set; }

        public RaycastHit GroundHit { get; private set; }

        private void Update()
        {
            PerformGroundCheck();
        }

        private void PerformGroundCheck()
        {
            Vector3 gravityDirection =
                gravityController.CurrentGravityDirection;

            bool hit =
                Physics.SphereCast(
                    groundCheckOrigin.position,
                    sphereRadius,
                    gravityDirection,
                    out RaycastHit hitInfo,
                    checkDistance,
                    groundLayer);

            IsGrounded = hit;

            if (hit)
            {
                GroundHit = hitInfo;

                GroundNormal = hitInfo.normal;
            }
            else
            {
                GroundNormal = Vector3.up;
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (groundCheckOrigin == null)
                return;

            Gizmos.color =
                IsGrounded ? Color.green : Color.red;

            Vector3 direction =
                gravityController != null
                    ? gravityController.CurrentGravityDirection
                    : Vector3.down;

            Gizmos.DrawWireSphere(
                groundCheckOrigin.position +
                direction * checkDistance,
                sphereRadius);
        }
    }
}