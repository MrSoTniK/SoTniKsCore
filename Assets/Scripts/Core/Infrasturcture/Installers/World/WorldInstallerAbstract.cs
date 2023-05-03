using Leopotam.Ecs;
using System;
using UnityEngine;
using Zenject;
using Core.Infrastructure.Installers.Data;

namespace Core.Infrastructure.Installers.World 
{
    public abstract class WorldInstallerAbstract<TSceneType, TSceneInfo, TDataInstaller> : MonoInstaller 
        where TSceneType : Enum where TSceneInfo : SceneInfoAbstract<TSceneType>, new() where TDataInstaller : DataInstaller<TSceneType, TSceneInfo>
    {
        [SerializeField] protected DataInstaller<TSceneType, TSceneInfo> DataInstaller;

        private WorldsInfo _worldsInfo;

        public override void InstallBindings()
        {
            BindWorld();
        }

        [Inject]
        public void Construct(WorldsInfo worldsInfo) 
        {
            _worldsInfo = worldsInfo;
        }

        protected virtual void BindWorld()
        {
            EcsWorld world = new();

            int key = Convert.ToInt32(DataInstaller.SceneTypeProp);
            _worldsInfo.WorldsDictionary.Add(key, world);
        }
    }
}