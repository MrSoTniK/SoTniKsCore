using Core.Infrastructure;
using Leopotam.Ecs;
using System;

namespace Core.Tools 
{
    public static class WorldGetter<TSceneType, TSceneInfo>
       where TSceneType : Enum
       where TSceneInfo : SceneInfoAbstract<TSceneType>
    {
        public static EcsWorld GetWorld(TSceneInfo sceneInfo, WorldsInfo worldsInfo)
        {
            int key = Convert.ToInt32(sceneInfo.SceneType);
            return worldsInfo.WorldsDictionary[key];
        }
    }   
}