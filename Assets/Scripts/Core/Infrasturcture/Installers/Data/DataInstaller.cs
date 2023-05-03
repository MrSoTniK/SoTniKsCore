using System;
using UnityEngine;
using Zenject;

namespace Core.Infrastructure.Installers.Data 
{
    public abstract class DataInstaller<TType, TSceneInfo> : MonoInstaller where TType : Enum where TSceneInfo : SceneInfoAbstract<TType>, new()
    {
        [SerializeField] protected TType SceneType;

        public TType SceneTypeProp => SceneType;

        public override void InstallBindings()
        {
            TSceneInfo sceneInfo = new();
            sceneInfo.SceneType = SceneType;
            Container.Bind<TSceneInfo>().FromInstance(sceneInfo).AsSingle();

            BindData();
        }

        protected abstract void BindData();       
    }
}