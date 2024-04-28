using UnityEngine;

public class PhysicalMoveInteractor : IInteractorSubsystem
{
    public void TryInteract(Collider hitCollider)
    {
        if (hitCollider.TryGetComponent<IPhysicMovable>(out IPhysicMovable movable))
        {
            movable.PhysicalMove();
        }
    }
}