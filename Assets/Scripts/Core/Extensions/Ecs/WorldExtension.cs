using Leopotam.Ecs;

namespace Core.Extensions.Ecs 
{
    public static class WorldExtension
    {
        public static void SendMessage<TStruct>(this EcsWorld world, in TStruct message) where TStruct : struct 
        {
            world.NewEntity().Get<TStruct>() = message;
        }
    }
}