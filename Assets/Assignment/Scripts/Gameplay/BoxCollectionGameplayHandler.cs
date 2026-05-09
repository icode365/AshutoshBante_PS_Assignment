using System.Collections.Generic;
using UnityEngine;


namespace Assignment.Scripts.Gameplay
{
    public class BoxCollectionGameplayHandler : MonoBehaviour
    {
        [SerializeField] private List<BoxCollectible> collectibles =
            new List<BoxCollectible>();

        private Queue<BoxCollectible> collectibleQueue =
            new Queue<BoxCollectible>();

        public BoxCollectible CurrentTarget { get; private set; }

        private void Awake()
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

                collectibleQueue.Enqueue(collectible);
            }

            ActivateNextCollectible();
        }

        private void HandleBoxCollected(BoxCollectible collectedBox)
        {
            ActivateNextCollectible();
        }

        private void ActivateNextCollectible()
        {
            if (collectibleQueue.Count <= 0)
            {
                Debug.Log("ALL COLLECTIBLES COMPLETED");
                CurrentTarget = null;

                return;
            }

            CurrentTarget = collectibleQueue.Dequeue();

            CurrentTarget.SetActiveState(true);
        }
    }
}