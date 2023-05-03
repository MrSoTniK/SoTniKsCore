using Core.AI.GOAP.Agents;
using Core.AI.GOAP.Goals;
using Core.AI.GOAP.WorldStates;
using Leopotam.Ecs;
using System;
using System.Collections.Generic;

namespace Core.AI.GOAP.Planners 
{
    public abstract class GoalsCheckingSystemAbstract<TAgentsContainer, TAgentInfo, TGoalType, TWorldStatesInfo, TGoalChecker> : IEcsRunSystem 
        where TAgentsContainer : AgentsContainerAbstract<TAgentInfo> 
        where TGoalType : Enum 
        where TWorldStatesInfo : WorldStatesInfoAbstract<TGoalType>
        where TGoalChecker : GoalCheckerBase<TGoalType, TWorldStatesInfo, TAgentInfo>
    {
        protected readonly Dictionary<TGoalType, TGoalChecker> GoalsDictionary;
        protected readonly TAgentsContainer AgentsContainer;

       public GoalsCheckingSystemAbstract(Dictionary<TGoalType, TGoalChecker> goalsDictionary, TAgentsContainer agentsContainer) 
       {
            GoalsDictionary = goalsDictionary;
            AgentsContainer = agentsContainer;
       }

        public virtual void Run()
        {
            CheckGoals();
        }

        protected virtual void CheckGoals() 
        {
            foreach (var agentInfo in AgentsContainer.AgentInfosList)
            {
                var goalType = GetGoalType(agentInfo);
                if (GoalsDictionary.ContainsKey(goalType) && GoalsDictionary[goalType].Check(agentInfo))
                {
                    SetNewGoal(agentInfo);
                }
            }
        }

        protected abstract TGoalType GetGoalType(TAgentInfo agentInfo);
        protected abstract void SetNewGoal(TAgentInfo agentInfo);
    }
}