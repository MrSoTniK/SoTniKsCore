using System.Collections.Generic;

namespace Core.Infrastructure.Controllers
{
    public abstract class ControllerBase<TControlledEntity> : IController
    {
        private readonly List<TControlledEntity> _controlledEntities;

        public ControllerBase(List<TControlledEntity> controlledEntities) 
        {
            _controlledEntities = controlledEntities;
        }

        protected abstract void OnEntityActionInvoked();
    }

    public abstract class ControllerBase<TControlledEntity, TValue> : IController
    {
        private readonly List<TControlledEntity> _controlledEntities;

        public ControllerBase(List<TControlledEntity> controlledEntities)
        {
            _controlledEntities = controlledEntities;
        }

        protected abstract void OnEntityActionInvoked(TValue value);
    }
}