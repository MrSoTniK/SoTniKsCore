using System;

namespace Core.Infrastructure 
{
    public abstract class SceneInfoAbstract<TType> where TType : Enum
    {
        public TType SceneType;
    }
}