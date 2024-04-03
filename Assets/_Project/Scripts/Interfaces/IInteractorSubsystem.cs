using UnityEngine;

public interface IInteractorSubsystem
{
    public void TryInteract(RaycastHit hit);
}