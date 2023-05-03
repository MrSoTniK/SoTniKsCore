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

        public bool FlipCoin()
        {          
            switch (_random.Next(0, 2)) 
            {
                case 0:
                    return true;
                case 1:
                    return false;
            }

            return false;
        }
    }
}