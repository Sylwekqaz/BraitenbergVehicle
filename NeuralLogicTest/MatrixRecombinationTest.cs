using System;
using System.Diagnostics;
using System.Linq;
using MathNet.Numerics.LinearAlgebra;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralLogic.Genetics;

namespace NeuralLogicTest
{
    [TestClass]
    public class MatrixRecombinationTest
    {
        [TestMethod]
        public void TestThatRecombinedMatrixValueIsCloseToParent()
        {
            var mat = Matrix<float>.Build.Dense(3, 3, 1);

            var diff = Enumerable.Range(0, 500)
                .Select(_ => MatrixRecombination.Recombine(mat, mat, 0.01f))
                .Select(m => (m - mat).Enumerate().Average())
                .Average();

            Debug.WriteLine($"Difference: {diff}");
            Assert.AreEqual(0, diff, 0.06);
        }

        [TestMethod]
        public void TestThatGenesFromParentAre50PercentOfChildPopulation()
        {
            var mat1 = Matrix<float>.Build.Dense(3, 3, 1);
            var mat2 = Matrix<float>.Build.Dense(3, 3, 2);

            var ratio = Enumerable.Range(0, 500)
                .Select(_ => MatrixRecombination.Recombine(mat1, mat2, 0.01f))
                .Select(m => m.Enumerate().Count(f => Math.Abs(f - 1) < 0.1) / 9f)
                .Average();

            Debug.WriteLine($"Ratio: {ratio}");
            Assert.AreEqual(0.5f, ratio, 0.06);
        }

        [TestMethod]
        public void TestThatAllFieldsAreGoodMixed()
        {
            var mat1 = Matrix<float>.Build.Dense(3, 3, 0);
            mat1[2, 1] = 1;

            var mutated = MatrixRecombination.Recombine(mat1, mat1, 0.01f);

            Assert.AreEqual(0, mutated[0, 0], 0.6);
            Assert.AreEqual(0, mutated[0, 1], 0.6);
            Assert.AreEqual(0, mutated[0, 2], 0.6);

            Assert.AreEqual(0, mutated[1, 0], 0.6);
            Assert.AreEqual(0, mutated[1, 1], 0.6);
            Assert.AreEqual(0, mutated[1, 2], 0.6);

            Assert.AreEqual(0, mutated[2, 0], 0.6);
            Assert.AreEqual(1, mutated[2, 1], 0.6);
            Assert.AreEqual(0, mutated[2, 2], 0.6);

            Debug.WriteLine($"Mat: {mutated}");
        }
    }
}