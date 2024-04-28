using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    protected float TimeToRespawn;

    public abstract void Activate();

    public abstract void Deactivate();

    protected abstract void Act();
}
