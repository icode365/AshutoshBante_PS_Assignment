using System;
using TMPro;
using UnityEngine;

namespace Assignment.Scripts.UI
{
    
}
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

    private void UpdateUI(float seconds)
    {
        TimeSpan time = TimeSpan.FromSeconds(seconds);

        // Format as HH:mm:ss
        string formattedTime = time.ToString(@"mm\:ss");
        timerText.text = formattedTime; //Mathf.CeilToInt(time).ToString();
    }
}