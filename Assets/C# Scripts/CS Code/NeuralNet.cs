using System;

namespace NeuralNetworks
{
    public class NeuralNet : NetworkData
    {
            // - NeuralNet Added Fields - //
        
        public double[] Output { get; private set; } // The final Layer //

        public double[][] Signals
        {
            get
            {
                double[][] signals = new double[Depth - 1][];

                for (int L = 0; L < Depth - 1; L++)
                {
                    signals[L] = new double[Architecture[L + 1]];

                    for (int i = 0; i < Architecture[L + 1]; i++)
                    {
                        signals[L][i] = Layers[L].Nodes[i].Signal;
                    }
                }

                return signals;
            }
        }


        // - NeuralNet Constructor Methods - //

        public NeuralNet(string id, NetworkInitData initData)
            : base(id, initData) { }
        public NeuralNet(NetworkInitData initData)
            : base("Anonymous", initData) { }

        public NeuralNet(string id, int[] architecture, object weightInput = null, object biasInput = null, object activationInput = null, params int[] randDepths)
            : base(id, architecture, weightInput, biasInput, activationInput, randDepths) { }
        public NeuralNet(int[] architecture, object weightInput = null, object biasInput = null, object activationInput = null, params int[] randDepths)
            : base("Anonymous", architecture, weightInput, biasInput, activationInput, randDepths) { }

        public NeuralNet(string id, NetworkData oldNetwork)
            : base(id, oldNetwork) { }
        public NeuralNet(NetworkData oldNetwork)
            : base("Anonymous", oldNetwork) { }



        // ~~~ // ~~~ // ~~~ // ~~~ // ~~~ // ~~~ //



            // - NeuralNet Methods - //

        public void Run(double[] inputs)
        {
            for (int i = 0; i < Layers.Count; i++)
            {
                Layers[i].Run(inputs);
            }

            FormatOutput();
        }


        private void FormatOutput()
        {
            Output = new double[Architecture[^1]];

            for (int i = 0; i < Architecture[^1]; i++)
            {
                Output[i] = Layers[^1].Nodes[i].Signal;
            }
        }
    }
}