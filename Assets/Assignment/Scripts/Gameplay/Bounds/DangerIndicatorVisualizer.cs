using UnityEngine;

namespace Assignment.Scripts.Gameplay.Bounds
{
    public class DangerIndicatorVisualizer : MonoBehaviour
    {
        [SerializeField] private Transform indicator;

        [SerializeField] private float maxDistance = 10f;
        [SerializeField] private float minScale = 0.2f;
        [SerializeField] private float maxScale = 2f;
        [SerializeField] private float surfaceOffset = 0.02f;

        [Header("Normal")] [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private float lineThreshold = 5f;
        [SerializeField] private float lineLength = 2f;

        public void UpdateIndicator(
            Vector3 position,
            Vector3 surfaceNormal,
            float distance)
        {
            indicator.position =
                position + surfaceNormal * surfaceOffset;

            indicator.rotation =
                Quaternion.LookRotation(surfaceNormal);

            float normalized =
                Mathf.Clamp01(distance / maxDistance);

            float scale =
                Mathf.Lerp(
                    maxScale,
                    minScale,
                    normalized);

            indicator.localScale =
                Vector3.one * scale;

            if (distance > lineThreshold)
            {
                lineRenderer.enabled = true;

                lineRenderer.SetPosition(
                    0,
                    position);
                
                float dynamicLength =
                    Mathf.Clamp(
                        distance * 0.5f,
                        0.5f,
                        3f);
                
                lineRenderer.SetPosition(
                    1,
                    position + surfaceNormal * dynamicLength);
            }
            else
            {
                lineRenderer.enabled = false;
            }
        }
    }
}