using System.Collections.Generic;

namespace Core.Infrastructure.Controllers 
{
    public abstract class RxFieldsListControllerBase<TControlledListEntity, TValue, TRxField> : IController where TRxField : RxField<TValue>
    {
        public RxFieldsListControllerBase(List<TControlledListEntity> controlledEntities)
        {
            for (int i = 0; i < controlledEntities.Count; i++)
            {
                var rxField = GetRxValueFromEntity(controlledEntities[i]);
                rxField.OnUpdate += OnValueUpdate;
            }
        }

        protected abstract TRxField GetRxValueFromEntity(TControlledListEntity controlledEntity);

        protected abstract void OnValueUpdate(RxValue<TValue> rxValue);
    }
}