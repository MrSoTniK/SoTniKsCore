using Core.ScriptableObjects;
using Core.Tools;
using Core.Views;
using UnityEngine;
using Zenject;

namespace Core.Factories 
{
    public abstract class FactoryAbstract<TDataBase, TElement> : IFactory where TDataBase : DataBaseAbstract<TElement> where TElement : ViewBase
    {
        protected TDataBase DataBase;
        protected Randomizer Randomizer;

        public virtual TElement Create(int index, Vector3 position)
        {
            if (DataBase.GetElementByIndex(index, out TElement element))
            {
                return Object.Instantiate(element, position, Quaternion.identity);
            }

            return null;
        }

        public virtual TElement Create(int index, Vector3 position, Transform parent)
        {
            if (DataBase.GetElementByIndex(index, out var element))
            {
                Object.Instantiate(element, position, Quaternion.identity, parent);
            }

            return null;
        }

        public TElement CreateRandom(Vector3 position)
        {
            TElement element = DataBase.GetRandomElement(Randomizer);
            return Object.Instantiate(element, position, Quaternion.identity);
        }

        public TElement CreateRandom(Vector3 position, Transform parent)
        {
            TElement element = DataBase.GetRandomElement(Randomizer);
            return Object.Instantiate(element, position, Quaternion.identity, parent);
        }
    }

    public abstract class FactoryAbstract<TDataBase, TData, TElement> : IFactory where TDataBase : DataBaseAbstract<TData> where TElement : ViewBase
    {
        protected TDataBase DataBase;
        protected Randomizer Randomizer;
        protected TData Data;

        public abstract TElement Create(int dataIndex, int elementIndex, Vector3 position);

        public abstract TElement Create(int dataIndex, int elementIndex, Vector3 position, Transform parent);

        public abstract TElement CreateRandom(int dataIndex, Vector3 position);

        public abstract TElement CreateRandom(int dataIndex, Vector3 position, Transform parent);
       
    }
}