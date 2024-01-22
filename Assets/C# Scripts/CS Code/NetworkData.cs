using System;
using System.Collections.Generic;

namespace NeuralNetworks
{
    public class NetworkData
    {
        // - Network Data Fields - //

        public readonly string id;

        public int[] Architecture { get; private set; } // The underlying Layer structure of the Network //

        public List<Layer> Layers { get; private set; } // The Layer Objects of the Network, containing Weights, Biases and Activation Funcs //


        public int Population { get; private set; } // Number of Nodes //
        public int Depth => Architecture.Length; // Number of Layers //

        public int HiddenPopulation { get; private set; } // Number of Nodes, excluding the Input Layer //
        public int FullWeights { get; private set; } // Number of Weights in a fully Connected Network of this Architecture //
        public int ActiveWeights { get; private set; } // Number of Weights in this Network (Non-Pruned) //


        public NetworkInitData InitData { get; private set; } // The Initialization Data (NetworkInitData object) used for the Network //


        // - NetworkData Constructor Methods - //

        private void FormatData()
        {
            Initialize();
            FormatInitData();
        }
        private void Initialize()
        {
            Architecture = InitData.Architecture;
            HiddenPopulation = 0;
            FullWeights = 0;
            Layers = new();

            for (int L = 1; L < Depth; L++)
            {
                HiddenPopulation += Architecture[L];
                FullWeights += Architecture[L] * Architecture[L - 1];

                Layers.Add(new Layer());

                for (int i = 0; i < Architecture[L]; i++)
                {
                    Layers[L - 1].Nodes.Add(new Neuron(Architecture[L - 1]));
                }
            }

            Population = HiddenPopulation + Architecture[0];
            ActiveWeights = FullWeights;
        }
        private void FormatInitData()
        {
            if (InitData.Activations is Array)
            {
                Activation[] activationInit = (Activation[])InitData.Activations;

                if (activationInit.Length == Depth - 1)
                {
                    for (int L = 0; L < Depth - 1; L++)
                    {
                        for (int i = 0; i < Layers[L].Length; i++)
                        {
                            Layers[L].Nodes[i].ActivationFunc = activationInit[L];
                        }
                    }
                }
                else if (activationInit.Length == HiddenPopulation)
                {
                    int index = 0;

                    for (int L = 0; L < Depth - 1; L++)
                    {
                        for (int i = 0; i < Layers[L].Length; i++)
                        {
                            Layers[L].Nodes[i].ActivationFunc = activationInit[index];
                            index++;
                        }
                    }
                }
            }
            else
            {
                Activation activationInit = (Activation)InitData.Activations;

                for (int L = 0; L < Depth - 1; L++)
                {
                    for (int i = 0; i < Layers[L].Length; i++)
                    {
                        Layers[L].Nodes[i].ActivationFunc = activationInit;
                    }
                }
            }


            if (InitData.Biases.GetType() == typeof(double[]))
            {
                double[] initBiases = (double[])InitData.Biases;

                if (initBiases.Length == Depth - 1)
                {
                    for (int L = 0; L < Depth - 1; L++)
                    {
                        for (int i = 0; i < Layers[L].Length; i++)
                        {
                            Layers[L].Nodes[i].Bias = initBiases[L];
                        }
                    }
                }
                if (initBiases.Length == HiddenPopulation)
                {
                    int index = 0;

                    for (int L = 0; L < Depth - 1; L++)
                    {
                        for (int i = 0; i < Layers[L].Length; i++)
                        {
                            Layers[L].Nodes[i].Bias = initBiases[index];
                        }
                    }
                }
            }
            else if (InitData.Biases.GetType() == typeof(Func<double>[]))
            {
                Func<double>[] initBiases = (Func<double>[])InitData.Biases;
                int randDepth = InitData.RandDepths[1];

                if (initBiases.Length == Depth - 1)
                {
                    if (randDepth == 1)
                    {
                        for (int L = 0; L < Depth - 1; L++)
                        {
                            double initBias = initBiases[L]();

                            for (int i = 0; i < Layers[L].Length; i++)
                            {
                                Layers[L].Nodes[i].Bias = initBias;
                            }
                        }
                    }
                    if (randDepth == 2)
                    {
                        for (int L = 0; L < Depth - 1; L++)
                        {
                            for (int i = 0; i < Layers[L].Length; i++)
                            {
                                Layers[L].Nodes[i].Bias = initBiases[L]();
                            }
                        }
                    }
                }
                if (initBiases.Length == HiddenPopulation)
                {
                    int index = 0;

                    for (int L = 0; L < Depth - 1; L++)
                    {
                        for (int i = 0; i < Layers[L].Length; i++)
                        {
                            Layers[L].Nodes[i].Bias = initBiases[index]();
                            index++;
                        }
                    }
                }
            }
            else if (InitData.Biases.GetType() == typeof(double))
            {
                double initBiases = (double)InitData.Biases;

                for (int L = 0; L < Depth - 1; L++)
                {
                    for (int i = 0; i < Layers[L].Length; i++)
                    {
                        Layers[L].Nodes[i].Bias = initBiases;
                    }
                }
            }
            else
            {
                Func<double> initBiases = (Func<double>)InitData.Biases;
                int randDepth = InitData.RandDepths[1];

                if (randDepth == 0)
                {
                    double initBias = initBiases();

                    for (int L = 0; L < Depth - 1; L++)
                    {
                        for (int i = 0; i < Layers[L].Length; i++)
                        {
                            Layers[L].Nodes[i].Bias = initBias;
                        }
                    }
                }
                else if (randDepth == 1)
                {
                    for (int L = 0; L < Depth - 1; L++)
                    {
                        double initBias = initBiases();

                        for (int i = 0; i < Layers[L].Length; i++)
                        {
                            Layers[L].Nodes[i].Bias = initBias;
                        }
                    }
                }
                else if (randDepth == 2)
                {
                    for (int L = 0; L < Depth - 1; L++)
                    {
                        for (int i = 0; i < Layers[L].Length; i++)
                        {
                            Layers[L].Nodes[i].Bias = initBiases();
                        }
                    }
                }
            }


            if (InitData.Weights.GetType() == typeof(double[]))
            {
                double[] initWeights = (double[])InitData.Weights;

                if (initWeights.Length == Depth - 1)
                {
                    for (int L = 0; L < Depth - 1; L++)
                    {
                        for (int i = 0; i < Layers[L].Length; i++)
                        {
                            for (int j = 0; j < Architecture[L]; j++)
                            {
                                Layers[L].Nodes[i].Weights.Add(initWeights[L]);
                                Layers[L].Nodes[i].PrunedWeights[j] = true;
                            }
                        }
                    }
                }
                else if (initWeights.Length == HiddenPopulation)
                {
                    int index = 0;

                    for (int L = 0; L < Depth - 1; L++)
                    {
                        for (int i = 0; i < Layers[L].Length; i++)
                        {
                            for (int j = 0; j < Architecture[L]; j++)
                            {
                                Layers[L].Nodes[i].Weights.Add(initWeights[index]);
                                Layers[L].Nodes[i].PrunedWeights[j] = true;
                            }

                            index++;
                        }
                    }
                }
                else if (initWeights.Length == FullWeights)
                {
                    int index = 0;

                    for (int L = 0; L < Depth - 1; L++)
                    {
                        for (int i = 0; i < Layers[L].Length; i++)
                        {
                            for (int j = 0; j < Architecture[L]; j++)
                            {
                                Layers[L].Nodes[i].Weights.Add(initWeights[index]);
                                Layers[L].Nodes[i].PrunedWeights[j] = true;
                                index++;
                            }
                        }
                    }
                }
            }
            else if (InitData.Weights.GetType() == typeof(Func<double>[]))
            {
                Func<double>[] initWeights = (Func<double>[])InitData.Weights;
                int randDepth = InitData.RandDepths[0];

                if (initWeights.Length == Depth - 1)
                {
                    if (randDepth == 1)
                    {
                        for (int L = 0; L < Depth - 1; L++)
                        {
                            double initValue = initWeights[L]();

                            for (int i = 0; i < Layers[L].Length; i++)
                            {
                                for (int j = 0; j < Architecture[L]; j++)
                                {
                                    Layers[L].Nodes[i].Weights.Add(initValue);
                                    Layers[L].Nodes[i].PrunedWeights[j] = true;
                                }
                            }
                        }
                    }
                    else if (randDepth == 2)
                    {
                        for (int L = 0; L < Depth - 1; L++)
                        {
                            for (int i = 0; i < Layers[L].Length; i++)
                            {
                                double initValue = initWeights[L]();

                                for (int j = 0; j < Architecture[L]; j++)
                                {
                                    Layers[L].Nodes[i].Weights.Add(initValue);
                                    Layers[L].Nodes[i].PrunedWeights[j] = true;
                                }
                            }
                        }
                    }
                    else if (randDepth == 3)
                    {
                        for (int L = 0; L < Depth - 1; L++)
                        {
                            for (int i = 0; i < Layers[L].Length; i++)
                            {
                                for (int j = 0; j < Architecture[L]; j++)
                                {
                                    Layers[L].Nodes[i].Weights.Add(initWeights[L]());
                                    Layers[L].Nodes[i].PrunedWeights[j] = true;
                                }
                            }
                        }
                    }
                }
                else if (initWeights.Length == HiddenPopulation)
                {
                    if (randDepth == 2)
                    {
                        int index = 0;

                        for (int L = 0; L < Depth - 1; L++)
                        {
                            for (int i = 0; i < Layers[L].Length; i++)
                            {
                                double initValue = initWeights[index]();

                                for (int j = 0; j < Architecture[L]; j++)
                                {
                                    Layers[L].Nodes[i].Weights.Add(initValue);
                                    Layers[L].Nodes[i].PrunedWeights[j] = true;
                                }

                                index++;
                            }
                        }
                    }
                    else if (randDepth == 3)
                    {
                        int index = 0;

                        for (int L = 0; L < Depth - 1; L++)
                        {
                            for (int i = 0; i < Layers[L].Length; i++)
                            {
                                for (int j = 0; j < Architecture[L]; j++)
                                {
                                    Layers[L].Nodes[i].Weights.Add(initWeights[index]());
                                    Layers[L].Nodes[i].PrunedWeights[j] = true;
                                }

                                index++;
                            }
                        }
                    }
                }
                else if (initWeights.Length == FullWeights)
                {
                    int index = 0;

                    for (int L = 0; L < Depth - 1; L++)
                    {
                        for (int i = 0; i < Layers[L].Length; i++)
                        {
                            for (int j = 0; j < Architecture[L]; j++)
                            {
                                Layers[L].Nodes[i].Weights.Add(initWeights[index]());
                                Layers[L].Nodes[i].PrunedWeights[j] = true;
                                index++;
                            }
                        }
                    }
                }
            }
            else if (InitData.Weights.GetType() == typeof(double))
            {
                double initWeights = (double)InitData.Weights;

                for (int L = 0; L < Depth - 1; L++)
                {
                    for (int i = 0; i < Layers[L].Length; i++)
                    {
                        for (int j = 0; j < Architecture[L]; j++)
                        {
                            Layers[L].Nodes[i].Weights.Add(initWeights);
                            Layers[L].Nodes[i].PrunedWeights[j] = true;
                        }
                    }
                }
            }
            else
            {
                Func<double> initWeights = (Func<double>)InitData.Weights;
                int randDepth = InitData.RandDepths[0];

                if (randDepth == 0)
                {
                    double initValue = initWeights();

                    for (int L = 0; L < Depth - 1; L++)
                    {
                        for (int i = 0; i < Layers[L].Length; i++)
                        {
                            for (int j = 0; j < Architecture[L]; j++)
                            {
                                Layers[L].Nodes[i].Weights.Add(initValue);
                                Layers[L].Nodes[i].PrunedWeights[j] = true;
                            }
                        }
                    }
                }
                else if (randDepth == 1)
                {
                    for (int L = 0; L < Depth - 1; L++)
                    {
                        double initValue = initWeights();

                        for (int i = 0; i < Layers[L].Length; i++)
                        {
                            for (int j = 0; j < Architecture[L]; j++)
                            {
                                Layers[L].Nodes[i].Weights.Add(initValue);
                                Layers[L].Nodes[i].PrunedWeights[j] = true;
                            }
                        }
                    }
                }
                else if (randDepth == 2)
                {
                    for (int L = 0; L < Depth - 1; L++)
                    {
                        for (int i = 0; i < Layers[L].Length; i++)
                        {
                            double initValue = initWeights();

                            for (int j = 0; j < Architecture[L]; j++)
                            {
                                Layers[L].Nodes[i].Weights.Add(initValue);
                                Layers[L].Nodes[i].PrunedWeights[j] = true;
                            }
                        }
                    }
                }
                else if (randDepth == 3)
                {
                    for (int L = 0; L < Depth - 1; L++)
                    {
                        for (int i = 0; i < Layers[L].Length; i++)
                        {
                            for (int j = 0; j < Architecture[L]; j++)
                            {
                                Layers[L].Nodes[i].Weights.Add(initWeights());
                                Layers[L].Nodes[i].PrunedWeights[j] = true;
                            }
                        }
                    }
                }
            }
        }

