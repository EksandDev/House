using UnityEngine;

public class WaterMonsterTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Furnace>(out Furnace furnace))
        {
            furnace.FuelCount -= 3;
            Destroy(transform.parent.gameObject);
        }
    }
}