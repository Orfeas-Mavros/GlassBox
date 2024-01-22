using System;
using System.Collections.Generic;

namespace NeuralNetworks
{
    public abstract class Node
    {
        // The Foundational Computational Units of any Neural Network //


            // - Node Fields - //

        // The Weights, which correspond to the previous Layer's length //
        public List<double> Weights { get; set; }
        public double Bias { get; set; } // The Node's Bias' Value //
        public Activation ActivationFunc { get; set; } // The Node's Activation Function //

        // In the PrunedWeights Boolean Array, each Boolean indicates whether or not the Weight has not been Pruned //
        public bool[] PrunedWeights { get; protected set; }

        public double Signal { get; protected set; } // The Output Signal //


            // The Running Method //
        
        public abstract void Run(double[] inputs);

        public abstract void PruneWeight(int index, bool fromList = false);

    }
}