using TMPro;
using UnityEngine;

namespace Assignment.Scripts.Gameplay.UI
{
    public class GameplayUIController : MonoBehaviour
    {
        [Header("Collection UI")]
        [SerializeField] private TMP_Text collectedText;

        [Header("Result UI")]
        [SerializeField] private GameObject resultPanel;

        [SerializeField] private TMP_Text resultTitleText;

        [SerializeField] private TMP_Text resultReasonText;

        private int _currentCollected;
        private int _totalCollectibles;

        private void Start()
        {
            resultPanel.SetActive(false);

            
            GlobalEventHandler.OnAllBoxCollected += ShowWinState;
            GlobalEventHandler.OnBoxCollected += (_) => OnCollectibleCollected();

            GlobalEventHandler.CollectionUIInitialized += (collected, total) => InitializeCollectibles(total);
            GlobalEventHandler.OnPlayerOutOfBounds += () => ShowLoseState("You went out of Bounds!");
            
            UpdateCollectionUI();
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
    }
}