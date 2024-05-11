using UnityEngine;

[RequireComponent(typeof(ChestTrigger))]
public class Chest : Enemy
{
    private LidChest _lidChest;
    public bool OpenInChest { get; private set; }

    private void Start()
    {
        SubscribeToRespawn();
        _lidChest = GetComponentInChildren<LidChest>();
    }

    public override void Activate()
    {
        SubscribeCheckParametr();
        _lidChest.EndPosition();
        OpenInChest = true;
    }

    public override void Deactivate()
    {
        UnscribeCheckParametr();
        SubscribeToRespawn();
        StopCoroutine(_parametrTime);
        _lidChest.StartPosition();
        OpenInChest = false;
    }

    public override void CheckParameter(bool TimeIsUp)
    {
        if (OpenInChest && TimeIsUp)
        {
            Debug.Log("Умер от сундука!");
            return;
        }
    }
}