        public NetworkData(string id, NetworkInitData initData)
        {
            this.id = id;

            InitData = initData;

            FormatData();
        }
        public NetworkData(NetworkInitData initData)
            : this("Anonymous", initData) { }

        public NetworkData(string id, int[] architecture, object weightInput = null, object biasInput = null, object activationInput = null, params int[] randDepths)
        {
            this.id = id;

            InitData = new NetworkInitData(architecture, weightInput, biasInput, activationInput, randDepths);
            
            FormatData();
        }
        public NetworkData(int[] architecture, object weightInput = null, object biasInput = null, object activationInput = null, params int[] randDepths)
            : this("Anonymous", architecture, weightInput, biasInput, activationInput, randDepths) { }

        public NetworkData(string id, NetworkData oldNetwork)
        {
            this.id = id;

            InitData = oldNetwork.InitData;

            FormatData();
        }
        public NetworkData(NetworkData oldNetwork)
            : this("Anonymous", oldNetwork) { }



        // ~~~ // ~~~ // ~~~ // ~~~ // ~~~ // ~~~ //



        /*private List<double> GetAllWeights()
        {

        }


        private void WeightMergeSort()
        {
            // If ordering by the Weights' values,
            // we assume there are little to no repetitions
            // and as a result run MergeSort.
            double[,] auxiliaryArray = new double[Weights.Count, 3];

            for (int i = 0; i < Weights.Count; i++)
            {
                auxiliaryArray[i, 0] = Weights[i].origin;
                auxiliaryArray[i, 1] = Weights[i].target;
                auxiliaryArray[i, 2] = Weights[i].value;
            }


            for (int gLength = 1; gLength < Weights.Count; gLength *= 2)
            {
                int count = 0;

                if (Math.Log(gLength, 2) % 2 == 0)
                {
                    for (int gNum = 0; gNum < Math.Floor(Math.Ceiling(Weights.Count / (double)gLength) / 2); gNum++)
                    {
                        int i = 0;
                        int j = 0;

                        int iIndex = gNum * gLength;
                        int jIndex = (gNum + 1) * gLength;

                        while (i != gLength || !(j == gLength || jIndex + j == Weights.GetLength(0)))
                        {
                            if (i == gLength)
                            {
                                auxiliaryArray[count, 0] = Weights[jIndex + j, 0];
                                auxiliaryArray[count, 1] = Weights[jIndex + j, 1];
                                auxiliaryArray[count, 2] = Weights[jIndex + j, 2];

                                j++;
                                count++;
                            }
                            else if (j == gLength || jIndex + j == Weights.GetLength(0))
                            {
                                auxiliaryArray[count, 0] = Weights[iIndex + i, 0];
                                auxiliaryArray[count, 1] = Weights[iIndex + i, 1];
                                auxiliaryArray[count, 2] = Weights[iIndex + i, 2];

                                i++;
                                count++;
                            }
                            else if (Weights[iIndex + i, 2] >= Weights[jIndex + j, 2])
                            {
                                auxiliaryArray[count, 0] = Weights[iIndex + i, 0];
                                auxiliaryArray[count, 1] = Weights[iIndex + i, 1];
                                auxiliaryArray[count, 2] = Weights[iIndex + i, 2];

                                i++;
                                count++;
                            }
                            else
                            {
                                auxiliaryArray[count, 0] = Weights[jIndex + j, 0];
                                auxiliaryArray[count, 1] = Weights[jIndex + j, 1];
                                auxiliaryArray[count, 2] = Weights[jIndex + j, 2];

                                j++;
                                count++;
                            }
                        }
                    }
                }
                else
                {
                    for (int gNum = 0; gNum < Math.Floor(Math.Ceiling(Weights.GetLength(0) / (double)gLength) / 2); gNum++)
                    {
                        int i = 0;
                        int j = 0;

                        int iIndex = gNum * gLength;
                        int jIndex = (gNum + 1) * gLength;

                        while (i != gLength || !(j == gLength || jIndex + j == Weights.GetLength(0)))
                        {
                            if (i == gLength)
                            {
                                Weights[count, 0] = auxiliaryArray[jIndex + j, 0];
                                Weights[count, 1] = auxiliaryArray[jIndex + j, 1];
                                Weights[count, 2] = auxiliaryArray[jIndex + j, 2];

                                j++;
                                count++;
                            }
                            else if (j == gLength || jIndex + j == Weights.GetLength(0))
                            {
                                Weights[count, 0] = auxiliaryArray[iIndex + i, 0];
                                Weights[count, 1] = auxiliaryArray[iIndex + i, 1];
                                Weights[count, 2] = auxiliaryArray[iIndex + i, 2];

                                i++;
                                count++;
                            }
                            else if (Weights[iIndex + i, 2] >= Weights[jIndex + j, 2])
                            {
                                Weights[count, 0] = auxiliaryArray[iIndex + i, 0];
                                Weights[count, 1] = auxiliaryArray[iIndex + i, 1];
                                Weights[count, 2] = auxiliaryArray[iIndex + i, 2];

                                i++;
                                count++;
                            }
                            else
                            {
                                Weights[count, 0] = auxiliaryArray[jIndex + j, 0];
                                Weights[count, 1] = auxiliaryArray[jIndex + j, 1];
                                Weights[count, 2] = auxiliaryArray[jIndex + j, 2];

                                j++;
                                count++;
                            }
                        }
                    }
                }
            }
        }*/


