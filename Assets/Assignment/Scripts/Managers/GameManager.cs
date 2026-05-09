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
            GlobalEventHandler.OnCountdownEnded += HandleCountdownEnded;
        }

        private void UnsubscribeEvents()
        {
            GlobalEventHandler.OnCountdownEnded -= HandleCountdownEnded;
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
    }
}