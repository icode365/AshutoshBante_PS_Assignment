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

    public static Action OnGravityApplied;

    public static Action<BoxCollectible> OnBoxCollected;
    public static Action<BoxCollectible> OnTargetChanged;
}