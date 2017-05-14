using System;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Single;
using NeuralLogic.Genetics;

namespace NeuralLogic.CarNeuralBechavior
{
    public class CarNeuralNet
    {
        public Matrix<float> Weights { get; }

        public CarNeuralNet(Matrix<float> weights)
        {
            if (weights.ColumnCount != 2 || weights.RowCount != 6)
            {
                throw new ArgumentException("macierz musi być wymiarów 5x2", nameof(weights));
            }
            Weights = weights;
        }

        public CarOutputValues RunNeuralNet(CarInputValues inputValues)
        {
            var output = (Vector<float>) inputValues * Weights;
            var absoluteMaximum = output.AbsoluteMaximum();
            if (absoluteMaximum>1)
            {
                output /= absoluteMaximum;
            }

            return (CarOutputValues) output;
        }

        public static CarNeuralNet GetDefaultNet()
        {
            var matrix = DenseMatrix.OfArray(new[,]
            {
                {0, 0, 1, -0.5f, 0, 0},
                {1, -0.5f, 0, 0, 0, 0,}
            });
            return new CarNeuralNet(matrix.Transpose());
        }

        public static CarNeuralNet GetRandomNet()
        {
            var random = new Random();
            var matrix = DenseMatrix.Build.Dense(6, 2, (_, __) => (float) (random.NextDouble() * 2) - 1);
            return new CarNeuralNet(matrix);
        }

        public static CarNeuralNet GetDefaultMutatedNet(float sigma = 0.03f)
        {
            return CarNeuralNetRecombine.Recombine(GetDefaultNet(), GetDefaultNet(),sigma);
        }
    }
}