
using System;
using UnityEngine;

public class Chest : MonsterSpot
{
    protected override float TimeToActivate { get; set; }
    [SerializeField] private GameObject _Pos;
    [SerializeField] private GameObject _boxLid;
    private Vector3 _startPos;
    private Vector3 _endPos;
    private TimeСounting _timeCounting = new();
    private Coroutine _spawnTime;
    private Coroutine _monsterTime;
    private bool _openInChest;

    private void Start()
    {
        SubscribeToRespawn();
        _startPos = _boxLid.transform.position;
        _endPos = _Pos.transform.position;
    }
    protected override void SpawnMonster()
    {
        Activate();
        SubscribeCheckChestLock();
    }
    public override void Activate()
    {
        ChangePosition(_endPos);
        UnsubscribeFromRespawn();
        _openInChest = true;
    }
    public override void Deactivate()
    {
        ChangePosition(_startPos);
        Debug.Log("enemy deactivated beach!!");
        SubscribeToRespawn();
    }
    private void CheckTimeIsUp(bool TimeIsUp)
    {
        if (TimeIsUp)
        {
            SpawnMonster();
        }
    }

    private void CheckCurtainOpen(bool TimeIsUp)
    {
        if (_openInChest && TimeIsUp)
        {
            Debug.Log("Проеб!");
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _boxLid && _openInChest)
        {
            _openInChest = false;
            ChangePosition(_startPos);
            Deactivate();
            StopCoroutine(_monsterTime);
            UnscribeCheckChestLock();
        }
    }
    #region SubscribeAndUnsubscribeRespawn
    private void SubscribeToRespawn()
    {
        _timeCounting.TimeIsUp += CheckTimeIsUp;
        _spawnTime = StartCoroutine(_timeCounting.TimerCounting(5f));
    }
    private void UnsubscribeFromRespawn()
    {
        StopCoroutine(_spawnTime);
        _timeCounting.TimeIsUp -= CheckTimeIsUp;
    }
    #endregion

    #region SubscribeAndUnsubscribeCheckLockChest
    private void SubscribeCheckChestLock()
    {
        _timeCounting.TimeIsUp += CheckCurtainOpen;
        _monsterTime = StartCoroutine(_timeCounting.TimerCounting(7f));
    }
    private void UnscribeCheckChestLock()
    {
        StopCoroutine(_monsterTime);
        _timeCounting.TimeIsUp -= CheckTimeIsUp;
    }
    #endregion
    private void ChangePosition(Vector3 targePos)
    {
        _boxLid.transform.position = targePos;
    }
}
