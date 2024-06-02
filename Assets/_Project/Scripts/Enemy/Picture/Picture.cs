using System;
using UnityEngine;

public class Picture : Enemy, IClickable
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

    public void OnClick()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Deactivate();
        }
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

