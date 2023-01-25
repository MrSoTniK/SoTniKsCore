using Core.World;
using Leopotam.Ecs;
using System.Collections.Generic;
using Zenject;

namespace Core.Infrastructure.Installers.Systems 
{
    public abstract class SystemsInstaller : MonoInstaller
    {
        public List<IEcsPreInitSystem> EcsPreInitSystems { get; private set; }
        public List<IEcsInitSystem> EcsInitSystems { get; private set; }
        public List<IEcsRunSystem> EcsRunSystems { get; private set; }
        public List<IEcsRunSystem> EcsFixedRunSystems { get; private set; }

        public override void InstallBindings()
        {
            InitializeSystems();
            AddPreInitSystems();
            AddInitSystems();
            AddRunSystems();
            AddFixedRunSystems();
        }

        protected virtual void InitializeSystems()
        {
            EcsPreInitSystems = new();
            EcsInitSystems = new();
            EcsRunSystems = new();
            EcsFixedRunSystems = new();
        }


        protected abstract void AddPreInitSystems();

        protected abstract void AddInitSystems();

        protected abstract void AddRunSystems();

        protected abstract void AddFixedRunSystems();
    }
}