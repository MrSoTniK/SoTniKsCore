namespace Core.Infrastructure.Controllers 
{
    public abstract class RxFieldsControllerBase<TControlledEntity, TValue, TRxField> : IController where TRxField : RxField<TValue>
    {
        protected readonly TControlledEntity ControlledEntity;

        public RxFieldsControllerBase(TControlledEntity controlledEntity) 
        {
            ControlledEntity = controlledEntity;
            var rxField = GetRxValueFromEntity(controlledEntity);
            rxField.OnUpdate += OnValueUpdate;
        }

        protected abstract TRxField GetRxValueFromEntity(TControlledEntity controlledEntity);

        protected abstract void OnValueUpdate(RxValue<TValue> rxValue);
    }
}