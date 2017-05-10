using System;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Single;

namespace NeuralLogic
{
    public class CarNeuralNet
    {
        private readonly Matrix<float> _weights;

        public CarNeuralNet() : this(DenseMatrix.OfArray(new[,]
            {{0, 1}, {0f, -0.5f}, {1, 0}, {-0.5f, 0f}, {0, 0}, {0, 0}}))
        {
        }

        public CarNeuralNet(Matrix<float> weights)
        {
            if (weights.ColumnCount != 2 || weights.RowCount != 6)
            {
                throw new ArgumentException("macierz musi być wymiarów 5x2", nameof(weights));
            }
            _weights = weights;
        }

        public CarOutputValues RunNeuralNet(CarInputValues inputValues)
        {
            return (CarOutputValues) ((Vector<float>) inputValues * _weights);
        }
    }

    public class CarInputValues
    {
        public float LeftAntenaGoodSignal { get; set; }
        public float LeftAntenaBadSignal { get; set; }
        public float RightAntenaGoodSignal { get; set; }
        public float RightAntenaBadSignal { get; set; }
        public float BatteryLevel { get; set; }

        public static explicit operator Vector<float>(CarInputValues values)
        {
            return Vector<float>.Build.DenseOfArray(new[]
            {
                values.LeftAntenaGoodSignal,
                values.LeftAntenaBadSignal,
                values.RightAntenaGoodSignal,
                values.RightAntenaBadSignal,
                values.BatteryLevel,
                1,
            });
        }
    }

    public class CarOutputValues
    {
        public float LeftWheelMultiplier { get; set; }
        public float RightWheelMultiplier { get; set; }

        public static explicit operator CarOutputValues(Vector<float> mat)
        {
            return new CarOutputValues()
            {
                LeftWheelMultiplier = mat.At(0),
                RightWheelMultiplier = mat.At(1)
            };
        }
    }
}