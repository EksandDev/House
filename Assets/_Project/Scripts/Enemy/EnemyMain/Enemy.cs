using System;
using UnityEngine;

public abstract class Enemy : MonsterSpot
{
    protected override float TimeToActivate { get; set; } = 10f;
    private Coroutine _spawnTime;
    private protected TimeСounting _timeCounting = new();
    public Action EnemyIsActivated;
    private protected void CheckTimeIsUp(bool TimeIsUp)
    {
        if (TimeIsUp)
        {
            SpawnMonster();
            UnsubscribeFromRespawn();
        }
    }

    protected override void SpawnMonster()
    {
        Activate();
    }

    #region SubscribeAndUnsubscribeRespawn
    private protected void SubscribeToRespawn()
    {
        _timeCounting.TimeIsUp += CheckTimeIsUp;
        _spawnTime = StartCoroutine(_timeCounting.TimerCounting(TimeToActivate));
    }
    private protected void UnsubscribeFromRespawn()
    {
        StopCoroutine(_spawnTime);
        _timeCounting.TimeIsUp -= CheckTimeIsUp;
    }

    #endregion
}
