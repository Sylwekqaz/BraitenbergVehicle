using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralLogic;
using NeuralLogic.Genetics;

namespace NeuralLogicTest
{
    [TestClass]
    public class FloatRecombinationTest
    {
        [TestMethod]
        public void TestThatResultIsCloseToParent()
        {
            var avg = Enumerable.Range(0, 500)
                .Select(_ => FloatRecombination.Recombine(1, 1))
                .Average();

            Debug.WriteLine($"Difference: {Math.Abs(avg - 1)}");
            Assert.AreEqual(1, avg, 0.06);
        }

        [TestMethod]
        public void TestSigma()
        {
            var sigma = 0.01f;
            var random = FloatRecombination.Recombine(1, 1, sigma);

            Debug.WriteLine($"Difference: {Math.Abs(random-1)} Sigma: {sigma}");
            Assert.AreEqual(1, random, 3*sigma);
        }

        [TestMethod]
        public void TestThatResultIsCloseToParents()
        {
            var randoms = Enumerable.Range(0, 500)
                .Select(_ => FloatRecombination.Recombine(1, 2, 0.01f));

            var countLikeFather = randoms.Count(f => Math.Abs(1-f)<0.3);
            var ratio = countLikeFather / 500f;


            Debug.WriteLine($"Ratio: {ratio}");
            Assert.AreEqual(0.5, ratio, 0.1);
        }
    }
}