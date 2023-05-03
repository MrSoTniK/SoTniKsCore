using System;
using System.Collections.Generic;

namespace Core.AI.GOAP.WorldStates 
{
    public abstract class WorldStatesInfoAbstract<TConditionType> where TConditionType : Enum
    {
        public Dictionary<TConditionType, bool> WorldStates;
    }
}