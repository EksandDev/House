using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    protected abstract float TimeToRespawn { get; set; }

    public abstract void Activate();

    public abstract void Deactivate();

    protected abstract void Act();
}
