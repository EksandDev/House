using UnityEngine;

public class Picture : Enemy, IClickable
{
    public bool Agressive { get; private set; }
    private void Start()
    {
        SubscribeToRespawn();
    }

    public override void Activate()
    {
        Agressive = true;
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
        if (Agressive)
        {
            Agressive = false;
            SubscribeToRespawn();
            transform.Rotate(Vector3.up, 180f);
        }
    }
}

