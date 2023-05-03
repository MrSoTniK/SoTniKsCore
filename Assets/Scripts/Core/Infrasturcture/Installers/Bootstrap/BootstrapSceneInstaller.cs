using System;
using UnityEngine;
using Zenject;

namespace Core.Infrastructure.Installers.Bootstrap 
{
    public abstract class BootstrapSceneInstaller<TSystemsInstaller, TEcsSceneStratup, TSceneType, TSceneInfo> : MonoInstaller 
        where TSystemsInstaller : MonoInstaller
        where TEcsSceneStratup : EcsSceneStartup<TSceneType>
        where TSceneType : Enum
        where TSceneInfo : SceneInfoAbstract<TSceneType>
    {
        [SerializeField] protected TSystemsInstaller SceneSystemsInstaller;

        protected WorldsInfo WorldsInfo;
        protected TSceneInfo SceneInfo;

        [Inject]
        public void Construct(WorldsInfo worldsInfo, TSceneInfo sceneInfo)
        {
            WorldsInfo = worldsInfo;
            SceneInfo = sceneInfo;
        }

        protected TEcsSceneStratup EcsSceneStratup;

        public override void InstallBindings()
        {
            Boot();
        }

        protected virtual void Boot()
        {
            BindEscSceneStartup();
        }

        protected virtual void BindEscSceneStartup() 
        {
            
        }
    }
}