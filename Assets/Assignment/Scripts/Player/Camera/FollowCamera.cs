using UnityEngine;

namespace Assignment.Scripts.Player.Camera
{
    public class ThirdPersonCameraController : MonoBehaviour
    {
        [Header("Target")] [SerializeField] private Transform target;

        [Header("Follow")] [SerializeField] private Vector3 offset = new Vector3(0, 3, -6);
        [SerializeField] private float followSmoothness = 10f;

        [Header("Rotation")] [SerializeField] private float rotationSpeed = 120f;

        [Header("Pitch")] [SerializeField] private float minPitch = -30f;
        [SerializeField] private float maxPitch = 60f;

        private float _yaw;
        private float _pitch;

        private void LateUpdate()
        {
            HandleRotation();
            HandleFollow();
        }

        private void HandleRotation()
        {
            _pitch = Mathf.Clamp(_pitch, minPitch, maxPitch);
        }

        private void HandleFollow()
        {
            Quaternion rotation =
                Quaternion.Euler(_pitch, _yaw, 0);

            Vector3 desiredPosition =
                target.position + rotation * offset;

            transform.position = Vector3.Lerp(
                transform.position,
                desiredPosition,
                followSmoothness * Time.deltaTime);

            transform.LookAt(target.position + Vector3.up * 1.5f);
        }

        public void RotateCamera(Vector2 lookInput)
        {
            _yaw += lookInput.x * rotationSpeed * Time.deltaTime;
            _pitch -= lookInput.y * rotationSpeed * Time.deltaTime;
        }
    }
}