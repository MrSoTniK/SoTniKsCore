using Core.Infrastructure.Installers.Systems;
using Core.Tools;
using Leopotam.Ecs;
using System;
using UnityEngine;
using Zenject;

namespace Core.Infrastructure.Installers.Bootstrap 
{
    public abstract class BootstrapProjectInstaller<TSceneType, TSceneInfo> : MonoInstaller 
        where TSceneType : Enum
        where TSceneInfo : SceneInfoAbstract<TSceneType>
    {
        [SerializeField] protected SystemsInstaller SystemsInstaller;

        protected WorldsInfo WorldsInfo;
        protected TSceneInfo SceneInfo;

        public override void InstallBindings() 
        {
            BindEscGameStartup();
        }

        [Inject]
        public void Construct(WorldsInfo worldsInfo, TSceneInfo sceneInfo) 
        {
            WorldsInfo = worldsInfo;
            SceneInfo = sceneInfo;
        }

        protected void BindEscGameStartup()
        {
            Randomizer randomizer = new();

            Container.Bind<Randomizer>().FromInstance(randomizer).AsSingle();

            EcsWorld world = WorldGetter<TSceneType, TSceneInfo>.GetWorld(SceneInfo, WorldsInfo);        
            EcsGameStartup ecsGameStartup = new(SystemsInstaller.EcsPreInitSystems, SystemsInstaller.EcsInitSystems,
                SystemsInstaller.EcsRunSystems, SystemsInstaller.EcsFixedRunSystems, world);

            Container.BindInterfacesAndSelfTo<EcsGameStartup>().FromInstance(ecsGameStartup)
                .AsSingle()
                .NonLazy();
        }
    }
}