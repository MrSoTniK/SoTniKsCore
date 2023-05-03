using System;
using UnityEngine;

namespace Core.Data 
{
    public abstract class DataAbstract
    {
    
    }

    public abstract class DataAbstract<TType> where TType : Enum
    {
        protected TType Type;

        public TType TypeProp => Type;
    }

    public abstract class DataAbstractSO<TType> : ScriptableObject where TType : Enum
    {
        [SerializeField] protected TType Type;

        public TType TypeProp => Type;
    }
}