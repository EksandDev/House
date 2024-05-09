using System;
using UnityEngine;

public class Picture : EnemyMain, IInteractable
{
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
}

