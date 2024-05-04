using System;
using UnityEngine;

public class Picture : MonsterSpot, IInteractable
{
    private TimeСounting _timeCounting = new();
    private Coroutine _spawnTime;
    protected override float TimeToActivate { get; set; } = 10f;
    public Action EnemyIsActivated;
    public bool _agressive;

    private void Start()
    {
        SubscribeToRespawn();
    }

    protected override void SpawnMonster()
    {
        Activate();
        _agressive = true;
        EnemyIsActivated?.Invoke();
    }

    public override void Activate()
    {
        UnsubscribeFromRespawn();
    }

    public override void Deactivate()
    {
        if (_agressive)
        {
            _agressive = false;
            SubscribeToRespawn();
            transform.Rotate(Vector3.up, 180f);
        }
    }
    private void CheckTimeIsUp(bool TimeIsUp)
    {
        if (TimeIsUp)
        {
            SpawnMonster();
        }
    }

    #region SubscribeAndUnsubscribeRespawn
    private void SubscribeToRespawn()
    {
        _timeCounting.TimeIsUp += CheckTimeIsUp;
        _spawnTime = StartCoroutine(_timeCounting.TimerCounting(TimeToActivate));
    }
    private void UnsubscribeFromRespawn()
    {
        StopCoroutine(_spawnTime);
        _timeCounting.TimeIsUp -= CheckTimeIsUp;
    }
    #endregion
}

