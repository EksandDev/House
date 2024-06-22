using UnityEngine;

public class Log : InteractableObject
{
    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Collider = GetComponent<Collider>();
    }
}