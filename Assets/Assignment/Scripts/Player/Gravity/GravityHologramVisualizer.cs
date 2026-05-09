using UnityEngine;

namespace Assignment.Scripts.Player.Gravity
{
    public class GravityPreviewVisualizer : MonoBehaviour
    {
        [SerializeField] private GravityController gravityController;
        [SerializeField] private Transform player;
        [SerializeField] private GameObject hologram;

        [SerializeField] private float previewDistance = 1.5f;

        public void UpdatePreview(Vector3 pendingGravity)
        {
            if (pendingGravity == gravityController.CurrentGravityDirection)
            {
                hologram.SetActive(false);
                return;
            }

            hologram.SetActive(true);

            Vector3 hologramUp = -pendingGravity;

            Quaternion targetRotation =
                Quaternion.FromToRotation(
                    Vector3.up,
                    hologramUp);

            hologram.transform.rotation = targetRotation;

            Vector3 previewPosition =
                player.position + pendingGravity * previewDistance;

            hologram.transform.position = previewPosition;
        }

        public void HidePreview()
        {
            hologram.SetActive(false);
        }
    }
}