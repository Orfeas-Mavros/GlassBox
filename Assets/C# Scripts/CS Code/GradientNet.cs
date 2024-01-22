using System;

namespace NeuralNetworks
{
    public class GradientNet : NeuralNet
    {
            // - GradientNet Added Fields - //

        public double[][][] WeightGradients { get; private set; }
        public double[][] NodeGradients { get; private set; }
        //public double[] LayerGradients { get; private set; }

        /*public double[] SecondWeightGradients { get; private set; }
        public double[] SecondBiasGradients { get; private set; }
        public double[] SecondLayerGradients { get; private set; }*/
        // Too much of a headache for no significant reason.
        // Maybe in the future.


            // - GradientNet Constructor Methods - //

        public GradientNet(string id, NetworkInitData initData)
            : base(id, initData) { }
        public GradientNet(NetworkInitData initData)
            : base("Anonymous", initData) { }

        public GradientNet(string id, int[] architecture, object weightInput = null, object biasInput = null, object activationInput = null, params int[] randDepths)
            : base(id, architecture, weightInput, biasInput, activationInput, randDepths) { }
        public GradientNet(int[] architecture, object weightInput = null, object biasInput = null, object activationInput = null, params int[] randDepths)
            : base("Anonymous", architecture, weightInput, biasInput, activationInput, randDepths) { }

        public GradientNet(string id, NetworkData oldNetwork)
            : base(id, oldNetwork) { }
        public GradientNet(NetworkData oldNetwork)
            : base("Anonymous", oldNetwork) { }



        // ~~~ // ~~~ // ~~~ // ~~~ // ~~~ // ~~~ //



        public void CalcGradients(double[] ideal)
        {
            //LayerGradients = new double[Depth];
            NodeGradients = new double[Depth][];
            WeightGradients = new double[Depth - 1][][];



            // Calculating the Output Layer's Nodes' Gradients //
            for (int i = 0; i < Architecture[^1]; i++)
            {

            }

            // Generalizing for all other Layers //
            for (int L = Depth - 1; L > 0; L--)
            {
                
            }
        }


        /*public void CalcSecondGradients(double[] ideal)
        {

        }*/
    }
}