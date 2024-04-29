using System.Collections;
using UnityEngine;

public class Furnace : MonoBehaviour
{
    private int _fuelCount = 6;
    private bool _fuelReadyToDecrease = true;

    public int FuelCount 
    { 
        get => _fuelCount;
        set
        {
            if (value <= 0)
                return;

            _fuelCount = value;

            if (_fuelCount < 0)
                _fuelCount = 0;

            OnFuelChange();
        }
    }

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
            FuelCount++;
        }
    }

    private void OnFuelChange()
    {
        if (_fuelCount == 0)
            _fuelReadyToDecrease = false;

        Debug.Log($"Fuel remain: {_fuelCount}");
    }

    private IEnumerator FuelDecreasing()
    {
        while(_fuelReadyToDecrease)
        {
            yield return new WaitForSeconds(5);

            FuelCount--;
        }
    }


    private void Start() => StartCoroutine(FuelDecreasing());
}
