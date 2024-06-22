using UnityEngine;

public class Wood : InteractableObject
{
    public PlaceForWood CurrentPlace { get; set; }

    public override void ReleaseHoldPoint()
    {
        if (CurrentPlace != null)
        {
            transform.position = CurrentPlace.transform.position;
            base.ReleaseHoldPoint();
        }
    }

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Collider = GetComponent<Collider>();
    }
}