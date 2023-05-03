using Core.Extensions.Ecs;
using Core.Infrastructure;
using Leopotam.Ecs;
using System;

namespace Core.Tools 
{
    public static class WorldMessageSender<TSceneType, TSceneInfo, TMessage>
           where TSceneType : Enum
           where TSceneInfo : SceneInfoAbstract<TSceneType>
           where TMessage : struct
    {
        public static void SendMessage(TSceneInfo sceneInfo, WorldsInfo worldsInfo, TMessage message)
        {
            EcsWorld world = GetWorld(sceneInfo, worldsInfo);
            world.SendMessage(message);
        }

        public static void SendMessage(TSceneInfo sceneInfo, WorldsInfo worldsInfo)
        {
            EcsWorld world = GetWorld(sceneInfo, worldsInfo);
            world.SendMessage(new TMessage());
        }

        public static EcsWorld GetWorld(TSceneInfo sceneInfo, WorldsInfo worldsInfo)
        {
            int key = Convert.ToInt32(sceneInfo.SceneType);
            return worldsInfo.WorldsDictionary[key];
        }
    }
}