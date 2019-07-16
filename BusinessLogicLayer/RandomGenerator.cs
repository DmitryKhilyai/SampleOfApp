using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer
{
    public class RandomGenerator : IRandomGenerator
    {
        private readonly int _numbersCount;
        private readonly int _length;
        private readonly Random _random;

        public RandomGenerator(int length, int numberCount)
        {
            if (length < numberCount)
            {
                numberCount = length;
            }

            _numbersCount = numberCount;
            _length = length;
            _random = new Random();
        }

        public string GenerateRandomSymbols()
        {
            StringBuilder sb = new StringBuilder(_length);

            var indexes = RandomUniqueList(0, _length, _numbersCount);
            int index;
            char? ch;

            for (int i = 0, j = 0; i < _length; i++)
            {
                ch = null;
                if (j < _numbersCount)
                {
                    index = indexes[j];
                    if (index == i)
                    {
                        ch = GetRandomNumberChar();
                        j++;
                    }
                }
                if (ch == null)
                {
                    ch = GetRandomCapitalLetter();
                }

                sb.Append(ch);
            }

            return sb.ToString();
        }

        private char GetRandomNumberChar()
        {
            return Convert.ToChar(_random.Next(48, 58));
        }

        private char GetRandomCapitalLetter()
        {
            return Convert.ToChar(Convert.ToInt32(Math.Floor(26 * _random.NextDouble() + 65)));
        }

        private List<int> RandomUniqueList(int minValue, int maxValue, int capacity)
        {
            if (minValue > maxValue)
            {
                throw new ArgumentOutOfRangeException("minValue is greater than maxValue.");
            }
            if (maxValue - minValue < capacity)
            {
                capacity = maxValue - minValue;
            }

            List<int> randomList = new List<int>(capacity);
            for (int i = 0; i < capacity;)
            {
                int randomNumber = _random.Next(minValue, maxValue);
                if (!randomList.Contains(randomNumber))
                {
                    randomList.Add(randomNumber);
                    i++;
                }
            }
            randomList.Sort();
            return randomList;
        }
    }
}
