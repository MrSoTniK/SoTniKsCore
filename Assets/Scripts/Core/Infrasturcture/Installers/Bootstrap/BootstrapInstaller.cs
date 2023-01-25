using Core.Infrastructure.Installers.Systems;
using Core.Tools;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;

namespace Core.Infrastructure.Installers.Bootstrap 
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private SystemsInstaller _systemsInstaller;

        public override void InstallBindings() 
        {
            BindEscGameStartup();
        }

        private void BindEscGameStartup()
        {
            EcsWorld world = new();
            Randomizer randomizer = new();
            WorldContainer.Init(world);

            Container.Bind<EcsWorld>().FromInstance(world).AsSingle().NonLazy();
            Container.Bind<Randomizer>().FromInstance(randomizer).AsSingle();

            EcsGameStartup ecsGameStartup = new(_systemsInstaller.EcsPreInitSystems, _systemsInstaller.EcsInitSystems,
                _systemsInstaller.EcsRunSystems, _systemsInstaller.EcsFixedRunSystems, world);

            Container.BindInterfacesAndSelfTo<EcsGameStartup>().FromInstance(ecsGameStartup)
                .AsSingle()
                .NonLazy();
        }
    }
}