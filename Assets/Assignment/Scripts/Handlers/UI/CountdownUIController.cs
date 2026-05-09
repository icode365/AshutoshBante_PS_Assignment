using TMPro;
using UnityEngine;

public class CountdownUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    private void OnEnable()
    {
        GlobalEventHandler.OnCountdownTick += UpdateUI;
    }

    private void OnDisable()
    {
        GlobalEventHandler.OnCountdownTick -= UpdateUI;
    }

    private void UpdateUI(float time)
    {
        timerText.text = Mathf.CeilToInt(time).ToString();
    }
}