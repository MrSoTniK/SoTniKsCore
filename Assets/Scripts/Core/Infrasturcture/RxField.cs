using System;
using UnityEngine;

namespace Core.Infrastructure
{
    [Serializable]
    public class RxField<T> : IReadonlyRxField<T>
    {
        public event Action<RxValue<T>> OnUpdate;

        [SerializeField] private T _value;

        public T Value
        {
            set
            {
                var oldValue = _value;
                _value = value;

                OnUpdate?.Invoke(new RxValue<T> {OldValue = oldValue, NewValue = _value});
            }
            get => _value;
        }

        public static implicit operator RxField<T>(T value) => new() {_value = value};
    }

    public struct RxValue<T>
    {
        public T OldValue;
        public T NewValue;
    }
}