using System;

namespace Core.Infrastructure
{
    public interface IReadonlyRxField<T>
    {
        public event Action<RxValue<T>> OnUpdate;

        public T Value { get; }
    }
}