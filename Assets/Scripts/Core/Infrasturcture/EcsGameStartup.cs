using Leopotam.Ecs;
using Zenject;
using System.Collections.Generic;
using Core.Tools;

namespace Core.Infrastructure 
{
    public class EcsGameStartup : IInitializable, ITickable, IFixedTickable, ILateDisposable
    {
        private EcsWorld _world;
        private EcsSystems _initializeSystems;
        private EcsSystems _preInitializeSystems;
        private EcsSystems _updateSystems;
        private EcsSystems _fixedUpdateSystems;

        private List<IEcsPreInitSystem> _ecsPreInitSystems;
        private List<IEcsInitSystem> _ecsInitSystems;
        private List<IEcsRunSystem> _ecsRunSystems;
        private List<IEcsRunSystem> _ecsFixedRunSystems;

        public EcsWorld World => _world;

        public EcsGameStartup(List<IEcsPreInitSystem> ecsPreInitSystems, List<IEcsInitSystem> ecsInitSystems,
                              List<IEcsRunSystem> ecsRunSystems, List<IEcsRunSystem> ecsFixedRunSystems, EcsWorld world)
        {
            _world = world;

            _preInitializeSystems = new(_world);
            _initializeSystems = new(_world);
            _updateSystems = new(_world);
            _fixedUpdateSystems = new(_world);

            _ecsPreInitSystems = ecsPreInitSystems;
            _ecsInitSystems = ecsInitSystems;
            _ecsRunSystems = ecsRunSystems;
            _ecsFixedRunSystems = ecsFixedRunSystems;
        }

        public void Initialize()
        {
            /*We need only one system for this method because this method creates EcsSystem which
            must exist as single exemplar only!*/
           /* _updateSystems.ConvertScene();*/

            AddSystems();
            AddOneFrames();
            AddInjections();

            _preInitializeSystems.Init();
            _initializeSystems.Init();
            _updateSystems.Init();
            _fixedUpdateSystems.Init();
        }

        public void Tick()
        {
            _updateSystems.Run();
        }

        public void FixedTick()
        {
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

            if(_world != null)
            {
                _world.Destroy();
            }

            ScenesLoader.Instance.Clear();
        }

        private void AddSystems() 
        {
            foreach(var system in _ecsPreInitSystems) 
                _preInitializeSystems.Add(system);

            foreach (var system in _ecsInitSystems)
                _initializeSystems.Add(system);

            foreach (var system in _ecsRunSystems)
                _updateSystems.Add(system);

            foreach (var system in _ecsFixedRunSystems)
                _fixedUpdateSystems.Add(system);
        }

        private void AddOneFrames()
        {

        }

        private void AddInjections() 
        {
            
        }
    }
}