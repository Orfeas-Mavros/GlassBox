using System.Collections.Generic;

namespace NeuralNetworks
{
    public static class Format
    {
        public static double DefaultInitWeights = 1;
        public static double DefaultInitBiases = 1;
        public static Activation DefaultInitActivationFunctions = DefaultActivation.ReLU;


        
        // ~~~ // ~~~ // ~~~ // ~~~ // ~~~ // ~~~ //



        public static T[] Flatten2D<T>(T[][] inArray)
        {
            List<T> outList = new();

            for (int i = 0; i < inArray.Length; i++)
            {
                for (int j = 0; j < inArray[i].Length; j++)
                {
                    outList.Add(inArray[i][j]);
                }
            }

            return outList.ToArray();
        } // Checked
        public static T[] Flatten3D<T>(T[][][] inArray)
        {
            List<T> outList = new();

            for (int i = 0; i < inArray.Length; i++)
            {
                for (int j = 0; j < inArray[i].Length; j++)
                {
                    for (int k = 0; k < inArray[i][j].Length; k++)
                    {
                        outList.Add(inArray[i][j][k]);
                    }
                }
            }

            return outList.ToArray();
        } // Checked
    }
}