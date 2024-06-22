using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private PlayerMover _playerMover;

    public override void InstallBindings()
    {
        Container.Bind<PlayerInput>().AsSingle();
        Container.Bind<PlayerMover>().FromInstance(_playerMover).AsSingle();

    }
}
