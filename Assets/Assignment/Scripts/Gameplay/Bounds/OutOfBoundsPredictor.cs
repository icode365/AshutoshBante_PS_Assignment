using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assignment.Scripts.Gameplay.Bounds
{
    public class OutOfBoundsPredictor : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private List<Collider> boundaryPlanes = new();
        [SerializeField] private DangerIndicatorVisualizer visualizer;

        private void Awake()
        {
            GlobalEventHandler.OnPlayerSpawned += SetPlayerTransformOnSpawned;
        }

        private void OnDestroy()
        {
            GlobalEventHandler.OnPlayerSpawned -= SetPlayerTransformOnSpawned;
        }

        private void Update()
        {
            SolveClosestBoundary();
        }

        private void SetPlayerTransformOnSpawned(PlayerReferences reference)
        {
            player = reference.transform;
        }

        private void SolveClosestBoundary()
        {
            Collider closestPlane = null;
            float closestDistance = Mathf.Infinity;
            Vector3 closestPoint = Vector3.zero;
            Vector3 closestNormal = Vector3.up;

            foreach (var plane in boundaryPlanes)
            {
                Vector3 point =
                    plane.ClosestPoint(player.position);

                float distance =
                    Vector3.Distance(
                        player.position,
                        point);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPlane = plane;
                    closestPoint = point;
                    closestNormal = (player.position - point).normalized;
                }
            }

            if (closestPlane != null)
            {
                visualizer.UpdateIndicator(
                    closestPoint,
                    closestNormal,
                    closestDistance);
            }
        }
    }
}