using Core.World;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;

namespace Core.Infrastructure.Installers.Bootstrap 
{
    public abstract class BootstrapSceneInstaller<TSystemsInstaller, TEcsSceneStratup> : MonoInstaller where TSystemsInstaller : MonoInstaller
        where TEcsSceneStratup : EcsSceneStartup
    {
        [SerializeField] protected TSystemsInstaller SceneSystemsInstaller;

        protected EcsWorld World;

        [Inject]
        public void Construct(EcsWorld world)
        {
            World = world;
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