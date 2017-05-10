using NeuralLogic.CarNeuralBechavior;

namespace NeuralLogic.Genetics
{
    public static class CarNeuralNetRecombine
    {
        public static CarNeuralNet Recombine(CarNeuralNet father,CarNeuralNet mother,float sigma = 0.1f)
        {
            var weights = MatrixRecombination.Recombine(father.Weights,mother.Weights,sigma);
            return new CarNeuralNet(weights);
        }
    }
}