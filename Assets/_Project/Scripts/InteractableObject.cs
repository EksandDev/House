using DG.Tweening;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class InteractableObject : MonoBehaviour, IPickUpable
{
    protected Rigidbody Rigidbody;
    protected Collider Collider;
    protected ItemHoldPoint ItemHoldPoint;
    protected readonly int DefaultLayer = 0;
    protected readonly int PickedUpLayer = 6;

    private bool _canPickUp = true;
    private bool _canInteract = true;

    public bool CanPickUp { get => _canPickUp; set => _canPickUp = value; }
    public bool CanInteract { get => _canInteract; }
    public Rigidbody ObjectRigidbody { get => Rigidbody; }
    public ItemHoldPoint HoldPoint { get => ItemHoldPoint; }

    public virtual void PickUp(ItemHoldPoint holdPoint)
    {
        if (!CanPickUp || !CanInteract)
            return;

        Rigidbody.isKinematic = true;
        Collider.isTrigger = true;
        transform.parent = holdPoint.transform;
        ItemHoldPoint = holdPoint;
        gameObject.layer = PickedUpLayer;
        StartCoroutine(AnimatePickUp());
    }

    public virtual void ReleaseHoldPoint()
    {
        if (!CanInteract)
            return;

        Rigidbody.isKinematic = false;
        Collider.isTrigger = false;
        transform.parent = null;
        ItemHoldPoint.CurrentItem = null;
        ItemHoldPoint = null;
        gameObject.layer = DefaultLayer;
    }

    protected IEnumerator AnimatePickUp()
    {
        _canInteract = false;

        Tween tween = DOTween.Sequence().
            Append(transform.DOMove(transform.parent.position, 0.1f)).
            Append(transform.DORotate(transform.parent.position, 0.1f)).
            SetLink(gameObject);
        
        yield return tween.WaitForCompletion();

        _canInteract = true;
        transform.position = transform.parent.position;
        transform.rotation = transform.parent.rotation;
    }

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Collider = GetComponent<Collider>();
    }
}