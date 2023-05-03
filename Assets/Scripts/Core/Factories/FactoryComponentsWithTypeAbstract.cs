using Core.Data;
using Core.Infrastructure;
using Core.ScriptableObjects;
using Core.Tools;
using Core.Views;
using Leopotam.Ecs;
using System;
using System.Collections.Generic;
using UnityEngine;
using Voody.UniLeo;
using Zenject;

namespace Core.Factories 
{
    public abstract class FactoryComponentsBase<TElement, TSceneType, TSceneInfo> 
        where TElement : ViewBase
        where TSceneType : Enum
        where TSceneInfo : SceneInfoAbstract<TSceneType>
    {
        protected WorldsInfo WorldsInfo;
        protected TSceneInfo SceneInfo;

        protected void AddEntity(ConvertToEntity convertToEntityComponent, TElement element, EcsWorld world)
        {
            // Creating new Entity
            EcsEntity entity = world.NewEntity();
            element.Entity = entity;

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

    public abstract class FactoryComponentsAbstract<TDataBase, TElement, TSceneType, TSceneInfo> : FactoryComponentsBase<TElement, TSceneType, TSceneInfo>, IFactory 
        where TDataBase : DataBaseAbstract<TElement> 
        where TElement : ViewBase
        where TSceneType : Enum
        where TSceneInfo : SceneInfoAbstract<TSceneType>
    {
        protected TDataBase DataBase;
        protected Randomizer Randomizer;

        public FactoryComponentsAbstract(TDataBase dataBase, Randomizer randomizer, WorldsInfo worldsInfo, TSceneInfo sceneInfo)
        {
            DataBase = dataBase;
            Randomizer = randomizer;
            WorldsInfo = worldsInfo;
            SceneInfo = sceneInfo;
        }      

        public virtual TElement Create(int index, Vector3 position)
        {
            if (DataBase.GetElementByIndex(index, out TElement element))
            {
                TElement createdElement =  UnityEngine.Object.Instantiate(element, position, Quaternion.identity);

                if (createdElement.TryGetComponent<ConvertToEntity>(out ConvertToEntity convertToEntity)) 
                {
                    EcsWorld world = WorldGetter<TSceneType, TSceneInfo>.GetWorld(SceneInfo, WorldsInfo);
                    AddEntity(convertToEntity, createdElement, world);
                }

                return createdElement;
            }

            return null;
        }

        public virtual TElement Create(int index, Vector3 position, Transform parent)
        {
            if (DataBase.GetElementByIndex(index, out TElement element))
            {
                TElement createdElement = UnityEngine.Object.Instantiate(element, position, Quaternion.identity, parent);

                if (createdElement.TryGetComponent<ConvertToEntity>(out ConvertToEntity convertToEntity))
                {
                    EcsWorld world = WorldGetter<TSceneType, TSceneInfo>.GetWorld(SceneInfo, WorldsInfo);
                    AddEntity(convertToEntity, createdElement, world);
                }

                return createdElement;
            }

            return null;
        }

        public virtual TElement Create(int index, Vector3 position, Transform parent, Quaternion rotation)
        {
            if (DataBase.GetElementByIndex(index, out TElement element))
            {
                TElement createdElement = UnityEngine.Object.Instantiate(element, position, rotation, parent);

                if (createdElement.TryGetComponent<ConvertToEntity>(out ConvertToEntity convertToEntity))
                {
                    EcsWorld world = WorldGetter<TSceneType, TSceneInfo>.GetWorld(SceneInfo, WorldsInfo);
                    AddEntity(convertToEntity, createdElement, world);
                }

                return createdElement;
            }

            return null;
        }

        public TElement CreateRandom(Vector3 position)
        {
            TElement element = DataBase.GetRandomElement(Randomizer);
            TElement createdElement = UnityEngine.Object.Instantiate(element, position, Quaternion.identity);

            if (createdElement.TryGetComponent<ConvertToEntity>(out ConvertToEntity convertToEntity))
            {
                EcsWorld world = WorldGetter<TSceneType, TSceneInfo>.GetWorld(SceneInfo, WorldsInfo);
                AddEntity(convertToEntity, createdElement, world);
            }

            return createdElement;
        }

        public TElement CreateRandom(Vector3 position, Transform parent)
        {
            TElement element = DataBase.GetRandomElement(Randomizer);
            TElement createdElement = UnityEngine.Object.Instantiate(element, position, Quaternion.identity, parent);

            if (createdElement.TryGetComponent<ConvertToEntity>(out ConvertToEntity convertToEntity))
            {
                EcsWorld world = WorldGetter<TSceneType, TSceneInfo>.GetWorld(SceneInfo, WorldsInfo);
                AddEntity(convertToEntity, createdElement, world);
            }

            return createdElement;
        }      
    }

    public abstract class FactoryComponentsAbstract<TDataBase, TElement, TData, TSceneType, TSceneInfo> : 
        FactoryComponentsBase<TElement, TSceneType, TSceneInfo>, IFactory 
        where TDataBase : DataBaseAbstract<TData>
        where TElement : ViewBase 
        where TData : DataBaseAbstract<TElement>
        where TSceneType : Enum
        where TSceneInfo : SceneInfoAbstract<TSceneType>
    {
        protected TDataBase DataBase;
        protected Randomizer Randomizer;
        protected TData Data;

        public FactoryComponentsAbstract(TDataBase dataBase, Randomizer randomizer, WorldsInfo worldsInfo, TSceneInfo sceneInfo)
        {
            DataBase = dataBase;
            Randomizer = randomizer;
            WorldsInfo = worldsInfo;
            SceneInfo = sceneInfo;
        }
     
        public virtual TElement[] CreateAll(int dataIndex, Vector3 position) 
        {
            if (DataBase.GetElementByIndex(dataIndex, out TData data))
            {
                List<TElement> createdElements = new();

                foreach(TElement element in data.Elements) 
                {                    
                    TElement createdElement = UnityEngine.Object.Instantiate(element, position, Quaternion.identity);

                    if (createdElement.TryGetComponent<ConvertToEntity>(out ConvertToEntity convertToEntity))
                    {
                        EcsWorld world = WorldGetter<TSceneType, TSceneInfo>.GetWorld(SceneInfo, WorldsInfo);
                        AddEntity(convertToEntity, createdElement, world);
                    }

                    createdElements.Add(createdElement);
                }

                return createdElements.ToArray();
            }

            return null;
        }

        public virtual TElement[] CreateAll(int dataIndex, Vector3 position, Transform parent)
        {
            if (DataBase.GetElementByIndex(dataIndex, out TData data))
            {
                List<TElement> createdElements = new();

                foreach (TElement element in data.Elements)
                {
                    TElement createdElement = UnityEngine.Object.Instantiate(element, position, Quaternion.identity, parent);

                    if (createdElement.TryGetComponent<ConvertToEntity>(out ConvertToEntity convertToEntity))
                    {
                        EcsWorld world = WorldGetter<TSceneType, TSceneInfo>.GetWorld(SceneInfo, WorldsInfo);
                        AddEntity(convertToEntity, createdElement, world);
                    }

                    createdElements.Add(createdElement);
                }

                return createdElements.ToArray();
            }

            return null;
        }

        public virtual TElement Create(int dataIndex, int elementIndex, Vector3 position)
        {
            if (DataBase.GetElementByIndex(dataIndex, out TData data))
            {
                if(data.GetElementByIndex(elementIndex, out TElement element)) 
                {
                    TElement createdElement = UnityEngine.Object.Instantiate(element, position, Quaternion.identity);

                    if (createdElement.TryGetComponent<ConvertToEntity>(out ConvertToEntity convertToEntity))
                    {
                        EcsWorld world = WorldGetter<TSceneType, TSceneInfo>.GetWorld(SceneInfo, WorldsInfo);
                        AddEntity(convertToEntity, createdElement, world);
                    }

                    return createdElement;
                }
            }

            return null;
        }

        public virtual TElement Create(int dataIndex, int elementIndex, Vector3 position, Transform parent)
        {
            if (DataBase.GetElementByIndex(dataIndex, out TData data))
            {
                if (data.GetElementByIndex(elementIndex, out TElement element))
                {
                    TElement createdElement = UnityEngine.Object.Instantiate(element, position, Quaternion.identity, parent);

                    if (createdElement.TryGetComponent<ConvertToEntity>(out ConvertToEntity convertToEntity))
                    {
                        EcsWorld world = WorldGetter<TSceneType, TSceneInfo>.GetWorld(SceneInfo, WorldsInfo);
                        AddEntity(convertToEntity, createdElement, world);
                    }

                    return createdElement;
                }
            }

            return null;
        }

        public TElement CreateRandom(int dataIndex, Vector3 position)
        {
            if(DataBase.GetElementByIndex(dataIndex, out TData data))
            {
                TElement element = data.GetRandomElement(Randomizer);
                TElement createdElement = UnityEngine.Object.Instantiate(element, position, Quaternion.identity);


                if (createdElement.TryGetComponent<ConvertToEntity>(out ConvertToEntity convertToEntity))
                {
                    EcsWorld world = WorldGetter<TSceneType, TSceneInfo>.GetWorld(SceneInfo, WorldsInfo);
                    AddEntity(convertToEntity, createdElement, world);
                }

                return createdElement;
            }

            return null;
        }

        public TElement CreateRandom(int dataIndex, Vector3 position, Transform parent)
        {

            if (DataBase.GetElementByIndex(dataIndex, out TData data))
            {
                TElement element = data.GetRandomElement(Randomizer);
                TElement createdElement = UnityEngine.Object.Instantiate(element, position, Quaternion.identity, parent);


                if (createdElement.TryGetComponent<ConvertToEntity>(out ConvertToEntity convertToEntity))
                {
                    EcsWorld world = WorldGetter<TSceneType, TSceneInfo>.GetWorld(SceneInfo, WorldsInfo);
                    AddEntity(convertToEntity, createdElement, world);
                }

                return createdElement;
            }

            return null;
        }
    }

    public abstract class FactoryComponentsWithTypeAbstract<TDataBase, TElement, TType, TSceneType, TSceneInfo> : 
        FactoryComponentsBase<TElement, TSceneType, TSceneInfo>, IFactory
        where TDataBase : DataBaseViewsAbstract<TElement, TType>
        where TElement : ElementViewBase<TType>
        where TType : Enum
        where TSceneType : Enum
        where TSceneInfo : SceneInfoAbstract<TSceneType>
    {
        protected TDataBase DataBase;
        protected Randomizer Randomizer;

        public FactoryComponentsWithTypeAbstract(TDataBase dataBase, Randomizer randomizer, WorldsInfo worldsInfo, TSceneInfo sceneInfo)
        {
            DataBase = dataBase;
            Randomizer = randomizer;
            WorldsInfo = worldsInfo;
            SceneInfo = sceneInfo;
        }

        public virtual TElement Create(int dataIndex, int elementIndex, Vector3 position)
        {
            if (DataBase.GetElementByIndex(dataIndex, out TElement elementTemplate))
            {
                TElement createdElement = UnityEngine.Object.Instantiate(elementTemplate, position, Quaternion.identity);

                if (createdElement.TryGetComponent<ConvertToEntity>(out ConvertToEntity convertToEntity))
                {
                    EcsWorld world = WorldGetter<TSceneType, TSceneInfo>.GetWorld(SceneInfo, WorldsInfo);
                    AddEntity(convertToEntity, createdElement, world);
                }

                return createdElement;
            }

            return null;
        }

        public virtual TElement Create(TType type, Vector3 position)
        {
            if (DataBase.ElementsDictionary.ContainsKey(type))
            {
                TElement elementTemplate = DataBase.ElementsDictionary[type];

                TElement createdElement = UnityEngine.Object.Instantiate(elementTemplate, position, Quaternion.identity);

                if (createdElement.TryGetComponent<ConvertToEntity>(out ConvertToEntity convertToEntity))
                {
                    EcsWorld world = WorldGetter<TSceneType, TSceneInfo>.GetWorld(SceneInfo, WorldsInfo);
                    AddEntity(convertToEntity, createdElement, world);
                }

                return createdElement;
            }

            return null;
        }

        public virtual TElement Create(TType type, Vector3 position, Transform parent)
        {
            if (DataBase.ElementsDictionary.ContainsKey(type))
            {
                TElement elementTemplate = DataBase.ElementsDictionary[type];
                
                TElement createdElement = UnityEngine.Object.Instantiate(elementTemplate, position, Quaternion.identity, parent);

                if (createdElement.TryGetComponent<ConvertToEntity>(out ConvertToEntity convertToEntity))
                {
                    EcsWorld world = WorldGetter<TSceneType, TSceneInfo>.GetWorld(SceneInfo, WorldsInfo);
                    AddEntity(convertToEntity, createdElement, world);
                }

                return createdElement;
            }

            return null;
        }

        public virtual TElement Create(TType type, Vector3 position, Transform parent, Quaternion rotation)
        {
            if (DataBase.ElementsDictionary.ContainsKey(type))
            {
                TElement elementTemplate = DataBase.ElementsDictionary[type];
                TElement createdElement = UnityEngine.Object.Instantiate(elementTemplate, position, rotation, parent);

                if (createdElement.TryGetComponent<ConvertToEntity>(out ConvertToEntity convertToEntity))
                {
                    EcsWorld world = WorldGetter<TSceneType, TSceneInfo>.GetWorld(SceneInfo, WorldsInfo);
                    AddEntity(convertToEntity, createdElement, world);
                }

                return createdElement;
            }

            return null;
        }

        public TElement CreateRandom(Vector3 position)
        {
            TElement elementTemplate = DataBase.GetRandomElement(Randomizer);

            if (elementTemplate != null)
            {
                TElement createdElement = UnityEngine.Object.Instantiate(elementTemplate, position, Quaternion.identity);

                if (createdElement.TryGetComponent<ConvertToEntity>(out ConvertToEntity convertToEntity))
                {
                    EcsWorld world = WorldGetter<TSceneType, TSceneInfo>.GetWorld(SceneInfo, WorldsInfo);
                    AddEntity(convertToEntity, createdElement, world);
                }

                return createdElement;
            }

            return null;
        }
    }

    public abstract class FactoryComponentsWithTypeAbstract<TDataBase, TElement, TData, TType, TSceneType, TSceneInfo> : FactoryComponentsBase<TElement, TSceneType, TSceneInfo>, IFactory 
       where TDataBase : DataBaseAbstract<TData, TType>
       where TElement : ViewBase
       where TData : DataAbstractSO<TType> 
       where TType : Enum
       where TSceneType : Enum
       where TSceneInfo : SceneInfoAbstract<TSceneType>
    {
        protected TDataBase DataBase;
        protected Randomizer Randomizer;
        protected TData Data;
 
        public FactoryComponentsWithTypeAbstract(TDataBase dataBase, Randomizer randomizer, WorldsInfo worldsInfo, TSceneInfo sceneInfo)
        {
            DataBase = dataBase;
            Randomizer = randomizer;
            WorldsInfo = worldsInfo;
            SceneInfo = sceneInfo;
        }

        public abstract bool GetTemplateFromData(TData data, out TElement element);           

        public virtual TElement Create(int dataIndex, int elementIndex, Vector3 position, out TData data)
        {
            if (DataBase.GetElementByIndex(dataIndex, out data))
            {
                if (GetTemplateFromData(data, out TElement elementTemplate))
                {
                    TElement createdElement = UnityEngine.Object.Instantiate(elementTemplate, position, Quaternion.identity);

                    if (createdElement.TryGetComponent<ConvertToEntity>(out ConvertToEntity convertToEntity))
                    {
                        EcsWorld world = WorldGetter<TSceneType, TSceneInfo>.GetWorld(SceneInfo, WorldsInfo);
                        AddEntity(convertToEntity, createdElement, world);
                    }

                    return createdElement;
                }
            }

            return null;
        }

        public virtual TElement Create(TType type, Vector3 position, Transform parent, out TData data)
        {
            data = default;

            if (DataBase.ElementsDictionary.ContainsKey(type))
            {
                data = DataBase.ElementsDictionary[type];
                if (GetTemplateFromData(data, out TElement element))
                {
                    TElement createdElement = UnityEngine.Object.Instantiate(element, position, Quaternion.identity, parent);

                    if (createdElement.TryGetComponent<ConvertToEntity>(out ConvertToEntity convertToEntity))
                    {
                        EcsWorld world = WorldGetter<TSceneType, TSceneInfo>.GetWorld(SceneInfo, WorldsInfo);
                        AddEntity(convertToEntity, createdElement, world);
                    }

                    return createdElement;
                }
            }

            return null;
        }

        public TElement CreateRandom(Vector3 position, out TData data)
        {
            data = DataBase.GetRandomElement(Randomizer);

            if(GetTemplateFromData(data, out TElement element)) 
            {
                TElement createdElement = UnityEngine.Object.Instantiate(element, position, Quaternion.identity);

                if (createdElement.TryGetComponent<ConvertToEntity>(out ConvertToEntity convertToEntity))
                {
                    EcsWorld world = WorldGetter<TSceneType, TSceneInfo>.GetWorld(SceneInfo, WorldsInfo);
                    AddEntity(convertToEntity, createdElement, world);
                }

                return createdElement;
            }          

            return null;
        }
    }
}