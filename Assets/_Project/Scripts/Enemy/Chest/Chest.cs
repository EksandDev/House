using UnityEngine;

[RequireComponent(typeof(ChestTrigger))]
public class Chest : Enemy
{
    private LidChest _lidChest;
    private Coroutine _monsterTime;
    public bool OpenInChest { get; private set; }

    private void Start()
    {
        SubscribeToRespawn();
        _lidChest = GetComponentInChildren<LidChest>();
    }

    public override void Activate()
    {
        SubscribeCheckChestLock();
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

    private void CheckChestIsOpen(bool TimeIsUp)
    {
        if (OpenInChest && TimeIsUp)
        {
            Debug.Log("Умер от сундука!");
            return;
        }
    }

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
