using System;
using UnityEngine;

namespace Core.Views 
{
    public abstract class ElementViewBase<TType> : ViewBase where TType : Enum
    {
        [SerializeField] protected TType Type;

        public TType TypeProp => Type;
    }
}