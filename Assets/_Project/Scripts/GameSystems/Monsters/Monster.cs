using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    protected Transform Target;

    protected abstract void Act();
}
