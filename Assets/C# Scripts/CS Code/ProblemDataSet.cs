using System;
using System.Collections.Generic;

namespace NeuralNetworks
{
    public class ProblemDataset
    {
        public List<ProblemData> problemData; // List of Pairs (Input - Ideal) of Problem Data arrays //

        public int[] Format => new int[2]
        {
            problemData[0].input.Length,
            problemData[0].ideal.Length
        }; // The Sizes of the Input and Ideal Arrays, int[2] { sizeIn, sizeOut } //
        public int Size => problemData.Count; // The Number of Pieces of Data //


        public void SetData(double[][] inputs, double[][] ideals)
        {
            if (inputs.Length != ideals.Length)
            {
                Console.WriteLine("Dataset Initialization Error: Not as many Input Cases as Output Cases");
                return;
            }

            problemData = new List<ProblemData>();

            for (int i = 0; i < inputs.Length; i++)
            {
                problemData.Add(new ProblemData(inputs[i], ideals[i]));
            }
        }


        public ProblemDataset(List<ProblemData> listData)
        {
            problemData = new List<ProblemData>();

            for (int i = 0; i < listData.Count; i++)
            {
                problemData.Add(listData[i].Clone());
            }
        }

        public ProblemDataset(double[][] inputs, double[][] outputs)
        {
            SetData(inputs, outputs);
        }

        // Two ways to input Problem Data (from a 3D array): //
        // An Array of Pairs of the Problem Data Arrays (twoArrays = false) - (Default)
        // For Example { { inputs[0], ideals[0] } ,  { inputs[1], ideals[1] } , ... }
        // Or
        // A Pair of Arrays of the Problem Data Arrays (twoArrays = true)
        // For Example { { inputs[0], inputs[1], ... } ,  { ideals[0], ideals[1], ... } }
        public ProblemDataset(double[][][] dataset, bool twoArrays = false)
        {
            if (twoArrays)
            {
                SetData(dataset[0], dataset[1]);
            }
            else
            {
                problemData = new List<ProblemData>();

                try
                {
                    for (int i = 0; i < dataset.Length; i++)
                    {
                        problemData.Add(new ProblemData(dataset[i]));
                    }
                }
                catch
                {
                    Console.WriteLine("Dataset Initialization Failed.");
                    problemData = null;
                }
            }
        }


        public void FormatUnsupervised()
        {
            for (int i = 0; i < Size; i++)
            {
                problemData[i].FormatUnsupervised();
            }
        }


        public ProblemDataset SupervisedClone()
        {
            ProblemDataset newDataset = Clone();

            newDataset.FormatUnsupervised();

            return newDataset;
        }

        public ProblemDataset Clone()
        {
            return new ProblemDataset(problemData);
        }

        public double[][][] GetArray(bool twoArrays = false)
        {
            if (twoArrays)
            {
                double[][][] arrayData = new double[2][][];
                int sizeIn = Format[0];
                int sizeOut = Format[1];

                arrayData[0] = new double[Size][];
                arrayData[1] = new double[Size][];

                for (int i = 0; i < Size; i++)
                {
                    arrayData[0][i] = new double[sizeIn];
                    arrayData[1][i] = new double[sizeOut];

                    for (int j = 0; j < sizeIn; j++)
                    {
                        arrayData[0][i][j] = problemData[i].input[j];
                    }

                    for (int j = 0; j < sizeOut; j++)
                    {
                        arrayData[1][i][j] = problemData[i].ideal[j];
                    }
                }

                return arrayData;
            }
            else
            {
                double[][][] arrayData = new double[Size][][];
                int sizeIn = Format[0];
                int sizeOut = Format[1];

                for (int i = 0; i < Size; i++)
                {
                    arrayData[i] = new double[2][];
                    arrayData[i][0] = new double[sizeOut];
                    arrayData[i][1] = new double[sizeIn];

                    for (int j = 0; j < sizeIn; j++)
                    {
                        arrayData[i][0][j] = problemData[i].input[j];
                    }

                    for (int j = 0; j < sizeOut; j++)
                    {
                        arrayData[i][1][j] = problemData[i].ideal[j];
                    }
                }

                return arrayData;
            }
        }
    }
}