using System;

namespace Core.Tools 
{
    public class Randomizer
    {
        private Random _random;

        public Randomizer() 
        {
            _random = new();
        }

        public int GetRandomValue(int minValueIncluded, int maxValueExcluded) 
        {
            return _random.Next(minValueIncluded, maxValueExcluded);
        }
    }
}