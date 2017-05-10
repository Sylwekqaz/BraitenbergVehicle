using MathNet.Numerics.LinearAlgebra;

namespace NeuralLogic.CarNeuralBechavior
{
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