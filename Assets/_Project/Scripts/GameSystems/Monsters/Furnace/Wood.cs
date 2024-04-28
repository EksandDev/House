using UnityEngine;

public class Wood : InteractableObject
{
    public PlaceForWood CurrentPlace { get; set; }

    public override void ReleaseHoldPoint()
    {
        if (CurrentPlace != null)
        {
            transform.position = CurrentPlace.transform.position;
            Rigidbody.isKinematic = false;
            Collider.isTrigger = false;
            transform.parent = null;
            ItemHoldPoint.CurrentItem = null;
            ItemHoldPoint = null;
            gameObject.layer = DefaultLayer;
        }
    }

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Collider = GetComponent<Collider>();
    }
}