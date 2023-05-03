using Core.Infrastructure.Installers.Bootstrap;
using Core.Tools;
using Game.Enums.Scenes;
using Game.Info.Scenes;
using Game.Installers.Systems;
using Game.Scenes.MainMenu;
using Leopotam.Ecs;

namespace Game.Installers.Bootstrap 
{
    public class MainMenuBootstrapInstaller : BootstrapSceneInstaller<MainMenuSystemsInstaller, MainMenuStartup, SceneType, MainMenuSceneInfo>
    {
        protected override void BindEscSceneStartup()
        {
            EcsWorld world = WorldGetter<SceneType, MainMenuSceneInfo>.GetWorld(SceneInfo, WorldsInfo);
            EcsSceneStratup = new(SceneSystemsInstaller.EcsPreInitSystems, SceneSystemsInstaller.EcsInitSystems,
                SceneSystemsInstaller.EcsRunSystems, SceneSystemsInstaller.EcsFixedRunSystems, world, WorldsInfo, SceneInfo.SceneType);

            Container.BindInterfacesAndSelfTo<MainMenuStartup>().FromInstance(EcsSceneStratup)
                .AsSingle()
                .NonLazy();
        }
    }
}