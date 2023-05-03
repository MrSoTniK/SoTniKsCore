using Core.AI.GOAP.Goals;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.AI.GOAP.Agents 
{
    public abstract class AgentInfo<TAgentType, TGoalType, TConditionType, TActionType, TOwner> where TAgentType : Enum 
        where TGoalType : Enum where TConditionType : Enum where TActionType : Enum
    {
        public TOwner Owner { get; private set; }
        public TAgentType AgentType { get; private set; }
        public Dictionary<TGoalType, GoalInfoBase<TGoalType, TConditionType>> Goals { get; private set; }
        public TGoalType CurrentGoal { get; private set; }
        public Queue<TActionType> ActionsQueue { get; private set; }

        public AgentInfo(TOwner ownerView, TAgentType agentType) 
        {
            Owner = ownerView;
            AgentType = agentType;
            Goals = new();
            ActionsQueue = new();
        }

        public void AddGoal(TGoalType goalType, GoalInfoBase<TGoalType, TConditionType> goal) 
        {
            Goals.Add(goalType, goal);
        }

        public void RemoveGoal(TGoalType goalType)
        {
            Goals.Remove(goalType);
        }

        public void AddActionToQueue(TActionType action) 
        {
            ActionsQueue.Enqueue(action);
        }

        public void DequeueCurrentAction()
        {
            ActionsQueue.Dequeue();
        }

        public virtual void SetNewQueue(List<TActionType> actionsList)
        {
            ActionsQueue = new Queue<TActionType>(actionsList);
        }

        public virtual void SetCurrentGoal(TGoalType goalType)
        {
            if (Goals.ContainsKey(goalType))
                CurrentGoal = goalType;
        }

        public virtual void ChangePriority(TGoalType goalType, float priority) 
        {
            if(Goals.ContainsKey(goalType))
                Goals[goalType].ChangePriority(priority);
        }

        public virtual TGoalType FindWithMaxPriority()
        {
            var goalsArray =  Goals.Values.ToArray();
            float maxPriority = float.MinValue;
            TGoalType goalTypeWithMaxPrior = default; 

            foreach(var goal in goalsArray) 
            {
                if(goal.CurrentPriority > maxPriority) 
                {
                    maxPriority = goal.CurrentPriority;
                    goalTypeWithMaxPrior = goal.GoalType;
                }
            }

            return goalTypeWithMaxPrior;
        }
    }
}