using System;

namespace NeuralNetworks
{
    public partial class NeuralNet
    {
            // - Class Fields - //

        public readonly string id; // An Identifying name for the Network //
        public string State { get; private set; } // A brief Description of what the Network is currently doing //
        public int[] Architecture { get; private set; } // The underlying Layer structure of the Network //
        public int Population { get; private set; } // Number of Nodes //
        public int Depth { get; private set; } // Number of Layers //

        // Pointers is the Array containing the index of the first element of each Layer.
        // So the ith Node of the Lth Layer is Nodes[pointers[L] + i].
        private int[] pointers;
        private int hiddenPopulation; // Number of Nodes, excluding the Input Layer //
        private int fullWeights; // Number of Weights in a fully Connected Network of this Architecture //


        // The Weights 2D Array is a Table with 3 Columns, where
        // The first item represents the origin of the Weight,
        // The second represents its target and the third its value.
        public double[,] Weights { get; private set; }
        public double[] Biases { get; private set; }
        public Activation[] ActivationFuncs { get; private set; }

        // The BackPropagation Calculus Gradient Array for Nodes, Weights and Biases.
        public double[] NodeGradients { get; private set; }
        public double[] WeightGradients { get; private set; }
        public double[] BiasGradients { get; private set; }
        public double learningRate;

        // The previous Gradient Arrays, for the Momentum calculation.
        private double[] prevWeightGradients;
        private double[] prevBiasGradients;
        public double momentumRate;


        // The Regularization Information
        public int RegulatizationMethod
        {
            get
            {
                return RegulatizationMethod;
            }
            set
            {
                if (value == 0 || value == 1 || value == 2) 
                {
                    RegulatizationMethod = value;
                }
                else
                {
                    Console.Write("RegularizationMethod must be either equal to 0 (for no Regularization), ");
                    Console.WriteLine("equal to 1 (for L1 - LASSO Regularization) or equal to 2 (for L2 - Ridge Regularization).");
                }
            }
        } // 0 for none, 1 for L1 (LASSO) and 2 for L2 (Ridge) //
        public double RegularizationCoefficient
        {
            get
            {
                return RegularizationCoefficient;
            }
            set
            {
                if (value < 0)
                {
                    Console.WriteLine("The Regularization Coefficient must be a value between 0 and 1 (Inclusive).");
                    RegularizationCoefficient = 0;
                }
                else if (value > 1)
                {
                    Console.WriteLine("The Regularization Coefficient must be a value between 0 and 1 (Inclusive).");
                    RegularizationCoefficient = 1;
                }
                else
                {
                    RegularizationCoefficient = value;
                }
            }
        }


        public double[] Nodes { get; private set; } // The Signal Array (Including Input Layer) //
        public double[] Output { get; private set; } // The final Layer //


        public NetworkSetupData SetupData { get; private set; } // The Initialization Data used for this Network //
        public NetworkData NetData
        {
            private set
            {
                NetData = value;
            }
            get
            {
                return new NetworkData(Architecture, Weights, Biases, ActivationFuncs, SetupData);
            }
        }


            // - Setup  &  Init Methods - //

        private void NetworkConstructorSetup(int[] architecture)
        {
            Depth = architecture.Length;
            Architecture = new int[Depth];

            for (int L = 0; L < Depth; L++)
            {
                Architecture[L] = architecture[L];
            }

            pointers = new int[Depth];
            Population = Architecture[0];
            fullWeights = 0;

            for (int L = 1; L < Depth; L++)
            {
                pointers[L - 1] = Population;
                Population += Architecture[L];
                fullWeights += Architecture[L] * Architecture[L - 1];
            }
            hiddenPopulation = Population - Architecture[0];
        }
        public NeuralNet(string id, int[] architecture)
        {
            State = "Init";
            this.id = id;
            NetworkConstructorSetup(architecture);

            SetupData = new NetworkSetupData(architecture, 1D, 1D, DefaultActivation.ReLU);

            //InitWeights();
            //InitBiases();
            //InitActivation();

            State = "Idle";
        }
        public NeuralNet(int[] architecture)
            : this("blank", architecture)
        {
            Console.WriteLine("Network Initialized without Id.");
        }
        public NeuralNet(string id, int[] architecture, object nodeWeights, object nodeBiases, object nodeFuncs, int weightDepth = 3, int biasDepth = 2)
        {
            State = "Init";
            this.id = id;
            NetworkConstructorSetup(architecture);

            SetupData = new NetworkSetupData(architecture, nodeWeights, nodeBiases, nodeFuncs, weightDepth, biasDepth);

            //InitWeights(nodeWeights, weightDepth);
            //InitBiases(nodeBiases, biasDepth);
            //InitActivation(nodeFuncs);

            State = "Idle";
        }
        public NeuralNet(int[] architecture, object nodeWeights, object nodeBiases, object nodeFuncs, int weightDepth = 3, int biasDepth = 2)
            : this("blank", architecture, nodeWeights, nodeBiases, nodeFuncs, weightDepth, biasDepth)
        {
            Console.WriteLine("Network Initialized without Id.");
        }   
        public NeuralNet(string id, NetworkData networkData)
        {

        }
        public NeuralNet(NetworkData networkData)
        {

        }
        public NeuralNet(string id, NetworkSetupData networkSetupData)
        {

        }
        public NeuralNet(NetworkSetupData networkSetupData)
        {

        }
    }
}