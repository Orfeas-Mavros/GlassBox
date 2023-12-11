using System;
using UnityEngine;
using NeuralNetworks;
using NetworkImportExport;

public class TestCreator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Func<double> testFunc1 = new NeuralNetworks.RandFuncs.UniformDistribution(5, 6).GenerateRandom;
        Func<double> testFunc2 = new NeuralNetworks.RandFuncs.UniformDistribution(7, 8).GenerateRandom;
        Func<double> testFunc3 = new NeuralNetworks.RandFuncs.UniformDistribution(9, 10).GenerateRandom;

        double weight = 1D;
        double[] weightArray1 = new double[5]
        {
            1D, 3D, 1D, 1D, 1D
        };
        int[] weightArray2 = new int[18]
        {
            1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2,
        };
        float[] weightArray3 = new float[66]
        {
            1F, 2F, 3F, 4F, 3F, 2F, 1F, 5F, 6F, 7F, 8F,
            1F, 2F, 3F, 4F, 3F, 2F, 1F, 5F, 6F, 7F, 8F,
            1F, 2F, 3F, 4F, 3F, 2F, 1F, 5F, 6F, 7F, 8F,
            1F, 2F, 3F, 4F, 3F, 2F, 1F, 5F, 6F, 7F, 8F,
            1F, 2F, 3F, 4F, 3F, 2F, 1F, 5F, 6F, 7F, 8F,
            1F, 2F, 3F, 4F, 3F, 2F, 1F, 5F, 6F, 7F, 8F
        };
        long[][] weight2dArray1 = new long[4][]
        {
            new long[3] {10, 20, 30},
            new long[5] {40, 50, 60, 70, 80},
            new long[4] {90, 100, 110, 120},
            new long[6] {130, 140, 150, 160, 170, 180}
        };
        long[][] weight2dArray2 = new long[8][]
        {
            new long[9] {1, 2, 3, 4, 5, 6, 7, 8, 9},
            new long[8] {1, 2, 4, 8, 16, 32, 64, 128},
            new long[10] {1, 3, 5, 7, 9, 11, 13, 15, 17, 19},
            new long[9] {2, 6, 4, 8, 10, 14, 12, 16, 18},
            new long[8] {2, 3, 5, 7, 11, 13, 17, 19},
            new long[10] {1, 1, 2, 3, 5, 8, 13, 21, 34, 55},
            new long[9] {100000, 500000, 250000, 125000, 625000, 312500, 156250, 781250, 5},
            new long[3] {1, 12, 144},
        };
        int[][][] weight3dArray = new int[2][][]
        {
            new int[3][]
            {
                new int[8] { 1, 2, 3, 4, 5, 6, 7, 8 },
                new int[10] { 18, 17, 16, 15, 14, 13, 12, 11, 10, 9 },
                new int[12] { 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 }
            },
            new int[4][]
            {
                new int[6] { 66, 65, 64, 63, 62, 61 },
                new int[8] { 53, 54, 55, 56, 57, 58, 59, 60 },
                new int[10] { 52, 51, 50, 49, 48, 47, 46, 45, 44, 43 },
                new int[12] { 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42 },
            }
        };
        Func<double> weightFunc = testFunc1;
        Func<double>[] weightFuncArr1 = new Func<double>[5]
        {
            testFunc1, testFunc2, testFunc3, testFunc2, testFunc1
        };
        Func<double>[] weightFuncArr2 = new Func<double>[18]
        {
            testFunc1, testFunc2, testFunc3, testFunc1, testFunc2, testFunc3,
            testFunc1, testFunc2, testFunc3, testFunc1, testFunc2, testFunc3,
            testFunc1, testFunc2, testFunc3, testFunc1, testFunc2, testFunc3
        };
        Func<double>[] weightFuncArr3 = new Func<double>[66]
        {
            testFunc1, testFunc2, testFunc3, testFunc1, testFunc2, testFunc3, testFunc1, testFunc2, testFunc3,
            testFunc1, testFunc2, testFunc3, testFunc1, testFunc2, testFunc3, testFunc1, testFunc2, testFunc3,
            testFunc1, testFunc2, testFunc3, testFunc1, testFunc2, testFunc3, testFunc1, testFunc2, testFunc3,
            testFunc1, testFunc2, testFunc3, testFunc1, testFunc2, testFunc3, testFunc1, testFunc2, testFunc3,
            testFunc1, testFunc2, testFunc3, testFunc1, testFunc2, testFunc3, testFunc1, testFunc2, testFunc3,
            testFunc1, testFunc2, testFunc3, testFunc1, testFunc2, testFunc3, testFunc1, testFunc2, testFunc3,
            testFunc1, testFunc2, testFunc3, testFunc1, testFunc2, testFunc3, testFunc1, testFunc2, testFunc3,
            testFunc1, testFunc2, testFunc3
        };
        Func<double>[][] weightFunc2dArr1 = new Func<double>[4][]
        {
            new Func<double>[3] {testFunc1, testFunc2, testFunc3},
            new Func<double>[5] {testFunc1, testFunc2, testFunc3, testFunc1, testFunc2},
            new Func<double>[4] {testFunc1, testFunc2, testFunc3, testFunc1},
            new Func<double>[6] {testFunc1, testFunc2, testFunc3, testFunc1, testFunc2, testFunc3}
        };
        Func<double>[][] weightFunc2dArr2 = new Func<double>[8][]
        {
            new Func<double>[9] {testFunc1, testFunc2, testFunc3, testFunc1, testFunc2, testFunc3, testFunc1, testFunc2, testFunc3},
            new Func<double>[8] {testFunc1, testFunc2, testFunc3, testFunc1, testFunc2, testFunc3, testFunc1, testFunc2},
            new Func<double>[10] {testFunc1, testFunc2, testFunc3, testFunc1, testFunc2, testFunc3, testFunc1, testFunc2, testFunc3, testFunc1},
            new Func<double>[9] {testFunc1, testFunc2, testFunc3, testFunc1, testFunc2, testFunc3, testFunc1, testFunc2, testFunc3},
            new Func<double>[8] {testFunc1, testFunc2, testFunc3, testFunc1, testFunc2, testFunc3, testFunc1, testFunc2},
            new Func<double>[10] {testFunc1, testFunc2, testFunc3, testFunc1, testFunc2, testFunc3, testFunc1, testFunc2, testFunc3, testFunc1},
            new Func<double>[9] {testFunc1, testFunc2, testFunc3, testFunc1, testFunc2, testFunc3, testFunc1, testFunc2, testFunc3},
            new Func<double>[3] {testFunc1, testFunc2, testFunc3}
        };
        Func<double>[][][] weightFunc3dArr = new Func<double>[2][][]
        {
            new Func<double>[5][]
            {
                new Func<double>[1] {testFunc1},
                new Func<double>[2] {testFunc1, testFunc2},
                new Func<double>[3] {testFunc1, testFunc2, testFunc3},
                new Func<double>[4] {testFunc1, testFunc2, testFunc3, testFunc1},
                new Func<double>[5] {testFunc1, testFunc2, testFunc3, testFunc2, testFunc1}
            },
            new Func<double>[6][]
            {
                new Func<double>[6] {testFunc1, testFunc2, testFunc3, testFunc2, testFunc1, testFunc1},
                new Func<double>[7] {testFunc1, testFunc2, testFunc3, testFunc2, testFunc1, testFunc1, testFunc2},
                new Func<double>[8] {testFunc1, testFunc2, testFunc3, testFunc2, testFunc1, testFunc1, testFunc2, testFunc3},
                new Func<double>[9] {testFunc1, testFunc2, testFunc3, testFunc2, testFunc1, testFunc1, testFunc2, testFunc3, testFunc1},
                new Func<double>[10] {testFunc1, testFunc2, testFunc3, testFunc2, testFunc1, testFunc1, testFunc2, testFunc3, testFunc2, testFunc1},
                new Func<double>[11] {testFunc1, testFunc2, testFunc3, testFunc1, testFunc2, testFunc3 , testFunc1, testFunc2, testFunc3 , testFunc1, testFunc2}
            }
        };
        long[,] weights2D = new long[12, 3]
        {
            {0, 2, 10 },
            {0, 3, 20 },
            {0, 4, 30 },
            {1, 2, 40 },
            {1, 3, 50 },
            {1, 4, 60 },
            {2, 5, 70 },
            {2, 6, 80 },
            {2, 7, 90 },
            {2, 8, 100 },
            {3, 5, 110 },
            {3, 6, 120 },
        };

        string weightFalseType = "hi";
        double[] weightFalseLength1D = new double[9] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        double[][] weightFalselength2D = new double[3][]
        {
            new double[2] {10, 20},
            new double[3] {40, 50, 60},
            new double[4] {80, 90, 100, 110}
        };
        double[][][] weightFalseLength3D = new double[4][][]
        {
            new double[5][]
            {
                new double[2] {10, 20},
                new double[2] {10, 10},
                new double[3] {30, 50, 80},
                new double[5] {110, 130, 170, 190, 230},
                new double[8] {240, 220, 200, 180, 160, 140, 120, 100}
            },
            new double[6][]
            {
                new double[4] {111, 222, 333, 444},
                new double[4] {9999, 8888, 7777, 5555},
                new double[5] {1234, 2345, 3456, 4567, 5678},
                new double[5] {8765, 7654, 6543, 5432, 4321},
                new double[6] {123, 234, 345, 456, 567, 678},
                new double[6] {876, 765, 654, 534, 432, 312},
            },
            new double[3][]
            {
                new double[3] {1337, 7341, 83110},
                new double[2] {720, 60},
                new double[3] {12, 63, 75}
            },
            new double[2][]
            {
                new double[4] {5, 10, 20, 25},
                new double[5] {99, 89, 79, 69, 59}
            }
        };
        double[,] weightFalse2dLengthX = new double[66, 2]
        {
            { 1, 5 }, { 1, 5 }, { 1, 5 }, { 1, 5 }, { 1, 5 }, { 1, 5 },
            { 1, 5 }, { 1, 5 }, { 1, 5 }, { 1, 5 }, { 1, 5 }, { 1, 5 },
            { 1, 5 }, { 1, 5 }, { 1, 5 }, { 1, 5 }, { 1, 5 }, { 1, 5 },
            { 1, 5 }, { 1, 5 }, { 1, 5 }, { 1, 5 }, { 1, 5 }, { 1, 5 },
            { 1, 5 }, { 1, 5 }, { 1, 5 }, { 1, 5 }, { 1, 5 }, { 1, 5 },
            { 1, 5 }, { 1, 5 }, { 1, 5 }, { 1, 5 }, { 1, 5 }, { 1, 5 },
            { 1, 5 }, { 1, 5 }, { 1, 5 }, { 1, 5 }, { 1, 5 }, { 1, 5 },
            { 1, 5 }, { 1, 5 }, { 1, 5 }, { 1, 5 }, { 1, 5 }, { 1, 5 },
            { 1, 5 }, { 1, 5 }, { 1, 5 }, { 1, 5 }, { 1, 5 }, { 1, 5 },
            { 1, 5 }, { 1, 5 }, { 1, 5 }, { 1, 5 }, { 1, 5 }, { 1, 5 },
            { 1, 5 }, { 1, 5 }, { 1, 5 }, { 1, 5 }, { 1, 5 }, { 1, 5 }
        };
        double[,] weightFalse2dLengthY = new double[67, 3]
        {
            {1, 3, 5 }, {1, 3, 5 }, {1, 3, 5 }, {1, 3, 5 }, {1, 3, 5 }, {1, 3, 5 },
            {1, 3, 5 }, {1, 3, 5 }, {1, 3, 5 }, {1, 3, 5 }, {1, 3, 5 }, {1, 3, 5 },
            {1, 3, 5 }, {1, 3, 5 }, {1, 3, 5 }, {1, 3, 5 }, {1, 3, 5 }, {1, 3, 5 },
            {1, 3, 5 }, {1, 3, 5 }, {1, 3, 5 }, {1, 3, 5 }, {1, 3, 5 }, {1, 3, 5 },
            {1, 3, 5 }, {1, 3, 5 }, {1, 3, 5 }, {1, 3, 5 }, {1, 3, 5 }, {1, 3, 5 },
            {1, 3, 5 }, {1, 3, 5 }, {1, 3, 5 }, {1, 3, 5 }, {1, 3, 5 }, {1, 3, 5 },
            {1, 3, 5 }, {1, 3, 5 }, {1, 3, 5 }, {1, 3, 5 }, {1, 3, 5 }, {1, 3, 5 },
            {1, 3, 5 }, {1, 3, 5 }, {1, 3, 5 }, {1, 3, 5 }, {1, 3, 5 }, {1, 3, 5 },
            {1, 3, 5 }, {1, 3, 5 }, {1, 3, 5 }, {1, 3, 5 }, {1, 3, 5 }, {1, 3, 5 },
            {1, 3, 5 }, {1, 3, 5 }, {1, 3, 5 }, {1, 3, 5 }, {1, 3, 5 }, {1, 3, 5 },
            {1, 3, 5 }, {1, 3, 5 }, {1, 3, 5 }, {1, 3, 5 }, {1, 3, 5 }, {1, 3, 5 },
            {1, 3, 5 }
        };




        double bias = 5D;
        double[] biasArray1 = new double[5]
        {
            1D, 3D, 1D, 1D, 1D
        };
        int[] biasArray2 = new int[18]
        {
            1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2,
        };
        Func<double> biasFunc = testFunc2;
        Func<double>[] biasFuncArr1 = new Func<double>[5]
        {
            testFunc1, testFunc2, testFunc3, testFunc2, testFunc1
        };
        Func<double>[] biasFuncArr2 = new Func<double>[18]
        {
            testFunc1, testFunc2, testFunc3, testFunc1, testFunc2, testFunc3,
            testFunc1, testFunc2, testFunc3, testFunc1, testFunc2, testFunc3,
            testFunc1, testFunc2, testFunc3, testFunc1, testFunc2, testFunc3
        };
        Func<double>[][] biasFunc2dArr = new Func<double>[3][]
        {
            new Func<double>[8] {testFunc1, testFunc2, testFunc3, testFunc2, testFunc1, testFunc2, testFunc3, testFunc2},
            new Func<double>[8] {testFunc1, testFunc2, testFunc3, testFunc2, testFunc1, testFunc2, testFunc3, testFunc2},
            new Func<double>[8] {testFunc1, testFunc2, testFunc3, testFunc2, testFunc1, testFunc2, testFunc3, testFunc2}
        };




        Activation activation = DefaultActivation.ReLU;
        Activation[] activationArray1 = new Activation[5]
        {
            DefaultActivation.ReLU, DefaultActivation.ReLU, DefaultActivation.TanH, DefaultActivation.ReLU, DefaultActivation.ReLU
        };
        Activation[] activationArray2 = new Activation[18]
        {
            DefaultActivation.ReLU, DefaultActivation.Sigmoid, DefaultActivation.ReLU, DefaultActivation.ReLU, DefaultActivation.ReLU, DefaultActivation.ReLU,
            DefaultActivation.ReLU, DefaultActivation.TanH, DefaultActivation.TanH, DefaultActivation.TanH, DefaultActivation.TanH, DefaultActivation.TanH,
            DefaultActivation.ReLU, DefaultActivation.LeakyReLU, DefaultActivation.ReLU, DefaultActivation.LeakyReLU, DefaultActivation.BinaryStep, DefaultActivation.ReLU
        };
        Activation[][] activation2DArray = new Activation[2][]
        {
            new Activation[10]
            {
                DefaultActivation.ReLU, DefaultActivation.Sigmoid, DefaultActivation.ReLU, DefaultActivation.ReLU, DefaultActivation.ReLU, DefaultActivation.ReLU,
            DefaultActivation.ReLU, DefaultActivation.TanH, DefaultActivation.TanH, DefaultActivation.TanH
            },
            new Activation[8]
            {
                DefaultActivation.TanH, DefaultActivation.TanH,
            DefaultActivation.ReLU, DefaultActivation.LeakyReLU, DefaultActivation.ReLU, DefaultActivation.LeakyReLU, DefaultActivation.BinaryStep, DefaultActivation.ReLU
            }
        };
        double activationFalseType = 5D;
        Activation[] activationFalseLength1D = new Activation[14]
        {
            DefaultActivation.ReLU, DefaultActivation.LeakyReLU, DefaultActivation.ReLU, DefaultActivation.LeakyReLU, DefaultActivation.BinaryStep,
            DefaultActivation.ReLU, DefaultActivation.TanH, DefaultActivation.TanH, DefaultActivation.ReLU,
            DefaultActivation.LeakyReLU, DefaultActivation.ReLU, DefaultActivation.LeakyReLU, DefaultActivation.BinaryStep, DefaultActivation.ReLU
        };
        Activation[][] activationFalseLength2D = new Activation[2][]
        {
            new Activation[9]
            {
                DefaultActivation.TanH, DefaultActivation.TanH, DefaultActivation.ReLU, DefaultActivation.LeakyReLU, DefaultActivation.Sigmoid,
                DefaultActivation.ReLU, DefaultActivation.LeakyReLU, DefaultActivation.BinaryStep, DefaultActivation.ReLU
            },
            new Activation[5]
            {
                DefaultActivation.TanH, DefaultActivation.TanH, DefaultActivation.ReLU, DefaultActivation.LeakyReLU, DefaultActivation.Sigmoid
            }
        };
        


        int[] architecture = new int[6] { 2, 3, 4, 5, 4, 2 };

        NeuralNet newNetwork = new NeuralNet("my man!", architecture, weight, bias, activation);

        Debug.Log(Export.JSON(newNetwork));
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
