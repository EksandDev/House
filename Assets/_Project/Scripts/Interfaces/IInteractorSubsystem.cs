using UnityEngine;

public interface IInteractorSubsystem
{
    public void TryInteract(Collider hitCollider);
}