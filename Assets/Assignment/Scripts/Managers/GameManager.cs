using Assignment.Scripts.Gameplay;
using UnityEngine;

namespace Assignment.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private CountdownHandler countdownHandler;
        [SerializeField] private BoxCollectionGameplayHandler gameplayHandler;
        
        [Header("Player")] [SerializeField] private GameObject playerPrefab;
        [SerializeField] private Transform spawnPoint;
        
        private GameObject currentPlayer;

        private void Start()
        {
            SpawnPlayer();

            gameplayHandler.enabled = true;
            countdownHandler.StartCountdown();

            SubscribeEvents();
        }

        private void OnDestroy()
        {
            UnsubscribeEvents();
        }

        private void SubscribeEvents()
        {
            GlobalEventHandler.OnAllBoxCollected += OnGameWon;
            
            GlobalEventHandler.OnCountdownEnded += HandleCountdownEnded;
            GlobalEventHandler.OnPlayerOutOfBounds += OnPlayerOutOfBounds;
        }

        private void UnsubscribeEvents()
        {
            GlobalEventHandler.OnAllBoxCollected -= OnGameWon;
            
            GlobalEventHandler.OnCountdownEnded -= HandleCountdownEnded;
            GlobalEventHandler.OnPlayerOutOfBounds -= OnPlayerOutOfBounds;
        }

        private void SpawnPlayer()
        {
            currentPlayer = Instantiate(
                playerPrefab,
                spawnPoint.position,
                spawnPoint.rotation);

            var playerRef = currentPlayer.GetComponent<PlayerReferences>();
            GlobalEventHandler.OnPlayerSpawned?.Invoke(playerRef);
        }

        private void HandleCountdownEnded()
        {
            Debug.Log("GAME OVER");

            // Future:
            // Disable input
            // Show UI
            // Stop gameplay
        }

        private void OnPlayerOutOfBounds()
        {
            Debug.Log("GAME OVER");

            CharacterController controller =
                currentPlayer.GetComponent<CharacterController>();

            if (controller != null)
                controller.enabled = false;
            
            // Future:
            // Disable input
            // Show UI
            // Stop gameplay
        }

        private void OnGameWon()
        {
            var collected = gameplayHandler.Collected;
            var total = gameplayHandler.TotalCollectibles;
            
            Debug.Log($"Collected {collected} out of {total} boxes");
            Debug.Log("GAME Won");
        }
    }
}