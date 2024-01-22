using System;
using System.Collections.Generic;

namespace NeuralNetworks
{
    public class Layer
    {
            // - Layer Fields - //

        public List<Node> Nodes; // The Nodes of the Layer //

        public int Length => Nodes.Count; // Number of Nodes of the Layer //
        public double[] Signals
        {
            get
            {
                double[] layerSignals = new double[Length];

                for (int i = 0; i < Length; i++)
                {
                    layerSignals[i] = Nodes[i].Signal;
                }

                return layerSignals;
            }
        } // Output Array of the Signal of each Node //


            // - Layer Constructor Logic - //

        public Layer()
        {
            Nodes = new();
        }

        public Layer(Layer oldLayer)
            : this()
        {
            for (int i = 0; i < oldLayer.Nodes.Count; i++)
            {
                Nodes.Add(new Neuron(oldLayer.Nodes[i]));
            }
        }


            // - Layer Methods - //

        public void Run(double[] input)
        {
            for (int i = 0; i < Length; i++)
            {
                Nodes[i].Run(input);
            }
        }


        public int PruneDeadNodes()
        {
            int prunedNodes = 0;

            for (int i = 0; i < Nodes.Count; i++)
            {
                if (Nodes[i].Weights.Count == 0)
                {
                    PruneNode(i);
                    prunedNodes++;
                }
            }

            return prunedNodes;
        }

        public void PruneNode(int index)
        {
            Nodes.RemoveAt(index);
        }
    }
}