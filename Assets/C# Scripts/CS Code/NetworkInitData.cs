using System;
using System.Linq;
using System.Collections.Generic;

namespace NeuralNetworks
{
    public class NetworkInitData
    {
            // - NetworkInitData Fields - //

        public int[] Architecture { get; private set; }

        private object _weights;
        public object Weights
        {
            get
            {
                return _weights;
            }
            set
            {
                if (value == null)
                {
                    _weights = Format.DefaultInitWeights;
                    return;
                }

                if (value.GetType() == typeof(double) ||
                    value.GetType() == typeof(double[]) ||
                    value.GetType() == typeof(Func<double>) ||
                    value.GetType() == typeof(Func<double>[]))
                {
                    _weights = value;
                }
                else if (value.GetType() == typeof(double[][]))
                {
                    _weights = Format.Flatten2D((double[][])value);
                }
                else if (value.GetType() == typeof(Func<double>[][]))
                {
                    _weights = Format.Flatten2D((Func<double>[][])value);
                }
                else if (value.GetType() == typeof(double[][][]))
                {
                    _weights = Format.Flatten3D((double[][][])value);
                }
                else if (value.GetType() == typeof(Func<double>[][][]))
                {
                    _weights = Format.Flatten3D((Func<double>[][][])value);
                }
                else if (value.GetType() == typeof(List<double>))
                {
                    _weights = ((List<double>)value).ToArray();
                }
                else if (value.GetType() == typeof(List<Func<double>>))
                {
                    _weights = ((List<Func<double>>)value).ToArray();
                }
                else
                {
                    LogError.Weights.InitType(value);
                    _weights = Format.DefaultInitWeights;
                }
            }
        }

        public object _biases;
        public object Biases
        {
            get
            {
                return _biases;
            }
            set
            {
                if (value == null)
                {
                    _biases = Format.DefaultInitBiases;
                    return;
                }

                if (value.GetType() == typeof(double) ||
                    value.GetType() == typeof(double[]) ||
                    value.GetType() == typeof(Func<double>) ||
                    value.GetType() == typeof(Func<double>[]))
                {
                    _biases = value;
                }
                else if (value.GetType() == typeof(double[][]))
                {
                    _biases = Format.Flatten2D((double[][])value);
                }
                else if (value.GetType() == typeof(Func<double>[][]))
                {
                    _biases = Format.Flatten2D((Func<double>[][])value);
                }
                else if (value.GetType() == typeof(List<double>))
                {
                    _biases = ((List<double>)value).ToArray();
                }
                else if (value.GetType() == typeof(List<Func<double>>))
                {
                    _biases = ((List<Func<double>>)value).ToArray();
                }
                else
                {
                    LogError.Biases.InitType(value);
                    _biases = Format.DefaultInitBiases;
                }
            }
        }

        public object _activations;
        public object Activations
        {
            get
            {
                return _activations;
            }
            set
            {
                if (value == null)
                {
                    _activations = Format.DefaultInitActivationFunctions;
                    return;
                }

                if (value.GetType() == typeof(Activation) ||
                    value.GetType() == typeof(Activation[]))
                {
                    _activations = value;
                }
                else if (value.GetType() == typeof(Activation[][]))
                {
                    _activations = Format.Flatten2D((Activation[][])value);
                }
                else if (value.GetType() == typeof(List<Activation>))
                {
                    _activations = ((List<Activation>)value).ToArray();
                }
                else
                {
                    LogError.ActivationFunctions.InitType(value);
                    _activations = Format.DefaultInitActivationFunctions;
                }
            }
        }

