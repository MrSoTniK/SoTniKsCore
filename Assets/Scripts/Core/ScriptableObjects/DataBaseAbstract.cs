using Core.Tools;
using UnityEngine;

namespace Core.ScriptableObjects 
{
    public abstract class DataBaseAbstract<TElement> : ScriptableObject
    {
        [SerializeField] private TElement[] _elements;

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