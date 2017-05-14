using System;
using System.Collections.Generic;
using System.Linq;

namespace NeuralLogic.Infrastructure
{
    public static class LinqExtension
    {
        private static readonly Random Random = new Random();

        public static T GetRandomItem<T>(this IEnumerable<T> pool, Func<T, double> probabilityFunc)
        {
            // get universal probability 
            double u = pool.Sum(probabilityFunc);

            // pick a random number between 0 and u
            double r = Random.NextDouble() * u;

            double sum = 0;
            foreach (var n in pool)
            {
                // loop until the random number is less than our cumulative probability
                if (r <= (sum = sum + probabilityFunc(n)))
                {
                    return n;
                }
            }

            // should never get here
            //      but sometimes do (nan od negative values)
            //          so we get random item without probability               
            return pool.GetRandomItem(_ => 1);
        }
    }
}