        private int[] _randDepths;
        public int[] RandDepths
        {
            get
            {
                return _randDepths ?? new int[2] { 3, 2 };
            }
            private set
            {
                if (Biases.GetType() != typeof(Func<double>) &&
                    Biases.GetType() != typeof(Func<double>[]) &&
                    Weights.GetType() != typeof(Func<double>) &&
                    Weights.GetType() != typeof(Func<double>[]))
                {
                    _randDepths = null;
                    return;
                }


                if (value == null || value.Length == 0)
                {
                    _randDepths = null;
                    return;
                }

                if (value.Length != 2)
                {
                    LogError.RandDepths.InitLength(value.Length);
                    _randDepths = null;
                    return;
                }

                _randDepths = new int[2];

                if (value[0] != 0 && value[0] != 1 && value[0] != 2 && value[0] != 3)
                {
                    LogError.RandDepths.WeightRandomDepth(value[0]);
                    value[0] = 3;
                }

                if (value[1] != 0 && value[1] != 1 && value[1] != 2)
                {
                    LogError.RandDepths.BiasRandomDepth(value[1]);
                    value[1] = 2;
                }

                _randDepths[0] = value[0];
                _randDepths[1] = value[1];
            }
        }


            // - NetworkInitData Constructor - //

        public void SetupData(int[] architecture, object weightInput = null, object biasInput = null, object activationInput = null, params int[] randDepths)
        {
            Architecture = architecture;

            Weights = weightInput;
            Biases = biasInput;
            Activations = activationInput;
            RandDepths = randDepths;

            CheckData();
        }

        public NetworkInitData(int[] architecture, object weightInput = null, object biasInput = null, object activationInput = null, params int[] randDepths)
        {
            SetupData(architecture, weightInput, biasInput, activationInput, randDepths);
        }


            // - NetworkInitData Method(s) - //

        public void CheckData()
        {
            int hiddenPopulation = 0;
            int fullWeights = 0;

            for (int i = 1; i < Architecture.Length; i++)
            {
                hiddenPopulation += Architecture[i];
                fullWeights += Architecture[i - 1] * Architecture[i];
            }


            if (Activations is Array)
            {
                int arrayLength = ((object[])Activations).Length;

                if (arrayLength != Architecture.Length - 1 &&
                    arrayLength != hiddenPopulation)
                {
                    LogError.ActivationFunctions.InitLength(Architecture, Activations);
                    Activations = Format.DefaultInitActivationFunctions;
                }
            }


            if (Biases is Array)
            {
                int arrayLength = ((object[])Biases).Length;

                if (arrayLength == Architecture.Length - 1)
                {
                    if (Biases.GetType() == typeof(Func<double>[]) &&
                        RandDepths[1] != 1 && RandDepths[1] != 2)
                    {
                        LogError.RandDepths.BiasRandomFuncDepth(RandDepths[1], 1);
                        RandDepths[1] = 2;
                    }
                }
                else if (arrayLength == hiddenPopulation)
                {
                    if (Biases.GetType() == typeof(Func<double>[]) &&
                        RandDepths[1] != 2)
                    {
                        LogError.RandDepths.BiasRandomFuncDepth(RandDepths[1], 2);
                        RandDepths[1] = 2;
                    }
                }
                else
                {
                    LogError.Biases.InitLength(Architecture, Biases);
                    Biases = Format.DefaultInitBiases;
                }
            }


            if (Weights is Array)
            {
                int arrayLength = ((object[])Weights).Length;

                if (arrayLength == Architecture.Length - 1)
                {
                    if (Weights.GetType() == typeof(Func<double>[]) &&
                        RandDepths[0] != 1 && RandDepths[0] != 2 && RandDepths[0] != 3)
                    {
                        LogError.RandDepths.WeightRandomFuncDepth(RandDepths[0], 1);
                        RandDepths[0] = 3;
                    }
                }
                else if (arrayLength == hiddenPopulation)
                {
                    if (Weights.GetType() == typeof(Func<double>[]) &&
                        RandDepths[0] != 2 && RandDepths[0] != 3)
                    {
                        LogError.RandDepths.WeightRandomFuncDepth(RandDepths[0], 2);
                        RandDepths[0] = 3;
                    }
                }
                else if (arrayLength == fullWeights)
                {
                    if (Weights.GetType() == typeof(Func<double>[]) &&
                        RandDepths[0] != 3)
                    {
                        LogError.RandDepths.WeightRandomFuncDepth(RandDepths[0], 3);
                        RandDepths[0] = 3;
                    }
                }
                else
                {
                    LogError.Weights.InitLength(Architecture, Weights);
                    Weights = Format.DefaultInitWeights;
                }
            }
        }
    }
}