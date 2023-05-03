using Core.Infrastructure.Installers.Bootstrap;
using Core.Tools;
using Game.Enums.Scenes;
using Game.Info.Scenes;
using Game.Installers.Systems;
using Game.Scenes.Game;
using Leopotam.Ecs;

namespace Game.Installers.Bootstrap 
{
    public class GameBootstrapInstaller : BootstrapSceneInstaller<GameSystemsInstaller, GameStartup, SceneType, GameSceneInfo>
    {
        protected override void BindEscSceneStartup()
        {
            EcsWorld world = WorldGetter<SceneType, GameSceneInfo>.GetWorld(SceneInfo, WorldsInfo);
            EcsSceneStratup = new(SceneSystemsInstaller.EcsPreInitSystems, SceneSystemsInstaller.EcsInitSystems,
                SceneSystemsInstaller.EcsRunSystems, SceneSystemsInstaller.EcsFixedRunSystems, world, WorldsInfo, SceneInfo.SceneType);

            Container.BindInterfacesAndSelfTo<GameStartup>().FromInstance(EcsSceneStratup)
                .AsSingle()
                .NonLazy();
        }
    }
}