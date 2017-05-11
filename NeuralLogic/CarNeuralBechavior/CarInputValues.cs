using MathNet.Numerics.LinearAlgebra;

namespace NeuralLogic.CarNeuralBechavior
{
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
//                values.BatteryLevel,
//                1, TODO
                0, 0
            });
        }
    }
}