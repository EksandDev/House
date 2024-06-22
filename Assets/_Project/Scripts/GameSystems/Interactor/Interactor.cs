using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Interactor : MonoBehaviour
{
    private PickUpInteractor _pickUpInteractor;
    private PhysicalMoveInteractor _holdMouseInteractor;
    private ClickInteractor _clickInteractor;
    private List<IInteractorSubsystem> _interactorSubsystems = new(5);
    private float _rayDistance = 3;

    #region Zenject init
    [Inject]
    private void Initialize(PickUpInteractor pickUpInteractor, PhysicalMoveInteractor holdMouseInteractor,
        ClickInteractor clickInteractor)
    {
        _pickUpInteractor = pickUpInteractor;
        _holdMouseInteractor = holdMouseInteractor;
        _clickInteractor = clickInteractor;
    }
    #endregion

    private void Awake()
    {
        _interactorSubsystems.Add(_pickUpInteractor);
        _interactorSubsystems.Add(_holdMouseInteractor);
        _interactorSubsystems.Add(_clickInteractor);
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