using System;
using UnityEngine;

public class EnemyMain : MonsterSpot
{
    protected override float TimeToActivate { get; set; } = 10f;
    private Coroutine _spawnTime;
    private protected TimeСounting _timeCounting = new();
    public Action EnemyIsActivated;
    public override void Activate()
    {
    }

    public override void Deactivate()
    {
    }

    protected override void SpawnMonster()
    {
    }

    private protected void CheckTimeIsUp(bool TimeIsUp)
    {
        if (TimeIsUp)
        {
            SpawnMonster();
        }
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
