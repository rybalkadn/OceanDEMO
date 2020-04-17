using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocean_DEMO
{
    /// <summary>
    /// Генератор
    /// </summary>
    class RandomGenerator
    {
        private static Random rnd = new Random();

        /// <summary>
        /// Вернуть уникальное случайное число
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static int[] GenerateIntUniqueSeries(int minValue = 0, int maxValue = int.MaxValue, int count = 1)
        {
            if (count < 1)
            {
                count = 1;
            }

            int[] ar = new int[count];

            int i = count - 1;

            SortedSet<int> ss = new SortedSet<int>();

            do
            {
                int num = rnd.Next(minValue, maxValue);

                if (ss.Add(num))
                {
                    ar[i--] = num;
                }
            } while (i >= 0);

            return ar;
        }
      
        public static int GetIntRandom(int minValue = 0, int maxValue = int.MaxValue)
        {
            return rnd.Next(minValue, maxValue);
        }
    }
}
