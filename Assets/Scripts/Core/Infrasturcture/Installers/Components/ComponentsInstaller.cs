using Core.Tools;
using Leopotam.Ecs;
using System;
using UnityEngine;
using Voody.UniLeo;
using Zenject;

namespace Core.Infrastructure.Installers.Componenets 
{
    public abstract class ComponentsInstaller<TSceneInfo, TSceneType> : MonoInstaller where TSceneInfo : SceneInfoAbstract<TSceneType>
        where TSceneType : Enum
    {
        protected WorldsInfo WorldsInfo;
        protected TSceneInfo SceneInfo;

        public override void InstallBindings() 
        {
            BindComponents();
        }

        [Inject]
        public void Construct(WorldsInfo worldsInfo, TSceneInfo sceneInfo) 
        {
            WorldsInfo = worldsInfo;
            SceneInfo = sceneInfo;
        }

        protected virtual void BindComponents() 
        {         
            var convertableGameObjects =
               GameObject.FindObjectsOfType<ConvertToEntity>();
            // Iterate throught all gameobjects, that has ECS Components

            EcsWorld world = WorldGetter<TSceneType, TSceneInfo>.GetWorld(SceneInfo, WorldsInfo);

            foreach (var convertable in convertableGameObjects)
            {
                AddEntity(convertable.gameObject, world);
            }
        }

        private void AddEntity(GameObject gameObject, EcsWorld world)
        {
            // Creating new Entity
            EcsEntity entity = world.NewEntity();
            ConvertToEntity convertComponent = gameObject.GetComponent<ConvertToEntity>();
            if (convertComponent)
            {
                foreach (var component in gameObject.GetComponents<Component>())
                {
                    if (component is IConvertToEntity entityComponent)
                    {
                        // Adding Component to entity
                        entityComponent.Convert(entity);
                        GameObject.DestroyImmediate(component);
                    }
                }

                convertComponent.setProccessed();
                switch (convertComponent.convertMode)
                {
                    case ConvertMode.ConvertAndDestroy:
                        GameObject.DestroyImmediate(gameObject);
                        break;
                    case ConvertMode.ConvertAndInject:
                        GameObject.DestroyImmediate(convertComponent);
                        break;
                    case ConvertMode.ConvertAndSave:
                        convertComponent.Set(entity);
                        break;
                }
            }
        }
    }
}