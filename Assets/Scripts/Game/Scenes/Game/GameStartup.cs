using Core.Infrastructure;
using Game.Enums.Scenes;
using Leopotam.Ecs;
using System.Collections.Generic;

namespace Game.Scenes.Game 
{
    public class GameStartup : EcsSceneStartup<SceneType>
    {
        public GameStartup(List<IEcsPreInitSystem> ecsPreInitSystems, List<IEcsInitSystem> ecsInitSystems,
                                     List<IEcsRunSystem> ecsRunSystems, List<IEcsRunSystem> ecsFixedRunSystems, 
                                     EcsWorld world, WorldsInfo worldsInfo, SceneType sceneType) :
               base(ecsPreInitSystems, ecsInitSystems, ecsRunSystems, ecsFixedRunSystems, world, worldsInfo, sceneType) { }
    }
}