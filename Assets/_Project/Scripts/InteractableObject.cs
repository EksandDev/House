using DG.Tweening;
using System.Collections;
using UnityEngine;

public class InteractableObject : MonoBehaviour, IPickUpable
{
    private Rigidbody _rigidbody;
    private Collider _collider;
    private readonly int _defaultLayer = 0;
    private readonly int _pickedUpLayer = 6;

    public void PickUp(Transform holdPoint)
    {
        _rigidbody.isKinematic = true;
        _collider.isTrigger = true;
        transform.parent = holdPoint;
        gameObject.layer = _pickedUpLayer;
        StartCoroutine(AnimatePickUp());
    }

    public void ReleaseHoldPoint()
    {
        _rigidbody.isKinematic = false;
        _collider.isTrigger = false;
        transform.parent = null;
        gameObject.layer = _defaultLayer;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    private IEnumerator AnimatePickUp()
    {
        DOTween.Sequence().
            Append(transform.DOMove(transform.parent.position, 0.1f)).
            Append(transform.DORotate(transform.parent.position, 0.1f)).
            SetLink(gameObject);

        yield return new WaitForSeconds(0.2f);

        transform.position = transform.parent.position;
        transform.rotation = transform.parent.rotation;
    }
}