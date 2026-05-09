using UnityEngine;

namespace Assignment.Scripts.Gameplay.Bounds
{
    public class BoundaryRespawnTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player"))
                return;

            GlobalEventHandler.OnPlayerOutOfBounds?.Invoke();
            Debug.Log("PLAYER Died");
        }
    }
}