using Leopotam.Ecs;

namespace Core.Views 
{
    public class ViewsEntityInitializingSystem : IEcsRunSystem
    {
        private readonly EcsFilter<InitializeViewRequest> _viewsFilter = null;      

        public void Run()
        {
            foreach (var i in _viewsFilter)
            {
                ref var entity = ref _viewsFilter.GetEntity(i);
                ref var request = ref _viewsFilter.Get1(i);
                request.View.Entity = entity;
                entity.Del<InitializeViewRequest>();
            }
        }
    }
}