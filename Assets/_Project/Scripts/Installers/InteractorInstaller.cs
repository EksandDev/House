using UnityEngine;
using Zenject;

public class InteractorInstaller : MonoInstaller
{
    [SerializeField] private ItemHoldPoint _itemHoldPoint;

    public override void InstallBindings()
    {
        Container.Bind<PickUpInteractor>().AsSingle();
        Container.Bind<ItemHoldPoint>().FromInstance(_itemHoldPoint).AsSingle();
    }
}
