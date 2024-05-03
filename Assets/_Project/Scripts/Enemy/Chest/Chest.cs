
using System;
using UnityEngine;

public class Chest : MonsterSpot
{
    protected override float TimeToActivate { get; set; }
    [SerializeField] private Transform _Pos;
    [SerializeField] private GameObject _boxLid;
    private Vector3 _startPos;
    private Transform _endPos;
    private TimeСounting _timeCounting = new();
    private Coroutine _spawnTime;
    private Coroutine _monsterTime;
    private bool _openInChest;

    private void Start()
    {
        SubscribeToRespawn();
        _startPos = _boxLid.transform.position;
        _endPos = _Pos;
    }
    protected override void SpawnMonster()
    {
        Activate();
        SubscribeCheckChestLock();
    }
    public override void Activate()
    {
        ChangePosition(_endPos.transform.position);
        UnsubscribeFromRespawn();
        _openInChest = true;
    }
    public override void Deactivate()
    {
        ChangePosition(_startPos);
        SubscribeToRespawn();
    }
    private void CheckTimeIsUp(bool TimeIsUp)
    {
        if (TimeIsUp)
        {
            SpawnMonster();
        }
    }

    private void CheckChestOpen(bool TimeIsUp)
    {
        if (_openInChest && TimeIsUp)
        {
            Debug.Log("Умер от сундука!");
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _boxLid && _openInChest)
        {
            other.transform.parent = transform;
            _openInChest = false;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
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
        _timeCounting.TimeIsUp += CheckChestOpen;
        _monsterTime = StartCoroutine(_timeCounting.TimerCounting(7f));
    }
    private void UnscribeCheckChestLock()
    {
        StopCoroutine(_monsterTime);
        _timeCounting.TimeIsUp -= CheckChestOpen;
    }
    #endregion
    private void ChangePosition(Vector3 targePos)
    {
        _boxLid.transform.position = targePos;
        _boxLid.transform.rotation = Quaternion.identity;
    }
}
