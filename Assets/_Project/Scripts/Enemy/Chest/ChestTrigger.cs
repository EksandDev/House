using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChestTrigger : MonoBehaviour
{
    private Chest _chest;

    private void Start()
    {
        _chest = GetComponent<Chest>();
    }

    private void OnTriggerEnter(Collider lidChest)
    {
        if (lidChest.gameObject.GetComponent<LidChest>() && _chest.OpenInChest)
        {
            lidChest.transform.parent = transform;
            lidChest.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            _chest.Deactivate();
        }
    }
}
