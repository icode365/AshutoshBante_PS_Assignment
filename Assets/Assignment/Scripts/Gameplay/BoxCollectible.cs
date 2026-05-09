using UnityEngine;

namespace Assignment.Scripts.Gameplay
{
    [RequireComponent(typeof(Collider))]
    public class BoxCollectible : MonoBehaviour
    {
        [SerializeField] private GameObject visualRoot;

        private Collider collectibleCollider;

        public bool IsCollected { get; private set; }
        public bool IsActiveTarget { get; private set; }

        private void Awake()
        {
            collectibleCollider = GetComponent<Collider>();

            SetActiveState(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!IsActiveTarget)
                return;

            if (!other.CompareTag("Player"))
                return;

            Collect();
        }

        public void SetActiveState(bool state)
        {
            IsActiveTarget = state;

            collectibleCollider.enabled = state;

            if (visualRoot != null)
                visualRoot.SetActive(state);
        }

        private void Collect()
        {
            if (IsCollected)
                return;

            IsCollected = true;

            SetActiveState(false);

            GlobalEventHandler.OnBoxCollected?.Invoke(this);
        }
    }
}