            // - Adjustment Methods - //

        public void AdjustWeights(double[] weightChange, double strength = 1)
        {
            if (weightChange.Length != ActiveWeights)
            {
                Console.WriteLine("Adjusting Weights Failed: Gradient Array not of same length as Weight Array.");
                return;
            }

            int index = 0;
            for (int L = 0; L < Layers.Count; L++)
            {
                for (int i = 0; i < Layers[L].Nodes.Count; i++)
                {
                    for (int j = 0; j < Layers[L].Nodes[i].Weights.Count; j++)
                    {
                        Layers[L].Nodes[i].Weights[j] -= weightChange[index] * strength;
                        index++;
                    }
                }
            }
        }

        public void AdjustWeights(double[][][] weightChange, double strength = 1)
        {
            if (weightChange.Length != Depth - 1)
            {
                Console.WriteLine("Adjusting Weights Failed: Gradient Array not of same size as Weight Array.");
                return;
            }

            for (int L = 0; L < Depth - 1; L++)
            {
                if (weightChange[L].Length != Architecture[L + 1])
                {
                    Console.WriteLine("Adjusting Weights Failed: Gradient Array not of same size as Weight Array.");
                    return;
                }

                for (int i = 0; i < weightChange[L].Length; i++)
                {
                    if (weightChange[L][i].Length != Layers[L].Nodes[i].Weights.Count)
                    {
                        Console.WriteLine("Adjusting Weights Failed: Gradient Array not of same size as Weight Array.");
                        return;
                    }

                    for (int j = 0; j < weightChange[L][i].Length; j++)
                    {
                        Layers[L].Nodes[i].Weights[j] -= weightChange[L][i][j] * strength;
                    }
                }
            }
        }


