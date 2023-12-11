using System;

namespace NeuralNetworks
{
    namespace RandFuncs
    {
        public class UniformDistribution
        {
            private readonly double lowerBound;
            private readonly double upperBound;
            private readonly Random randomFunc;

            public UniformDistribution(double lowerBound = 0, double upperBound = 1)
            {
                this.lowerBound = lowerBound;
                this.upperBound = upperBound;

                randomFunc = new Random();
            }


            public double GenerateRandom()
            {
                return randomFunc.NextDouble() * (upperBound - lowerBound) + lowerBound;
            }
        }


        // ~~~ // ~~~ // ~~~ // ~~~ // ~~~ // ~~~ //


        public class NormalDistribution
        {
            private readonly double lowerBound;
            private readonly double upperBound;
            private readonly Random randomFunc;

            public NormalDistribution(double lowerBound = 0, double upperBound = 1)
            {
                this.lowerBound = lowerBound;
                this.upperBound = upperBound;

                randomFunc = new Random();
            }


            public double GenerateRandom()
            {
                double uniformX = randomFunc.NextDouble();
                double uniformY = randomFunc.NextDouble();

                double normalDist = Math.Sqrt(-2 * Math.Log(uniformX)) * Math.Cos(2 * Math.PI * uniformY);

                return normalDist * (upperBound - lowerBound) + lowerBound;
            }
        }
    }



    // ~~~ // ~~~ // ~~~ // ~~~ // ~~~ // ~~~ //



    namespace InitFuncs
    {
        public static class Xavier
        {
            public static double[] InitializeUniform(int[] architecture)
            {
                double[] output = new double[architecture.Length];
                for (int i = 0; i < architecture.Length; i++)
                {
                    output[i] = (double)architecture[i];
                }
                return output;
            }

            public static double[] InitializeNormal(int[] architecture)
            {
                double[] output = new double[architecture.Length];
                for (int i = 0; i < architecture.Length; i++)
                {
                    output[i] = (double)architecture[i];
                }
                return output;
            }
        }

        public static class KaimingHe
        {
            public static double[] InitializeUniform(int[] architecture)
            {
                double[] output = new double[architecture.Length];
                for (int i = 0; i < architecture.Length; i++)
                {
                    output[i] = (double)architecture[i];
                }
                return output;
            }

            public static double[] InitializeNormal(int[] architecture)
            {
                double[] output = new double[architecture.Length];
                for (int i = 0; i < architecture.Length; i++)
                {
                    output[i] = (double)architecture[i];
                }
                return output;
            }
        }
    }
}