using UnityEngine;

public class LatchTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<MovableObject>(out MovableObject movable))
        {
            movable.IsOpen = false;
        }
    }
}
