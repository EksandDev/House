using System.Collections;
using System.Linq;
using UnityEngine;

public class Stump : MonoBehaviour
{
    [SerializeField] private PlaceForWood[] _placesForWood;
    [SerializeField] private Log _logPrefab;
    [SerializeField] private Wood _woodPrefab;
    [SerializeField] private Transform _chopPoint;

    public IEnumerator ChopWood(Log log)
    {
        if (_placesForWood.All(place => place.IsFull))
            yield break;

        log.ReleaseHoldPoint();
        log.ObjectRigidbody.isKinematic = true;
        log.CanPickUp = false;
        log.transform.position = _chopPoint.position;
        log.transform.rotation = _chopPoint.rotation;

        yield return new WaitForSeconds(2);

        if (log != null)
            Destroy(log.gameObject);


        foreach(var place in _placesForWood)
        {
            if (place.IsFull)
                continue;

            Wood wood = Instantiate(_woodPrefab, place.transform.position, Quaternion.identity);
            wood.CurrentPlace = place;
            place.IsFull = true;
        }
    }
}