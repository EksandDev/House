using System;
using UnityEngine;

public class Picture : Enemy, IInteractable
{
    public bool _agressive;

    private void Start()
    {
        SubscribeToRespawn();
    }

    public override void Activate()
    {
        _agressive = true;
        EnemyIsActivated?.Invoke();
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

