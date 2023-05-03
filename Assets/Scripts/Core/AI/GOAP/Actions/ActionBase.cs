using System;
using UnityEngine;

namespace Core.AI.GOAP.Actions
{
    public abstract class ActionBase<TActionType, TConditionType, TAgentInfo> : ScriptableObject where TActionType : Enum where TConditionType : Enum
    {
        [SerializeField] private TActionType _actionType;
        [SerializeField] private TConditionType _precondition;
        [SerializeField] private TConditionType _effect;
        [SerializeField] private float _cost;

        public TActionType ActionType => _actionType;
        public TConditionType Precondition => _precondition;
        public TConditionType Effect => _effect;
        public float Cost => _cost;

        public abstract void Perform(TAgentInfo agentInfo);
    }
}