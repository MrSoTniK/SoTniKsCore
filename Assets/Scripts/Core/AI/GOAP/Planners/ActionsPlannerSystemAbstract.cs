using Core.AI.GOAP.Agents;
using Leopotam.Ecs;

namespace Core.AI.GOAP.Planners 
{
    public abstract class ActionsPlannerSystemAbstract<TAgentInfo, TAgentsContainer> : IEcsRunSystem 
        where TAgentsContainer : AgentsContainerAbstract<TAgentInfo>
    {
        protected readonly TAgentsContainer AgentsContainer;

        public abstract bool Condition(TAgentInfo agentInfo);

        public ActionsPlannerSystemAbstract(TAgentsContainer agentsContainer)
        {
            AgentsContainer = agentsContainer;
        }

        public virtual void Run()
        {
            TryToPlanActions();
        }

        protected void TryToPlanActions() 
        {
            foreach (var agentInfo in AgentsContainer.AgentInfosList)
            {
                if (Condition(agentInfo))
                {
                    PalnGoal(agentInfo);
                }
            }
        }

        protected abstract void PalnGoal(TAgentInfo agentInfo);
    }
}