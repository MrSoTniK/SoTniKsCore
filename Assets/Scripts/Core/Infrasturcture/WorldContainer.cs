using Leopotam.Ecs;

namespace Core.Infrastructure 
{
    public static class WorldContainer
    {
        public static EcsWorld World { get; private set; }

        public static void Init(EcsWorld world) 
        {
            World = world;
        }

        public static void Destroy() 
        {
            World.Destroy();
        }
    }
}