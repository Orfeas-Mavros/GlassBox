using System;

namespace NeuralNetworks
{
    public class NetworkTrainer : GradientNet
    {
           // - NetworkTrainer Added Fields - //

        public double learningRate;
        
        // The previous Gradient Arrays, for the Momentum calculation.
        private double[][][] prevWeightGradients;
        private double[][] prevBiasGradients;
        public double momentumRate;

        private int _regularizationMethod;
        public int RegularizationMethod
        {
            get
            {
                return _regularizationMethod;
            }
            set
            {
                if (value == 0 || value == 1 || value == 2)
                {
                    _regularizationMethod = value;
                }
                else
                {
                    Console.Write("RegularizationMethod must be either equal to 0 (for no Regularization), ");
                    Console.WriteLine("equal to 1 (for L1 - LASSO Regularization) or equal to 2 (for L2 - Ridge Regularization).");
                }
            }
        } // 0 for none, 1 for L1 (LASSO) and 2 for L2 (Ridge) //
        private double _regularizationCoefficient;
        public double RegularizationCoefficient
        {
            get
            {
                return _regularizationCoefficient;
            }
            set
            {
                if (value < 0)
                {
                    Console.WriteLine("The Regularization Coefficient must be a value between 0 and 1 (Inclusive).");
                    _regularizationCoefficient = 0;
                }
                else if (value > 1)
                {
                    Console.WriteLine("The Regularization Coefficient must be a value between 0 and 1 (Inclusive).");
                    _regularizationCoefficient = 1;
                }
                else
                {
                    _regularizationCoefficient = value;
                }
            }
        } // Strength of the Regularization, between 0 and 1 //

        public double pruningThreshold; // The Threshold Value for Pruning. If 0, Prunes the smallest Value. //

        /*public int PruningStructure
        {
            get
            {
                return PruningStructure;
            }
            set
            {
                if (value == 0 || value == 1 || value == 2 || value == 3)
                {
                    PruningStructure = value;
                }
                else
                {
                    Console.Write("Pruning must be either equal to 0 (for no Pruning), equal to 1 (for Weight Pruning), ");
                    Console.WriteLine("equal to 2 (for Node Pruning) or equal to 3 (for Layer Pruning).");
                }
            }
        } // The Structure to Prune //
        public int PruningDerivative
        {
            get
            {
                return PruningDerivative;
            }
            set
            {

            }
        } // The Derivative of the Structure's Value to be compared to the Threshold //
        public int[] PruningData
        {
            get
            {
                return new int[3] { PruningStructure, PruningDerivative, pruningThreshold };
            }
            set { }
        }*/
        // Redundant


        // - NetworkTrainer Constructors - //

        public NetworkTrainer(string id, NetworkInitData initData)
            : base(id, initData) { }
        public NetworkTrainer(NetworkInitData initData)
            : base("Anonymous", initData) { }

        public NetworkTrainer(string id, int[] architecture, object weightInput = null, object biasInput = null, object activationInput = null, params int[] randDepths)
            : base(id, architecture, weightInput, biasInput, activationInput, randDepths) { }
        public NetworkTrainer(int[] architecture, object weightInput = null, object biasInput = null, object activationInput = null, params int[] randDepths)
            : base("Anonymous", architecture, weightInput, biasInput, activationInput, randDepths) { }

        public NetworkTrainer(string id, NetworkData oldNetwork)
            : base(id, oldNetwork) { }
        public NetworkTrainer(NetworkData oldNetwork)
            : base("Anonymous", oldNetwork) { }

        public NetworkTrainer(string id, NetworkTrainer oldNetwork)
            : base(id, oldNetwork.InitData)
        {
            learningRate = oldNetwork.learningRate;
            momentumRate = oldNetwork.momentumRate;
            RegularizationMethod = oldNetwork.RegularizationMethod;
            RegularizationCoefficient = oldNetwork.RegularizationCoefficient;
            pruningThreshold = oldNetwork.pruningThreshold;
        }
        public NetworkTrainer(NetworkTrainer oldNetwork)
            : this("Anonymous", oldNetwork) { }

