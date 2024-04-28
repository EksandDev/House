using UnityEngine;

public interface IPickUpable : IInteractable
{
    public void PickUp(ItemHoldPoint holdPoint);

    public void ReleaseHoldPoint();
}