using System.Collections.Generic;

namespace Core.Infrastructure.Controllers 
{
    public abstract class RxFieldsControllerBase<TControlledEntity, TValue, TRxField> : IController where TRxField : RxField<TValue>
    {
        public RxFieldsControllerBase(List<TControlledEntity> controlledEntities) 
        {
            for(int i = 0; i < controlledEntities.Count; i++) 
            {
                var rxField = GetRxValueFromEntity(controlledEntities[i]);
                rxField.OnUpdate += OnValueUpdate;
            }
        }

        protected abstract TRxField GetRxValueFromEntity(TControlledEntity controlledEntity);

        protected abstract void OnValueUpdate(RxValue<TValue> rxValue);
    }
}