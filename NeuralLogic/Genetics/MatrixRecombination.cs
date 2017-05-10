using System;
using MathNet.Numerics.LinearAlgebra;

namespace NeuralLogic.Genetics
{
    public static class MatrixRecombination
    {
        public static Matrix<float> Recombine(Matrix<float> father, Matrix<float> mother, float sigma = 0.5f)
        {
            if (father.ColumnCount != mother.ColumnCount || father.RowCount != mother.RowCount)
            {
                throw new ArgumentException("Wymiary macierzy nie są zgodne", nameof(mother));
            }

            var child = Matrix<float>.Build.Dense(father.RowCount, father.ColumnCount);

            for (var i = 0; i < father.ColumnCount; i++)
            {
                for (var j = 0; j < father.RowCount; j++)
                {
                    child[j, i] = FloatRecombination.Recombine(father[j, i], mother[j, i], sigma);
                }
            }
            return child;
        }
    }
}