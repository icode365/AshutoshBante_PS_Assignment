using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assignment.Scripts.Gameplay.UI
{
    public class GameplayUIController : MonoBehaviour
    {
        [Header("Collection UI")] [SerializeField]
        private TMP_Text collectedText;

        [Header("Result UI")] [SerializeField] private GameObject resultPanel;
        [SerializeField] private TMP_Text resultTitleText;
        [SerializeField] private TMP_Text resultReasonText;

        [SerializeField] private Button resetButton;

        private int _currentCollected;
        private int _totalCollectibles;

        private void Start()
        {
            resultPanel.SetActive(false);

            AddEventListners();
            UpdateCollectionUI();
        }

        private void OnDestroy()
        {
            RemoveListeners();
        }

        private void AddEventListners()
        {
            GlobalEventHandler.OnAllBoxCollected += ShowWinState;
            GlobalEventHandler.OnBoxCollected += (_) => OnCollectibleCollected();

            GlobalEventHandler.CollectionUIInitialized += (collected, total) => InitializeCollectibles(total);
            GlobalEventHandler.OnPlayerOutOfBounds += () => ShowLoseState("You went out of Bounds!");
            GlobalEventHandler.OnCountdownEnded += () => ShowLoseState("Out of Time!");

            resetButton.onClick.AddListener(() => OnResetButtonClicked());
        }

        private void RemoveListeners()
        {
            GlobalEventHandler.OnAllBoxCollected -= ShowWinState;
            GlobalEventHandler.OnBoxCollected -= (_) => OnCollectibleCollected();

            GlobalEventHandler.CollectionUIInitialized -= (collected, total) => InitializeCollectibles(total);
            GlobalEventHandler.OnPlayerOutOfBounds -= () => ShowLoseState("You went out of Bounds!");
            GlobalEventHandler.OnCountdownEnded -= () => ShowLoseState("Out of Time!");
            
            resetButton.onClick.RemoveListener(() => OnResetButtonClicked());
        }

        public void InitializeCollectibles(int totalCollectibles)
        {
            _totalCollectibles = totalCollectibles;
            _currentCollected = 0;

            UpdateCollectionUI();
        }

        public void OnCollectibleCollected()
        {
            _currentCollected++;

            UpdateCollectionUI();

            if (_currentCollected >= _totalCollectibles)
            {
                ShowWinState();
            }
        }

        public void ShowWinState()
        {
            resultPanel.SetActive(true);

            resultTitleText.text = "YOU WIN!";
            resultReasonText.text =
                "All collectibles gathered. You Rock!";
        }

        public void ShowLoseState(string reason)
        {
            resultPanel.SetActive(true);

            resultTitleText.text = "YOU LOST";
            resultReasonText.text = reason;
        }

        private void UpdateCollectionUI()
        {
            collectedText.text =
                $"{_currentCollected} / {_totalCollectibles}";
        }

        // TODO :Maybe animate
        private void OnResetButtonClicked()
        {
            GlobalEventHandler.RestartGameClicked?.Invoke();
        }
    }
}