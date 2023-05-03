using System;

namespace Core.AI.GOAP.Goals
{
    [Serializable]
    public struct GoalDataAbstract<TGoalType, TConditionType> where TGoalType : Enum where TConditionType : Enum
    {
        public TGoalType GoalType;
        public TConditionType ConditionType;
        public float Priority;
        public float MinPriority;
        public float MaxPriority;
    }
}