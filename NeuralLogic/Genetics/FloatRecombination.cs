using Troschuetz.Random;

namespace NeuralLogic.Genetics
{
    public static class FloatRecombination
    {
        private static readonly TRandom Random = new TRandom();

        public static float Recombine(float a, float b, float sigma = 0.5f, float mutationRate = 0.1f)
        {
            var parent = Random.NextBoolean() ? a : b; // randomly select parent

            return Mutation(parent, sigma, mutationRate);
        }

        public static float Mutation(float f, float sigma, float mutationRate)
        {
            return Random.NextDouble() < mutationRate
                ? (float) Random.Normal(f, sigma)
                : f;
        }
    }
}