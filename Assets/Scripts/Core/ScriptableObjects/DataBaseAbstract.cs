using Core.Data;
using Core.Tools;
using Core.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.ScriptableObjects 
{
    public interface IDataBase 
    {
        
    }

    public abstract class DataBaseAbstract<TElement> : ScriptableObject, IDataBase
    {
        [SerializeField] private TElement[] _elements;

        public TElement[] Elements => _elements;

        public bool GetElementByIndex(int index, out TElement element)
        {
            element = default;

            if (_elements.Length > index && index >= 0) 
            {
                element = _elements[index];
                return true;
            }

            return false;
        }

        public TElement GetRandomElement(Randomizer randomizer) 
        {
            int randomIndex = randomizer.GetRandomValue(0, _elements.Length);
            return _elements[randomIndex];
        }
    }

    public abstract class DataBaseAbstract<TElement, TType> : ScriptableObject, IDataBase where TType : Enum where TElement : DataAbstractSO<TType>
    {
        [SerializeField] private TElement[] _elements;

        public TElement[] Elements => _elements;

        public Dictionary<TType, TElement> ElementsDictionary { get { return _elements.ToDictionary(key => key.TypeProp); } }

        public bool GetElementByIndex(int index, out TElement element)
        {
            element = default;

            if (_elements.Length > index && index >= 0)
            {
                element = _elements[index];
                return true;
            }

            return false;
        }

        public TElement GetRandomElement(Randomizer randomizer)
        {
            int randomIndex = randomizer.GetRandomValue(0, _elements.Length);
            return _elements[randomIndex];
        }
    }

    public abstract class DataBaseViewsAbstract<TElement, TType> : ScriptableObject, IDataBase where TType : Enum where TElement : ElementViewBase<TType>
    {
        [SerializeField] private TElement[] _elements;

        public TElement[] Elements => _elements;

        public Dictionary<TType, TElement> ElementsDictionary { get { return _elements.ToDictionary(key => key.TypeProp); } }

        public bool GetElementByIndex(int index, out TElement element)
        {
            element = default;

            if (_elements.Length > index && index >= 0)
            {
                element = _elements[index];
                return true;
            }

            return false;
        }

        public TElement GetRandomElement(Randomizer randomizer)
        {
            int randomIndex = randomizer.GetRandomValue(0, _elements.Length);
            return _elements[randomIndex];
        }
    }
}