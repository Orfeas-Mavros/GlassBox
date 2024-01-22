using System;

namespace NeuralNetworks
{
    namespace RandFuncs
    {
        public static class GenerateRandom
        {
            public static Random randomFunc;

            static GenerateRandom()
            {
                randomFunc = new Random();
            }



            // ~~~ // ~~~ // ~~~ // ~~~ // ~~~ // ~~~ //



            public static double Uniform(double lowerBound, double upperBound)
            {
                return randomFunc.NextDouble() * (upperBound - lowerBound) + lowerBound;
            }


            public static double Normal(double mean, double standardDeviation)
            {
                double uniform1 = randomFunc.NextDouble();
                double uniform2 = randomFunc.NextDouble();

                double normalRand = Math.Sqrt(-2D * Math.Log(uniform1)) * Math.Cos(2D * Math.PI * uniform2);

                return mean + standardDeviation * normalRand;
            }


            public static double NormalBounded(double lowerBound, double upperBound)
            {
                return Normal((lowerBound + upperBound) / 2, (upperBound - lowerBound) / 6);
            }
        }



        // ~~~ // ~~~ // ~~~ // ~~~ // ~~~ // ~~~ //



        public class UniformDistribution
        {
            public double lowerBound;
            public double upperBound;

            public UniformDistribution(double lowerBound = 0, double upperBound = 1)
            {
                this.lowerBound = lowerBound;
                this.upperBound = upperBound;
            }


            public double Generate()
            {
                return GenerateRandom.Uniform(lowerBound, upperBound);
            }
        }


        public class NormalDistribution
        {
            public double mean;
            public double standardDeviation;

            public NormalDistribution(double mean = 0, double standardDeviation = 1)
            {
                this.mean = mean;
                this.standardDeviation = standardDeviation;
            }


            public double Generate()
            {
                return GenerateRandom.Normal(mean, standardDeviation);
            }
        }


        public class NormalBoundedDistribution
        {
            public double lowerBound;
            public double upperBound;

            public NormalBoundedDistribution(double lowerBound = 0, double upperBound = 1)
            {
                this.lowerBound = lowerBound;
                this.upperBound = upperBound;
            }


            public double Generate()
            {
                return GenerateRandom.NormalBounded(lowerBound, upperBound);
            }
        }



        // ~~~ // ~~~ // ~~~ // ~~~ // ~~~ // ~~~ //



        namespace InitFuncs
        {
            public static class Xavier
            {
                public static Func<double>[] Uniform(int[] architecture)
                {
                    Func<double>[] randFuncs = new Func<double>[architecture.Length - 1];

                    for (int i = 1; i < architecture.Length; i++)
                    {
                        double bound = Math.Sqrt(6D / (architecture[i - 1] * architecture[i]));

                        randFuncs[i] = new UniformDistribution(-bound, bound).Generate;
                    }

                    return randFuncs;
                }


                public static Func<double>[] Normal(int[] architecture)
                {
                    Func<double>[] randFuncs = new Func<double>[architecture.Length - 1];

                    for (int i = 1; i < architecture.Length; i++)
                    {
                        double standardDeviation = Math.Sqrt(2D / (architecture[i - 1] * architecture[i]));

                        randFuncs[i] = new NormalDistribution(0, standardDeviation).Generate;
                    }

                    return randFuncs;
                }
            }


            
            // ~~~ // ~~~ // ~~~ // ~~~ // ~~~ // ~~~ //



            public static class KaimingHe
            {
                public static Func<double>[] Uniform(int[] architecture)
                {
                    Func<double>[] randFuncs = new Func<double>[architecture.Length - 1];

                    for (int i = 1; i < architecture.Length; i++)
                    {
                        double bound = Math.Sqrt(6D / architecture[i - 1]);

                        randFuncs[i] = new UniformDistribution(-bound, bound).Generate;
                    }

                    return randFuncs;

                }


                public static Func<double>[] Normal(int[] architecture)
                {
                    Func<double>[] randFuncs = new Func<double>[architecture.Length - 1];

                    for (int i = 1; i < architecture.Length; i++)
                    {
                        double standardDeviation = Math.Sqrt(2D / architecture[i - 1]);

                        randFuncs[i] = new NormalDistribution(0, standardDeviation).Generate;
                    }

                    return randFuncs;
                }
            }
        }
    }
}