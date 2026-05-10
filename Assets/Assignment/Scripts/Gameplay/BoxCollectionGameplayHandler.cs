using System.Collections.Generic;
using UnityEngine;


namespace Assignment.Scripts.Gameplay
{
    public class BoxCollectionGameplayHandler : MonoBehaviour
    {
        public int Collected { get; private set; } = 0;
        public int TotalCollectibles { get; private set; } = 0;
        
        [SerializeField] private List<BoxCollectible> collectibles = new();

        private Queue<BoxCollectible> _collectibleQueue = new();
        public BoxCollectible CurrentTarget { get; private set; }

        private void Start()
        {
            InitializeQueue();
        }

        private void OnEnable()
        {
            GlobalEventHandler.OnBoxCollected += HandleBoxCollected;
        }

        private void OnDisable()
        {
            GlobalEventHandler.OnBoxCollected -= HandleBoxCollected;
        }

        private void InitializeQueue()
        {
            foreach (var collectible in collectibles)
            {
                collectible.SetActiveState(false);

                _collectibleQueue.Enqueue(collectible);
            }
            
            TotalCollectibles = _collectibleQueue.Count;
            GlobalEventHandler.CollectionUIInitialized?.Invoke(Collected, TotalCollectibles);
            // UpdateUI(); for future update
            
            ActivateNextCollectible();
        }

        private void HandleBoxCollected(BoxCollectible collectedBox)
        {
            Collected++;
            ActivateNextCollectible();
        }

        private void ActivateNextCollectible()
        {
            if (_collectibleQueue.Count <= 0)
            {
                Debug.Log("ALL COLLECTIBLES COMPLETED");
                CurrentTarget = null;
                
                GlobalEventHandler.OnAllBoxCollected?.Invoke();

                return;
            }

            CurrentTarget = _collectibleQueue.Dequeue();

            CurrentTarget.SetActiveState(true);
            GlobalEventHandler.OnTargetChanged?.Invoke(CurrentTarget);
        }
    }
}