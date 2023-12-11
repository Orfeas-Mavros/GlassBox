using System;

namespace NeuralNetworks
{
    public class Activation
    {
        public Func<double, double> Activate;
        public Func<double, double> Derivative;

        public Activation(Func<double, double> activation, Func<double, double> derivative)
        {
            Activate = activation;
            Derivative = derivative;
        }
    }


    // ~~~ // ~~~ // ~~~ // ~~~ // ~~~ // ~~~ //


    public static class DefaultActivation
    {
        public readonly static Activation BinaryStep;
        public readonly static Activation ReLU;
        public readonly static Activation LeakyReLU;
        public readonly static Activation Sigmoid;
        public readonly static Activation TanH;

        static DefaultActivation()
        {
            Func<double, double> activation = (double input) =>
            {
                return (input > 0) ? 1D : 0D;
            };

            Func<double, double> derivative = (double input) =>
            {
                return 0;
            };

            BinaryStep = new Activation(activation, derivative);



            activation = (double input) =>
            {
                return (input > 0) ? input : 0D;
            };

            derivative = (double input) =>
            {
                return (input > 0) ? 1D : 0D;
            };

            ReLU = new Activation(activation, derivative);



            activation = (double input) =>
            {
                return (input > 0) ? input : 0.1 * input;
            };

            derivative = (double input) =>
            {
                return (input > 0) ? 1D : 0.1D;
            };

            LeakyReLU = new Activation(activation, derivative);



            activation = (double input) =>
            {
                return 1 / 1 + Math.Exp(-input);
            };

            derivative = (double input) =>
            {
                return (1 / 1 + Math.Exp(-input)) * (1 - (1 / 1 + Math.Exp(-input)));
            };

            Sigmoid = new Activation(activation, derivative);



            activation = (double input) =>
            {
                return (Math.Exp(input) - Math.Exp(-input)) / (Math.Exp(input) + Math.Exp(-input));
            };

            derivative = (double input) =>
            {
                return 1 - Math.Pow((Math.Exp(input) - Math.Exp(-input)) / (Math.Exp(input) + Math.Exp(-input)), 2);
            };

            TanH = new Activation(activation, derivative);
        }
    }
}