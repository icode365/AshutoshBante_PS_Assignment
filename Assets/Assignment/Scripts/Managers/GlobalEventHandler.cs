using System;
using Assignment.Scripts.Gameplay;
using Assignment.Scripts.Player;

public static class GlobalEventHandler
{
    public static Action OnCountdownStarted;
    public static Action<float> OnCountdownTick;
    public static Action OnCountdownEnded;

    public static Action<PlayerReferences> OnPlayerSpawned;

    public static Action OnPlayerOutOfBounds;

    public static Action<int, int> CollectionUIInitialized;
    public static Action<BoxCollectible> OnBoxCollected;
    public static Action OnAllBoxCollected;
    public static Action<BoxCollectible> OnTargetChanged;

    public static Action RestartGameClicked;
}