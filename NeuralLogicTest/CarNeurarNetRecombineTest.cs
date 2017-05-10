using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralLogic.CarNeuralBechavior;

namespace NeuralLogicTest
{
    [TestClass]
    public class CarNeuralNetRecombineTest
    {
        [TestMethod]
        public void TestMutatedDefaultNet()
        {
            var defaultNet = CarNeuralNet.GetDefaultNet();
            var mutatedNet = CarNeuralNet.GetDefaultMutatedNet(0.01f);

            var diff = (mutatedNet.Weights-defaultNet.Weights);

            Debug.WriteLine($"Excepted: {defaultNet.Weights}");
            Debug.WriteLine($"Actual: {mutatedNet.Weights}");
            Debug.WriteLine($"Difference: {diff}");

            Assert.IsTrue(diff.ForAll(f => Math.Abs(f) < 0.03));
        }
    }
}