using Leopotam.Ecs;
using System.Collections.Generic;

namespace Core.Infrastructure 
{
    public class WorldsInfo
    {
        public Dictionary<int, EcsWorld> WorldsDictionary { get; private set; } = new();
    }
}