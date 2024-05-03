
using System;
using UnityEngine;

public class Picture : MonsterSpot, IInteractable
{
    private TimeСounting _timeCounting = new();
    private Coroutine _spawnTime;
    public Action EnemyIsActivated;
    public bool _agressive;
    private void Start()
    {
        SubscribeToRespawn();
    }
    protected override float TimeToActivate { get; set; }
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
            Debug.Log("Deactivate");
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
        _spawnTime = StartCoroutine(_timeCounting.TimerCounting(15f));
    }
    private void UnsubscribeFromRespawn()
    {
        StopCoroutine(_spawnTime);
        _timeCounting.TimeIsUp -= CheckTimeIsUp;
    }
    #endregion
}

