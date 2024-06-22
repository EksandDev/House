using UnityEngine;

public class StumpTrigger : MonoBehaviour
{
    [SerializeField] private Stump _stump;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Log>(out Log log) && log.HoldPoint != null)
            StartCoroutine(_stump.ChopWood(log));
    }
}