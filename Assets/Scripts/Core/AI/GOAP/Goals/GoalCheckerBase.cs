using Core.AI.GOAP.WorldStates;
using System;

namespace Core.AI.GOAP.Goals 
{  
    public abstract class GoalCheckerBase<TGoalType, TWorldStatesInfo, TAgentInfo> where TGoalType : Enum 
        where TWorldStatesInfo : WorldStatesInfoAbstract<TGoalType>
    {
        private readonly TWorldStatesInfo _worldStatesInfo;

        public GoalCheckerBase(TWorldStatesInfo worldStatesInfo) 
        {
            _worldStatesInfo = worldStatesInfo;
        }

        public abstract bool Check(TAgentInfo agentInfo);
    }   
}