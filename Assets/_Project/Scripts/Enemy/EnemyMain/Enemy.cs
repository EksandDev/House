using System;
using UnityEngine;

public abstract class Enemy : MonsterSpot
{
    [SerializeField] protected override float TimeToActivate { get; set; } = 10f;
    [SerializeField] protected float _timeParameter = 2f;
    private protected TimeСounting _timeCounting = new();
    private Coroutine _spawnTime;
    protected Coroutine _parametrTime;
    public Action EnemyIsActivated;

    private protected void CheckTimeIsUp(bool TimeIsUp)
    {
        if (TimeIsUp)
        {
            SpawnMonster();
            UnsubscribeFromRespawn();
        }
    }
    public virtual void CheckParameter(bool TimeIsUp)
    {
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

    #region SubscribeAndUnsubscribeCheckParametr
    public void SubscribeCheckParametr()
    {
        _timeCounting.TimeIsUp += CheckParameter;
        _parametrTime = StartCoroutine(_timeCounting.TimerCounting(_timeParameter));
    }
    protected void UnscribeCheckParametr()
    {
        StopCoroutine(_parametrTime);
        _timeCounting.TimeIsUp -= CheckParameter;
    }
    #endregion
    protected void ChangeEnemyVisibility(GameObject enemy, bool state)
    {
        enemy.gameObject.SetActive(state);
    }
}
