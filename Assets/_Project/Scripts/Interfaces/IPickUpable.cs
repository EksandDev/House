using UnityEngine;

public interface IPickUpable : IInteractable
{
    public void PickUp(Transform holdPoint);

    public void ReleaseHoldPoint();
}