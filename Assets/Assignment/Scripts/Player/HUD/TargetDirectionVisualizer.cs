using System;
using UnityEngine;

namespace Assignment.Scripts.Player.HUD
{
    public class TargetDirectionVisualizer : MonoBehaviour
    {
        [Header("References")] [SerializeField]
        private TargetDirectionSolver solver;

        [SerializeField] private UnityEngine.Camera mainCamera;

        [Header("UI")] [SerializeField] private RectTransform arrowUI;
        [SerializeField] private RectTransform targetMarkerUI;

        [SerializeField] private Canvas canvas;
        [SerializeField] private RectTransform canvasRect;

        private void Awake()
        {
            GlobalEventHandler.OnPlayerSpawned += SetPlayerReferences;
        }

        private void Update()
        {
            if (!solver.HasTarget)
            {
                HideAll();
                return;
            }

            if (solver.IsTargetVisible)
            {
                ShowTargetMarker();
            }
            else
            {
                ShowDirectionArrow();
            }
        }

        private void SetPlayerReferences(PlayerReferences playerRef)
        {
            mainCamera = playerRef.mainCamera;
            solver = playerRef.targetDirectionSolver;
        }

        private void ShowTargetMarker()
        {
            arrowUI.gameObject.SetActive(false);
            targetMarkerUI.gameObject.SetActive(true);

            Vector2 localPoint;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRect,
                solver.ScreenPosition,
                null,
                out localPoint);

            targetMarkerUI.localPosition = localPoint;
        }

        private void ShowDirectionArrow()
        {
            targetMarkerUI.gameObject.SetActive(false);

            arrowUI.gameObject.SetActive(true);

            Vector3 viewportPos =
                solver.GetViewportPosition();

            Vector2 direction =
                new Vector2(
                    viewportPos.x - 0.5f,
                    viewportPos.y - 0.5f);

            float angle =
                Mathf.Atan2(direction.y, direction.x)
                * Mathf.Rad2Deg;

            arrowUI.localRotation =
                Quaternion.Euler(0, 0, angle);
        }

        private void HideAll()
        {
            arrowUI.gameObject.SetActive(false);
            targetMarkerUI.gameObject.SetActive(false);
        }
    }
}