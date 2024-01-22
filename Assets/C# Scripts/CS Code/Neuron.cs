using System;

namespace NeuralNetworks
{
    public class Neuron : Node
    {
            // - Neuron Constructors - //

        public Neuron(int prevLength)
        {
            Weights = new();
            PrunedWeights = new bool[prevLength];
        }

        public Neuron(int prevLength, double bias, Activation activation)
            : this(prevLength)
        {
            Bias = bias;
            ActivationFunc = activation;
        }

        public Neuron(double[] initWeight, double bias, Activation activation)
            : this(initWeight.Length)
        {
            Bias = bias;
            ActivationFunc = activation;

            for (int i = 0; i < initWeight.Length; i++)
            {
                Weights.Add(initWeight[i]);
                PrunedWeights[i] = true;
            }
        }

        public Neuron(Node oldNode)
            : this(oldNode.PrunedWeights.Length)
        {
            Bias = oldNode.Bias;
            ActivationFunc = oldNode.ActivationFunc;

            for (int i = 0; i < oldNode.Weights.Count; i++)
            {
                Weights.Add(oldNode.Weights[i]);
                PrunedWeights[i] = true;
            }
        }


            // - Implemented Node Methods - //

        // Run Method //
        public override void Run(double[] inputs)
        {
            Signal = Bias; // Adding the Bias first

            int weightIndex = 0;
            for (int i = 0; i < inputs.Length; i++)
            {
                if (!PrunedWeights[i])
                {
                    continue; // Only adding Active Nodes
                }

                Signal += Weights[weightIndex] * inputs[i]; // Adding the Weighted Input Signal
                weightIndex++; // Moving to the next Weight Value
            }

            Signal = ActivationFunc.Activate(Signal); // Passing through the Activation Function
        }


        // Prune Methods //
        // Either according to their Index in the Weight List
        // Or according to their actual Index in the input Array
        public override void PruneWeight(int index, bool fromList = false)
        {
            int weightIndex = 0;

            for (int i = 0; i < PrunedWeights.Length; i++)
            {
                if (!PrunedWeights[i])
                {
                    continue;
                }

                if ((index == weightIndex && fromList) || (index == i && !fromList))
                {
                    Weights.RemoveAt(weightIndex);
                    PrunedWeights[i] = false;

                    return;
                }

                weightIndex++;
            }
        }
    }
}