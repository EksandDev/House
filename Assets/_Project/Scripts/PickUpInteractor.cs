using UnityEngine;
using Zenject;

public class PickUpInteractor : IInteractorSubsystem
{
    public ItemHoldPoint ItemHoldPoint => _itemHoldPoint;

    private ItemHoldPoint _itemHoldPoint;

    #region Zenject init
    [Inject]
    private void Initialize(ItemHoldPoint itemHoldPoint)
    {
        _itemHoldPoint = itemHoldPoint;
    }
    #endregion

    public void TryInteract(Collider hitCollider)
    {
        if (hitCollider.TryGetComponent<IPickUpable>(out IPickUpable pickUpable) &&
            Input.GetKeyDown(KeyCode.E) && ItemHoldPoint.CurrentItem == null)
        {
            pickUpable.PickUp(ItemHoldPoint.transform);
            ItemHoldPoint.CurrentItem = pickUpable;
        }
    }

    public void TryDropItem()
    {
        if (ItemHoldPoint.CurrentItem != null && Input.GetKeyDown(KeyCode.G))
        {
            ItemHoldPoint.CurrentItem.ReleaseHoldPoint();
            ItemHoldPoint.CurrentItem = null;
        }
    }
}