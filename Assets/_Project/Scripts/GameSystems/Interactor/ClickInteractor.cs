using UnityEngine;

public class ClickInteractor : IInteractorSubsystem
{
    public void TryInteract(Collider hitCollider)
    {
        if (hitCollider.TryGetComponent<IClickable>(out IClickable clickable))
            clickable.OnClick();
    }
}