        public void AdjustBiases(double[] biasChange, double strength = 1)
        {
            if (biasChange.Length != HiddenPopulation)
            {
                Console.WriteLine("Adjusting Biases Failed: Gradient Array not of same length as Bias Array.");
                return;
            }

            int index = 0;
            for (int L = 0; L < Layers.Count; L++)
            {
                for (int i = 0; i < Layers[L].Nodes.Count; i++)
                {
                    Layers[L].Nodes[i].Bias -= biasChange[index] * strength;
                    index++;
                }
            }
        }

        public void AdjustBiases(double[][] biasChange, double strength = 1)
        {
            if (biasChange.Length != Depth - 1)
            {
                Console.WriteLine("Adjusting Biases Failed: Gradient Array not of same size as Bias Array.");
                return;
            }

            for (int L = 0; L < biasChange.Length; L++)
            {
                if (biasChange[L].Length != Architecture[L + 1])
                {
                    Console.WriteLine("Adjusting Biases Failed: Gradient Array not of same size as Bias Array.");
                    return;
                }

                for (int i = 0; i < Architecture[L + 1]; i++)
                {
                    Layers[L].Nodes[i].Bias -= biasChange[L][i] * strength;
                }
            }
        }


            // - Reconfiguration Methods - //

        public void ReConfig(NetworkInitData initData)
        {
            InitData = initData;

            FormatData();
        }

        public void ReConfig()
        {
            FormatInitData();
        }
    }
}