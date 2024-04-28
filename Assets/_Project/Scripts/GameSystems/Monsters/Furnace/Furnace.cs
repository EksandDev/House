using System.Collections;
using UnityEngine;

public class Furnace : MonoBehaviour
{
    private int _fuelCount = 6;
    private bool _fuelReadyToDecrease = true;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Wood>(out Wood wood) && _fuelReadyToDecrease)
        {
            if (wood.CurrentPlace == null)
                return;

            wood.ReleaseHoldPoint();
            wood.CurrentPlace.IsFull = false;
            wood.CurrentPlace = null;
            Destroy(wood.gameObject);
            _fuelCount++;
        }
    }

    private IEnumerator FuelDecreasing()
    {
        while(_fuelReadyToDecrease)
        {
            yield return new WaitForSeconds(5);

            _fuelCount--;
            Debug.Log($"Fuel remain: {_fuelCount}");

            if (_fuelCount == 0)
                _fuelReadyToDecrease = false;
        }
    }

    private void Start() => StartCoroutine(FuelDecreasing());
}
