using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Interactor : MonoBehaviour
{
    private PickUpInteractor _pickUpInteractor;
    private List<IInteractorSubsystem> _interactorSubsystems = new(5);
    private float _rayDistance = 3;

    #region Zenject init
    [Inject]
    private void Initialize(PickUpInteractor pickUpInteractor)
    {
        _pickUpInteractor = pickUpInteractor;
    }
    #endregion

    private void Awake()
    {
        _interactorSubsystems.Add(_pickUpInteractor);
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _rayDistance) &&
            hit.collider.TryGetComponent<IInteractable>(out IInteractable interactable))
        {
            foreach (var subsystem in _interactorSubsystems)
                subsystem.TryInteract(hit.collider);
        }

        AdditionalChecks();
    }

    private void AdditionalChecks()
    {
        _pickUpInteractor.TryDropItem();
    }
}