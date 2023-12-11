using System;

namespace NeuralNetworks
{
    public static class LogError
    {
        public static class Weights
        {
            public static void InitType(object initObject)
            {
                Console.Write("Unexpected Weight Initialization Data Type - " + initObject.GetType());
                Console.WriteLine(". Default Weight Initialization (1) used instead.");
            }

            public static void InitLength(int[] architecture, object initObject)
            {
                Console.Write("Weight Initialization Array " + initObject.GetType());
                Console.Write(" is of incorrect Length given the Architecture, ");
                for (int i = 0; i < architecture.Length; i++)
                {
                    Console.Write(architecture[i]);
                }
                Console.WriteLine(". Default Weight Initialization (1) used instead.");
            }

            public static void DepthArgument(object initObject, double depth)
            {
                Console.Write("Weight Depth Argument of Value " + depth);
                Console.Write(" incorrect given the Initialization Data Type - " + initObject.GetType());
                Console.WriteLine(". Default Weight Initialization (1) used instead.");
            }

            public static void Array2D(object initObject)
            {
                Console.Write("The 2D Weight Initialization Array " + initObject.GetType() + " does not contain exactly 3 Columns.");
                Console.WriteLine("Default Weight Initialization (1) used instead.");
            }
        }

        public static class Biases
        {
            public static void InitType(object initObject)
            {
                Console.Write("Unexpected Bias Initialization Data Type - " + initObject.GetType());
                Console.WriteLine(". Default Bias Initialization (1) used instead.");
            }

            public static void InitLength(int[] architecture, object initObject)
            {
                Console.Write("Bias Initialization Array " + initObject.GetType());
                Console.Write(" is of incorrect Length given the Architecture, ");
                for (int i = 0; i < architecture.Length; i++)
                {
                    Console.Write(architecture[i]);
                }
                Console.WriteLine(". Default Bias Initialization (1) used instead.");
            }

            public static void DepthArgument(object initObject, double depth)
            {
                Console.Write("Bias Depth Argument of Value " + depth);
                Console.Write(" incorrect given the Initialization Data Type - " + initObject.GetType());
                Console.WriteLine(". Default Bias Initialization (1) used instead.");
            }
        }

        public static class ActivationFunctions
        {
            public static void InitType(object initObject)
            {
                Console.Write("Unexpected Activation Function Initialization Data Type - " + initObject.GetType());
                Console.WriteLine(". Default Activation Function Initialization (ReLU) used instead.");
            }

            public static void InitLength(int[] architecture, object initObject)
            {
                Console.Write("Activation Function Initialization Array " + initObject.GetType());
                Console.Write(" is of incorrect Length given the Architecture, ");
                for (int i = 0; i < architecture.Length; i++)
                {
                    Console.Write(architecture[i]);
                }
                Console.WriteLine(". Default Activation Function Initialization (ReLU) used instead.");
            }
        }
    }
}