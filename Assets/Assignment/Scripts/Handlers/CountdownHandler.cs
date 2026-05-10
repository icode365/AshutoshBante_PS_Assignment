using UnityEngine;

public class CountdownHandler : MonoBehaviour
{
    [SerializeField] private float countdownDuration = 60f;

    private float currentTime;
    private bool isRunning;

    private void Update()
    {
        if (!isRunning)
            return;

        currentTime -= Time.deltaTime;

        GlobalEventHandler.OnCountdownTick?.Invoke(currentTime);

        if (currentTime <= 0f)
        {
            currentTime = 0f;

            isRunning = false;

            GlobalEventHandler.OnCountdownEnded?.Invoke();
        }
    }

    public void StartCountdown()
    {
        currentTime = countdownDuration;

        isRunning = true;

        GlobalEventHandler.OnCountdownStarted?.Invoke();
    }

    public void StopCountdown()
    {
        isRunning = false;
    }
}