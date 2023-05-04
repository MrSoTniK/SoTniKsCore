using Core.Infrastructure;
using Game.Enums.Scenes;
using Leopotam.Ecs;
using System.Collections.Generic;

namespace Game.Startups.MainMenu 
{ 
    public class MainMenuStartup : EcsSceneStartup<SceneType>
    {
       public MainMenuStartup(List<IEcsPreInitSystem> ecsPreInitSystems, List<IEcsInitSystem> ecsInitSystems,
                                  List<IEcsRunSystem> ecsRunSystems, List<IEcsRunSystem> ecsFixedRunSystems, EcsWorld world,
                                  WorldsInfo worldsInfo, SceneType sceneType) : 
            base(ecsPreInitSystems, ecsInitSystems, ecsRunSystems, ecsFixedRunSystems, world, worldsInfo, sceneType) { }
    }
}