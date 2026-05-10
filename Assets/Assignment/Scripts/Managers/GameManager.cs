using Assignment.Scripts.Gameplay;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Assignment.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        [Header("References")] [SerializeField]
        private CountdownHandler countdownHandler;

        [SerializeField] private BoxCollectionGameplayHandler gameplayHandler;

        [Header("Player")] [SerializeField] private GameObject playerPrefab;
        [SerializeField] private Transform spawnPoint;

        private GameObject _currentPlayer;

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
            GlobalEventHandler.RestartGameClicked += Restart;
            GlobalEventHandler.OnPlayerOutOfBounds += OnPlayerOutOfBounds;
        }

        private void UnsubscribeEvents()
        {
            GlobalEventHandler.OnAllBoxCollected -= OnGameWon;
            GlobalEventHandler.RestartGameClicked -= Restart;
            GlobalEventHandler.OnPlayerOutOfBounds -= OnPlayerOutOfBounds;
        }

        private void SpawnPlayer()
        {
            _currentPlayer = Instantiate(
                playerPrefab,
                spawnPoint.position,
                spawnPoint.rotation);

            var playerRef = _currentPlayer.GetComponent<PlayerReferences>();
            GlobalEventHandler.OnPlayerSpawned?.Invoke(playerRef);
        }

        private void OnPlayerOutOfBounds()
        {
            Debug.Log("GAME OVER");

            DisablePlayerInput();
        }

        private void OnGameWon()
        {
            var collected = gameplayHandler.Collected;
            var total = gameplayHandler.TotalCollectibles;

            countdownHandler.StopCountdown();
            DisablePlayerInput();

            Debug.Log($"Collected {collected} out of {total} boxes");
            Debug.Log("GAME Won");
        }

        private void DisablePlayerInput()
        {
            var playerInput =
                _currentPlayer.GetComponent<PlayerInput>();

            if (playerInput != null)
                playerInput.enabled = false;
        }

        private void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}