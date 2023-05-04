using Core.Infrastructure.Installers.Bootstrap;
using Core.Tools;
using Game.Enums.Scenes;
using Game.Info.Scenes;
using Game.Installers.Systems;
using Game.Startups.Project;
using Leopotam.Ecs;

namespace Game.Installers.Bootstrap 
{
    public class BootstrapInstaller : BootstrapSceneInstaller<SystemsProjectInstaller, ProjectStartup, SceneType, ProjectInfo>
    {
        protected override void BindEscSceneStartup()
        {
            EcsWorld world = WorldGetter<SceneType, ProjectInfo>.GetWorld(SceneInfo, WorldsInfo);
            EcsSceneStratup = new(SceneSystemsInstaller.EcsPreInitSystems, SceneSystemsInstaller.EcsInitSystems,
                SceneSystemsInstaller.EcsRunSystems, SceneSystemsInstaller.EcsFixedRunSystems, world, WorldsInfo, SceneInfo.SceneType);

            Container.BindInterfacesAndSelfTo<ProjectStartup>().FromInstance(EcsSceneStratup)
                .AsSingle()
                .NonLazy();
        }
    }
}