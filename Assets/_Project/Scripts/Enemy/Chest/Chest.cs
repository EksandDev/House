using UnityEngine;

[RequireComponent(typeof(ChestTrigger))]
public class Chest : MonsterSpot
{
    private LidChest _lidChest;
    private Coroutine _spawnTime;
    private Coroutine _monsterTime;
    private TimeСounting _timeCounting = new();
    public bool OpenInChest { get; private set; }
    protected override float TimeToActivate { get; set; } = 7f;

    private void Start()
    {
        _lidChest = GetComponentInChildren<LidChest>();
        SubscribeToRespawn();
    }

    protected override void SpawnMonster()
    {
        Activate();
        UnsubscribeFromRespawn();
        SubscribeCheckChestLock();
    }

    public override void Activate()
    {
        _lidChest.EndPosition();
        OpenInChest = true;
    }

    public override void Deactivate()
    {
        UnscribeCheckChestLock();
        SubscribeToRespawn();
        StopCoroutine(_monsterTime);
        _lidChest.StartPosition();
        OpenInChest = false;
    }

    private void CheckTimeIsUp(bool TimeIsUp)
    {
        if (TimeIsUp)
        {
            SpawnMonster();
        }
    }

    private void CheckChestIsOpen(bool TimeIsUp)
    {
        if (OpenInChest && TimeIsUp)
        {
            Debug.Log("Умер от сундука!");
            return;
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

    #region SubscribeAndUnsubscribeCheckLockChest
    private void SubscribeCheckChestLock()
    {
        _timeCounting.TimeIsUp += CheckChestIsOpen;
        _monsterTime = StartCoroutine(_timeCounting.TimerCounting(TimeToActivate));
    }
    private void UnscribeCheckChestLock()
    {
        StopCoroutine(_monsterTime);
        _timeCounting.TimeIsUp -= CheckChestIsOpen;
    }
    #endregion
}
