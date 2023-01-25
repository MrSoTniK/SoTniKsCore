using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace Core.World 
{
    public class WorldConvertingPreInitSystem : IEcsPreInitSystem
    {        
        public void PreInit()
        {
            var convertableGameObjects =
               GameObject.FindObjectsOfType<ConvertToEntity>();
            // Iterate throught all gameobjects, that has ECS Components
            foreach (var convertable in convertableGameObjects)
            {
                AddEntity(convertable);
            }
        }

        private void AddEntity(ConvertToEntity convertToEntityComponent)
        {
            // Creating new Entity
            EcsEntity entity = WorldHandler.GetWorld().NewEntity();
            if (convertToEntityComponent)
            {
                foreach (var component in convertToEntityComponent.gameObject.GetComponents<Component>())
                {
                    if (component is IConvertToEntity entityComponent)
                    {
                        // Adding Component to entity
                        entityComponent.Convert(entity);
                        GameObject.Destroy(component);
                    }
                }

                convertToEntityComponent.setProccessed();
                switch (convertToEntityComponent.convertMode)
                {
                    case ConvertMode.ConvertAndDestroy:
                        GameObject.Destroy(convertToEntityComponent.gameObject);
                        break;
                    case ConvertMode.ConvertAndInject:
                        GameObject.Destroy(convertToEntityComponent);
                        break;
                    case ConvertMode.ConvertAndSave:
                        convertToEntityComponent.Set(entity);
                        break;
                }
            }
        }
    }
}