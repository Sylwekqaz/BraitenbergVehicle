using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Single;
using NeuralLogic.CarNeuralBechavior;
using Troschuetz.Random;

namespace NeuralLogic.Genetics
{
    public static class CarNeuralNetRecombine
    {
        private static readonly TRandom Random = new TRandom();

        public static CarNeuralNet Recombine(CarNeuralNet father,CarNeuralNet mother,float sigma = 0.01f)
        {
            Matrix<float> child = DenseMatrix.Build.Dense(6, 2);

            var pairs = new[,,]
            {
                {{0, 0}, {2, 1}},
                {{1, 0}, {3, 1}},
                {{0, 1}, {2, 0}},
                {{1, 1}, {3, 0}},
                {{4, 0}, {4, 1}},
                {{5, 0}, {5, 1}},
            };

            for (int p = 0; p < pairs.GetLength(0); p++)
            {
                var net = Random.NextBoolean() ? mother.Weights : father.Weights;
                for (int m = 0; m < pairs.GetLength(1); m++)
                {
                    child[pairs[p,m,0], pairs[p,m,1]] = FloatRecombination.Mutation(net[pairs[p, m, 0], pairs[p, m, 1]],sigma);
                }
            }

            return new CarNeuralNet(child);
        }
    }
}