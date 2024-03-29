using Leopotam.Ecs;
using Zenject;
using System.Collections.Generic;
using System;

namespace Core.Infrastructure 
{
    public abstract class EcsSceneStartup<TSceneType> : IInitializable, ITickable, IFixedTickable, ILateDisposable where TSceneType : Enum
    {
        protected readonly EcsWorld World;
        protected readonly WorldsInfo WorldsInfo;

        protected readonly TSceneType SceneType;

        protected EcsSystems _initializeSystems;
        protected EcsSystems _preInitializeSystems;
        protected EcsSystems _updateSystems;
        protected EcsSystems _fixedUpdateSystems;

        protected List<IEcsPreInitSystem> _ecsPreInitSystems;
        protected List<IEcsInitSystem> _ecsInitSystems;
        protected List<IEcsRunSystem> _ecsRunSystems;
        protected List<IEcsRunSystem> _ecsFixedRunSystems;

        protected bool UpdateSystemsExist;
        protected bool FixedUpdateSystemsExist;

        public EcsSceneStartup(List<IEcsPreInitSystem> ecsPreInitSystems, List<IEcsInitSystem> ecsInitSystems,
                              List<IEcsRunSystem> ecsRunSystems, List<IEcsRunSystem> ecsFixedRunSystems, 
                              EcsWorld world, WorldsInfo worldsInfo, TSceneType sceneType) 
        {
            World = world;
            WorldsInfo = worldsInfo;
            SceneType = sceneType;
            _preInitializeSystems = new(World);
            _initializeSystems = new(World);
            _updateSystems = new(World);
            _fixedUpdateSystems = new(World);

            _ecsPreInitSystems = ecsPreInitSystems;
            _ecsInitSystems = ecsInitSystems;
            _ecsRunSystems = ecsRunSystems;
            _ecsFixedRunSystems = ecsFixedRunSystems;
        }

        public void Initialize()
        {
            AddSystems();
            AddOneFrames();
            AddInjections();

            _preInitializeSystems.Init();
            _initializeSystems.Init();
            _updateSystems.Init();
            _fixedUpdateSystems.Init();

            UpdateSystemsExist = _updateSystems.GetAllSystems().Count > 0;
            FixedUpdateSystemsExist = _fixedUpdateSystems.GetAllSystems().Count > 0;
        }

        public void Tick()
        {
            if(UpdateSystemsExist)
                _updateSystems.Run();
        }

        public void FixedTick()
        {
            if (FixedUpdateSystemsExist)
                _fixedUpdateSystems.Run();
        }

        public void LateDispose()
        {
            if (_preInitializeSystems != null)
            {
                _preInitializeSystems.Destroy();
                _ecsPreInitSystems.Clear();

                _ecsPreInitSystems = null;
                _preInitializeSystems = null;
            }

            if (_initializeSystems != null)
            {
                _initializeSystems.Destroy();
                _ecsInitSystems.Clear();

                _ecsInitSystems = null;
                _initializeSystems = null;
            }

            if (_updateSystems != null)
            {
                _updateSystems.Destroy();
                _ecsRunSystems.Clear();

                _ecsRunSystems = null;
                _updateSystems = null;
            }

            if (_fixedUpdateSystems != null)
            {
                _fixedUpdateSystems.Destroy();
                _ecsFixedRunSystems.Clear();

                _ecsFixedRunSystems = null;
                _fixedUpdateSystems = null;
            }

            if (World != null) 
            {
                World.Destroy();
                int key = Convert.ToInt32(SceneType);
                if (WorldsInfo.WorldsDictionary.ContainsKey(key)) 
                {
                    WorldsInfo.WorldsDictionary.Remove(key);
                }
            }
        }     

        protected virtual void AddSystems()
        {
            foreach (var system in _ecsPreInitSystems)
                _preInitializeSystems.Add(system);

            foreach (var system in _ecsInitSystems)
                _initializeSystems.Add(system);

            foreach (var system in _ecsRunSystems)
                _updateSystems.Add(system);

            foreach (var system in _ecsFixedRunSystems)
                _fixedUpdateSystems.Add(system);
        }

        protected virtual void AddOneFrames()
        {

        }

        protected virtual void AddInjections()
        {

        }
    }
}