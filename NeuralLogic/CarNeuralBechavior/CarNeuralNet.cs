using System;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Single;

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
            return (CarOutputValues) ((Vector<float>) inputValues * Weights);
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
            var matrix = DenseMatrix.Build.Random(6, 2, new Normal(1, 0.5));
            return new CarNeuralNet(matrix);
        }
    }
}