        public NetworkTrainer(string id, NetworkData oldNetwork,
            double learningRate, double momentumRate,
            int regularizationMethod, double regularizationCoefficient, double pruningThreshold)
            : base(id, oldNetwork)
        {
            this.learningRate = learningRate;
            this.momentumRate = momentumRate;
            RegularizationCoefficient = regularizationCoefficient;
            RegularizationMethod = regularizationMethod;
            this.pruningThreshold = pruningThreshold;
        }
        public NetworkTrainer(NetworkData oldNetwork,
            double learningRate, double momentumRate,
            int regularizationMethod, double regularizationCoefficient, double pruningThreshold)
            : this("Anonymous", oldNetwork, learningRate, momentumRate,
                  regularizationMethod, regularizationCoefficient, pruningThreshold) { }



        // ~~~ // ~~~ // ~~~ // ~~~ // ~~~ // ~~~ //



        public void RunEpoch(double[] ideal)
        {
            CalcGradients(ideal);
            BackPropagate();
        }


        public void BackPropagate()
        {
            // Accounting for Regularization //
            PerformRegularization();


            // Adjusting by the Gradients //
            AdjustBiases(NodeGradients, learningRate);
            AdjustWeights(WeightGradients, learningRate);

            // Adjusting by Momentum and Updating Momentum Terms //
            PerformMomentum();
        }

        private void PerformRegularization()
        {
            if (RegularizationMethod == 1)
            {
                for (int L = 0; L < Depth - 1; L++)
                {
                    for (int i = 0; i < Architecture[L + 1]; i++)
                    {
                        Layers[L].Nodes[i].Bias -=
                            RegularizationCoefficient * Math.Sign(Layers[L].Nodes[i].Bias);

                        for (int j = 0; j < Layers[L].Nodes[i].Weights.Count; j++)
                        {
                            Layers[L].Nodes[i].Weights[j] -=
                            RegularizationCoefficient * Math.Sign(Layers[L].Nodes[i].Weights[j]);
                        }
                    }
                }
            }
            else if (RegularizationMethod == 2)
            {
                for (int L = 0; L < Depth - 1; L++)
                {
                    for (int i = 0; i < Architecture[L + 1]; i++)
                    {
                        Layers[L].Nodes[i].Bias -=
                            RegularizationCoefficient * 2 * Layers[L].Nodes[i].Bias;

                        for (int j = 0; j < Layers[L].Nodes[i].Weights.Count; j++)
                        {
                            Layers[L].Nodes[i].Weights[j] -=
                            RegularizationCoefficient * 2 * Layers[L].Nodes[i].Weights[j];
                        }
                    }
                }
            }
        }

        private void PerformMomentum()
        {
            // Adjusting by the Momentum Terms //
            if (momentumRate != 0)
            {
                AdjustBiases(prevBiasGradients, momentumRate);
                AdjustWeights(prevWeightGradients, momentumRate);
            }

            // Calculating the new Momentum Terms //
            prevBiasGradients = new double[NodeGradients.Length][];
            for (int i = 0; i < NodeGradients.Length; i++)
            {
                prevBiasGradients[i] = new double[NodeGradients[i].Length];

                for (int j = 0; j < NodeGradients[i].Length; j++)
                {
                    prevBiasGradients[i][j] = NodeGradients[i][j];
                }
            }

            prevWeightGradients = new double[WeightGradients.Length][][];
            for (int i = 0; i < WeightGradients.Length; i++)
            {
                prevWeightGradients[i] = new double[WeightGradients[i].Length][];

                for (int j = 0; j < WeightGradients[i].Length; j++)
                {
                    prevWeightGradients[i][j] = new double[WeightGradients[i][j].Length];

                    for (int k = 0; k < WeightGradients[i][j].Length; k++)
                    {
                        prevWeightGradients[i][j][k] = WeightGradients[i][j][k];
                    }
                }
            }
        }
    }
}