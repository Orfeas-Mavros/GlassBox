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
                Console.Write(" is of incorrect Length given the Architecture");
                for (int i = 0; i < architecture.Length; i++)
                {
                    Console.Write(", " + architecture[i]);
                }
                Console.WriteLine(". Default Activation Function Initialization (ReLU) used instead.");
            }
        }


        public static class RandDepths
        {
            public static void InitLength(int initLength)
            {
                Console.Write("Random Initialization Function Depth '" + initLength + "' not allowed. ");
                Console.Write("The RandDepth Array should be of the Format int[2] { weightRandomDepth, biasRandomDepth }. ");
                Console.WriteLine("RandDepth Property nullified.");
            }

            public static void WeightRandomDepth(int weightDepth)
            {
                Console.Write("Incorrect Weight Random Depth '" + weightDepth + "'. ");
                Console.Write("Acceptable Depth Values for the Weight Random Initialization are integers '0', '1', '2' and '3'. ");
                Console.WriteLine("Weight Depth set to default Value, '3' (Max Depth).");
            }

            public static void WeightRandomFuncDepth(int weightDepth, int funcDepth)
            {
                Console.Write("Incorrect Weight Random Depth '" + weightDepth + "'");

                if (funcDepth == 1)
                {
                    Console.Write(", given that the Weight Random Initialization Functions correspond to each Layer of the Network. ");
                    Console.Write("Acceptable Depth Values are '1' (Layer-Depth), '2' (Node-Depth) or '3' (Weight-Depth). ");
                }
                else if (funcDepth == 2)
                {
                    Console.Write(", given that the Weight Random Initialization Functions correspond to each Node of the Network. ");
                    Console.Write("Acceptable Depth Values are '2' (Node-Depth) or '3' (Weight-Depth). ");
                }
                else if (funcDepth == 3)
                {
                    Console.Write(", given that the Weight Random Initialization Functions correspond to each Weight of the Network. ");
                    Console.Write("Acceptable Depth Value is only '3' (Weight-Depth). ");
                }

                Console.WriteLine("Weight Depth '3' (Max Depth) used instead.");
            }

            public static void BiasRandomDepth(int biasDepth)
            {
                Console.Write("Incorrect Bias Random Depth '" + biasDepth + "'. ");
                Console.Write("Acceptable Depth Values for the Bias Random Initialization are integers '0', '1' and '2'. ");
                Console.WriteLine("Bias Depth set to default Value, '2' (Max Depth).");
            }

            public static void BiasRandomFuncDepth(int biasDepth, int funcDepth)
            {
                Console.Write("Incorrect Bias Random Depth '" + biasDepth + "'");

                if (funcDepth == 1)
                {
                    Console.Write(", given that the Bias Random Initialization Functions correspond to each Layer of the Network. ");
                    Console.Write("Acceptable Depth Values are '1' (Layer-Depth) or '2' (Node-Depth). ");
                }
                else if (funcDepth == 2)
                {
                    Console.Write(", given that the Bias Random Initialization Functions correspond to each Node of the Network. ");
                    Console.Write("Acceptable Depth Value is only '2' (Node-Depth). ");
                }

                Console.WriteLine("Bias Depth '2' (Max Depth) used instead.");
            }
        }
    }
}