using System;
using System.Linq;

namespace NeuralNetworks
{
    public class NetworkData
    {
        private int[] Architecture;

        public double[,] Weights;
        public double[] Biases;
        public Activation[] Activation;

        public NetworkSetupData setupData;

        public NetworkData(NetworkSetupData setupData)
        {
            // More advanced Import Method, to be tested along with Database/Problem Space Import Methods.
        }
        public NetworkData(int[] architecture, object weightSetup, object biasSetup, object activationSetup, NetworkSetupData setupData, double weightDepth = 3, double biasDepth = 2)
        {
            // More advanced Import Method, to be tested along with Database/Problem Space Import Methods.
        }
        public NetworkData(int[] architecture)
        {
            // More advanced Import Method, to be tested along with Database/Problem Space Import Methods.
        }

        /* NON-FUNCTIONAL, to be tested during the Database/Problem Space Import Method phase.
        public void ReConfig(NetworkSetupData setupData)
        {

        }

        public void Regenerate()
        {

        }*/


        //  //
        //  //
        //  //  Important Note to self:
        //  //
        //  //    Every NetworkData Class (The NetworkData, the NetworkDataSetup
        //  //    and lastly the NeuralNet class itself) can only be initialized
        //  //    with th immediately less abstract class (Setup => NetData => NeuralNet)
        //  //
        //  //    In case of other initialization arguments given, first convert them
        //  //    to the previous in the order of abstraction class, check that class
        //  //    and then Initialize normally.
        //  //
        //  //
    }


    // ~~~ // ~~~ // ~~~ // ~~~ // ~~~ // ~~~ //


    public class NetworkSetupData
    {
        public int[] Architecture { get; private set; }
        public int Population { get; private set; }
        public int FullWeigths { get; private set; }
        
        public object Weights { get; private set; }
        
        public object Biases { get; private set; }
        
        public object Activation { get; private set; }

        public int[] RandDepths { get; private set; }


        public NetworkSetupData(int[] architecture, object weightInput, object biasInput, object activationInput, int[] randDepths)
        {
            // More advanced Import Method, to be tested along with Database/Problem Space Import Methods.
        }

        public NetworkSetupData(int[] architecture, object weightInput, object biasInput, object activationInput, int weightDepth = 3, int biasDepth = 2)
            : this(architecture, weightInput, biasInput, activationInput, new int[2] { weightDepth, biasDepth }) { }
    }
}