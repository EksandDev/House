using UnityEngine;

public abstract class MonsterSpot : MonoBehaviour
{
    protected abstract float TimeToActivate { get; set; }

    protected abstract void SpawnMonster();
    public abstract void Activate();
    public abstract void Deactivate();
}