using System;

namespace Core.AI.GOAP.Goals 
{
    public class GoalInfoBase<TGoalType, TConditionType> where TGoalType : Enum where TConditionType : Enum
    {
        public TGoalType GoalType { get; private set; }
        public TConditionType ConditionType { get; private set; }
        public float CurrentPriority { get; private set; }
        public float MinPriority { get; private set; }
        public float MaxPriority { get; private set; }

        public GoalInfoBase(TGoalType goalType, TConditionType conditionType, float priority, float minPriority,
            float maxPriority) 
        {
            GoalType = goalType;
            ConditionType = conditionType;
            CurrentPriority = priority;
            MinPriority = minPriority;
            MaxPriority = maxPriority;
        }

        public void ChangePriority(float newPriorityValue) 
        {
            CurrentPriority = newPriorityValue;
        }
    }
}