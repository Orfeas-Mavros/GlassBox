using System;
using System.Linq;

namespace NeuralNetworks
{
    public static class FormatData
    {
        public static Activation[] ActivationFuncs(int[] architecture, object activationInit)
        {
            int hiddenPopulation = 0;
            for ( int L = 1; L < architecture.Length; L++)
            {
                hiddenPopulation += architecture[L];
            }

            Activation[] nodeFuncs = new Activation[hiddenPopulation];

            if (activationInit.GetType() == typeof(Activation))
            {
                Activation initActivation = (Activation)activationInit;

                for (int i = 0; i < hiddenPopulation; i++)
                {
                    nodeFuncs[i] = initActivation;
                }
            }
            else if (activationInit.GetType() == typeof(Activation[]))
            {
                Activation[] initActivation = (Activation[])activationInit;

                if (initActivation.Length == architecture.Length - 1)
                {
                    int index = 0;
                    for (int L = 0; L < initActivation.Length; L++)
                    {
                        for (int width = 0; width < architecture[L + 1]; width++)
                        {
                            nodeFuncs[index] = initActivation[L];
                            index++;
                        }
                    }
                }
                else if (initActivation.Length == hiddenPopulation)
                {
                    for (int i = 0; i < hiddenPopulation; i++)
                    {
                        nodeFuncs[i] = initActivation[i];
                    }
                }
                else
                {
                    LogError.ActivationFunctions.InitLength(architecture, initActivation);

                    return ActivationFuncs(architecture, DefaultActivation.ReLU);
                }
            }
            else if (activationInit.GetType() == typeof(Activation[][]))
            {
                Activation[][] initActivation = (Activation[][])activationInit;

                int count = 0;
                for (int i = 0; i < initActivation.Length; i++)
                {
                    count += initActivation[i].Length;
                }

                if (count == hiddenPopulation)
                {
                    int index = 0;
                    for (int L = 0; L < initActivation.Length; L++)
                    {
                        for (int i = 0; i < initActivation[L].Length; i++)
                        {
                            nodeFuncs[index] = initActivation[L][i];
                            index++;
                        }
                    }
                }
                else
                {
                    LogError.ActivationFunctions.InitLength(architecture, initActivation);

                    return ActivationFuncs(architecture, DefaultActivation.ReLU);
                }
            }
            else
            {
                LogError.ActivationFunctions.InitType(activationInit);

                return ActivationFuncs(architecture, DefaultActivation.ReLU);
            }

            return nodeFuncs;
        }


        // ~~~ // ~~~ // ~~~ // ~~~ // ~~~ // ~~~ //


        public static double[] Biases(int[] architecture, object biasInit, double biasDepth = 2)
        {
            int hiddenPopulation = 0;
            for (int L = 1; L < architecture.Length; L++)
            {
                hiddenPopulation += architecture[L];
            }

            double[] nodeBiases = new double[hiddenPopulation];

            if (AcceptableSetupTypes.acceptableTypes.Contains(biasInit.GetType()))
            {
                if (biasInit.GetType() == typeof(Func<double>))
                {
                    Func<double> biasFunc = (Func<double>)biasInit;

                    if (biasDepth == 0)
                    {
                        nodeBiases = Biases(architecture, biasFunc());
                    }
                    else if (biasDepth == 1)
                    {
                        int count = 0;
                        for (int L = 1; L < architecture.Length; L++)
                        {
                            double initBias = biasFunc();
                            for (int i = 0; i < architecture[i]; i++)
                            {
                                nodeBiases[count] = initBias;
                                count++;
                            }
                        }
                    }
                    else if (biasDepth == 2)
                    {
                        for (int i = 0; i < hiddenPopulation; i++)
                        {
                            nodeBiases[i] = biasFunc();
                        }
                    }
                    else
                    {
                        LogError.Biases.DepthArgument(biasInit, biasDepth);

                        return Biases(architecture, 1D);
                    }
                }
                else
                {
                    double initBias = (double)biasInit;
                    for (int i = 0; i < hiddenPopulation; i++)
                    {
                        nodeBiases[i] = initBias;
                    }
                }
            }
            else if (AcceptableSetupTypes.acceptableTypes1D.Contains(biasInit.GetType()))
            {
                if (biasInit.GetType() == typeof(Func<double>[]))
                {
                    Func<double>[] biasFuncs = (Func<double>[])biasInit;
                    if (biasFuncs.Length == architecture.Length - 1)
                    {
                        if (biasDepth == 1)
                        {
                            int count = 0;
                            for (int L = 1; L < architecture.Length; L++)
                            {
                                double initBias = biasFuncs[L - 1]();
                                for (int i = 0; i < architecture[L]; i++)
                                {
                                    nodeBiases[count] = initBias;
                                    count++;
                                }
                            }
                        }
                        else if (biasDepth == 2)
                        {
                            int count = 0;
                            for (int L = 1; L < architecture.Length; L++)
                            {
                                for (int i = 0; i < architecture[L]; i++)
                                {
                                    nodeBiases[count] = biasFuncs[count]();
                                    count++;
                                }
                            }
                        }
                        else
                        {
                            LogError.Biases.DepthArgument(biasInit, biasDepth);

                            return Biases(architecture, 1D);
                        }
                    }
                    else if (biasFuncs.Length == hiddenPopulation)
                    {
                        if (biasDepth != 2)
                        {
                            LogError.Biases.DepthArgument(biasInit, biasDepth);

                            return Biases(architecture, 1D);
                        }

                        for (int i = 0; i < hiddenPopulation; i++)
                        {
                            nodeBiases[i] = biasFuncs[i]();
                        }
                    }
                    else
                    {
                        LogError.Biases.InitLength(architecture, biasFuncs);

                        return Biases(architecture, 1D);
                    }
                }
                else
                {
                    object[] objectInit = (object[])biasInit;
                    if (objectInit.Length == architecture.Length - 1)
                    {
                        int count = 0;
                        for (int L = 1; L < architecture.Length; L++)
                        {
                            double initBias = (double)objectInit[L - 1];
                            for (int i = 0; i < architecture[L]; i++)
                            {
                                nodeBiases[count] = initBias;
                            }
                        }
                    }
                    else if (objectInit.Length == hiddenPopulation)
                    {
                        for (int i = 0; i < hiddenPopulation; i++)
                        {
                            nodeBiases[i] = (double)objectInit[i];
                        }
                    }
                    else
                    {
                        LogError.Biases.InitLength(architecture, objectInit);

                        return Biases(architecture, 1D);
                    }
                }
            }
            else if (AcceptableSetupTypes.acceptableTypesJagged2D.Contains(biasInit.GetType()))
            {
                object[][] initBias = (object[][])biasInit;
                int count = 0;
                
                for (int L = 0; L < initBias.Length; L++)
                {
                    count += initBias[L].Length;
                }

                if (count != hiddenPopulation)
                {
                    LogError.Biases.InitLength(architecture, biasInit);

                    return Biases(architecture, 1D);
                }

                if (biasInit.GetType() == typeof(Func<double>[][]))
                {
                    if (biasDepth != 2)
                    {
                        LogError.Biases.DepthArgument(biasInit, biasDepth);

                        return Biases(architecture, 1D);
                    }

                    Func<double>[][] biasFuncs = (Func<double>[][])biasInit;
                    count = 0;

                    for (int L = 0; L < biasFuncs.Length; L++)
                    {
                        for (int i = 0; i < biasFuncs[L].Length; i++)
                        {
                            nodeBiases[count] = biasFuncs[L][i]();
                            count++;
                        }
                    }
                }
                else
                {
                    count = 0;
                    for (int L = 0; L < initBias.Length; L++)
                    {
                        for (int i = 0; i < initBias[L].Length; i++)
                        {
                            nodeBiases[count] = (double)initBias[L][i];
                            count++;
                        }
                    }
                }
            }
            else
            {
                LogError.Biases.InitType(biasInit);

                return Biases(architecture, 1D);
            }

            return nodeBiases;
        }


        // ~~~ // ~~~ // ~~~ // ~~~ // ~~~ // ~~~ //


        public static double[,] Weights(int[] architecture, object weightInit, double weightDepth = 3)
        {
            int hiddenPopulation = 0;
            int fullWeights = 0;
            for (int L = 1; L < architecture.Length; L++)
            {
                hiddenPopulation += architecture[L];
                fullWeights += architecture[L] * architecture[L - 1];
            }

            double[] nodeWeights = new double[fullWeights];

            if (AcceptableSetupTypes.acceptableTypes.Contains(weightInit.GetType()))
            {
                if (weightInit.GetType() == typeof(Func<double>))
                {
                    Func<double> weightFunc = (Func<double>)weightInit;

                    if (weightDepth == 0)
                    {
                        double initWeight = weightFunc();

                        for (int i = 0; i < fullWeights; i++)
                        {
                            nodeWeights[i] = initWeight;
                        }
                    }
                    else if (weightDepth == 1)
                    {
                        int count = 0;
                        for (int L = 0; L < architecture.Length - 1; L++)
                        {
                            double initWeights = weightFunc();

                            for (int j = 0; j < architecture[L]; j++)
                            {
                                for (int i = 0; i < architecture[L + 1]; i++)
                                {
                                    nodeWeights[count] = initWeights;
                                    count++;
                                }
                            }
                        }
                    }
                    else if (weightDepth == 2)
                    {
                        int count = 0;
                        for (int L = 0; L < architecture.Length - 1; L++)
                        {
                            for (int j = 0; j < architecture[L]; j++)
                            {
                                double initWeights = weightFunc();

                                for (int i = 0; i < architecture[L + 1]; i++)
                                {
                                    nodeWeights[count] = initWeights;
                                    count++;
                                }
                            }
                        }
                    }
                    else if (weightDepth == 3)
                    {
                        for (int i = 0; i < fullWeights; i++)
                        {
                            nodeWeights[i] = weightFunc();
                        }
                    }
                    else
                    {
                        LogError.Weights.DepthArgument(weightInit, weightDepth);

                        return Weights(architecture, 1D);
                    }
                }
                else
                {
                    double initWeight = (double)weightInit;

                    for (int i = 0; i < fullWeights; i++)
                    {
                        nodeWeights[i] = initWeight;
                    }
                }
            }
            else if (AcceptableSetupTypes.acceptableTypes1D.Contains(weightInit.GetType()))
            {
                if (weightInit.GetType() == typeof(Func<double>[]))
                {
                    Func<double>[] weightFuncs = (Func<double>[])weightInit;

                    if (weightFuncs.Length == architecture.Length - 1)
                    {
                        if (weightDepth == 1)
                        {
                            int count = 0;
                            for (int L = 0; L < architecture.Length - 1; L++)
                            {
                                double initWeights = weightFuncs[L]();

                                for (int j = 0; j < architecture[L]; j++)
                                {
                                    for (int i = 0; i < architecture[L + 1]; i++)
                                    {
                                        nodeWeights[count] = initWeights;
                                        count++;
                                    }
                                }
                            }
                        }
                        else if (weightDepth == 2)
                        {
                            int count = 0;
                            for (int L = 0; L < architecture.Length - 1; L++)
                            {
                                for (int j = 0; j < architecture[L]; j++)
                                {
                                    double initWeights = weightFuncs[L]();

                                    for (int i = 0; i < architecture[L + 1]; i++)
                                    {
                                        nodeWeights[count] = initWeights;
                                        count++;
                                    }
                                }
                            }
                        }
                        else if (weightDepth == 3)
                        {
                            int count = 0;
                            for (int L = 0; L < architecture.Length - 1; L++)
                            {
                                for (int j = 0; j < architecture[L]; j++)
                                {
                                    for (int i = 0; i < architecture[L + 1]; i++)
                                    {
                                        nodeWeights[count] = weightFuncs[L]();
                                        count++;
                                    }
                                }
                            }
                        }
                        else
                        {
                            LogError.Weights.DepthArgument(weightFuncs, weightDepth);

                            return Weights(architecture, 1D);
                        }
                    }
                    else if (weightFuncs.Length == hiddenPopulation)
                    {
                        if (weightDepth == 2)
                        {
                            int countWeights = 0;
                            int countNodes = 0;
                            for (int L = 0; L < architecture.Length - 1; L++)
                            {
                                for (int j = 0; j < architecture[L]; j++)
                                {
                                    double initWeights = weightFuncs[countNodes]();

                                    for (int i = 0; i < architecture[L + 1]; i++)
                                    {
                                        nodeWeights[countWeights] = initWeights;
                                        countWeights++;
                                    }

                                    countNodes++;
                                }
                            }
                        }
                        else if (weightDepth == 3)
                        {
                            int countWeights = 0;
                            int countNodes = 0;
                            for (int L = 0; L < architecture.Length - 1; L++)
                            {
                                for (int j = 0; j < architecture[L]; j++)
                                {
                                    for (int i = 0; i < architecture[L + 1]; i++)
                                    {
                                        nodeWeights[countWeights] = weightFuncs[countNodes]();
                                        countWeights++;
                                    }

                                    countNodes++;
                                }
                            }
                        }
                        else
                        {
                            LogError.Weights.DepthArgument(weightFuncs, weightDepth);

                            return Weights(architecture, 1D);
                        }
                    }
                    else if (weightFuncs.Length == fullWeights)
                    {
                        if (weightDepth != 3)
                        {
                            LogError.Weights.DepthArgument(weightFuncs, weightDepth);

                            return Weights(architecture, 1D);
                        }

                        for (int i = 0; i < fullWeights; i++)
                        {
                            nodeWeights[i] = weightFuncs[i]();
                        }
                    }
                    else
                    {
                        LogError.Weights.InitLength(architecture, weightFuncs);

                        return Weights(architecture, 1D);
                    }
                }
                else
                {
                    object[] initWeights = (object[])weightInit;
                    if (initWeights.Length == architecture.Length - 1)
                    {
                        int count = 0;
                        for (int L = 0; L < architecture.Length - 1; L++)
                        {
                            for (int j = 0; j < architecture[L]; j++)
                            {
                                for (int i = 0; i < architecture[L + 1]; i++)
                                {
                                    nodeWeights[count] = (double)initWeights[L];
                                    count++;
                                }
                            }
                        }
                    }
                    else if (initWeights.Length == hiddenPopulation)
                    {
                        int countWeights = 0;
                        int countNodes = 0;
                        for (int L = 0; L < architecture.Length - 1; L++)
                        {
                            for (int j = 0; j < architecture[L]; j++)
                            {
                                for (int i = 0; i < architecture[L + 1]; i++)
                                {
                                    nodeWeights[countWeights] = (double)initWeights[countNodes];
                                    countWeights++;
                                }

                                countNodes++;
                            }
                        }
                    }
                    else if (initWeights.Length == fullWeights)
                    {
                        for (int i = 0; i < fullWeights; i++)
                        {
                            nodeWeights[i] = (double)initWeights[i];
                        }
                    }
                    else
                    {
                        LogError.Weights.InitLength(architecture, initWeights);

                        return Weights(architecture, 1D);
                    }
                }
            }
            else if (AcceptableSetupTypes.acceptableTypesJagged2D.Contains(weightInit.GetType()))
            {
                if (weightInit.GetType() == typeof(Func<double>[][]))
                {
                    Func<double>[][] weightFuncs = (Func<double>[][])weightInit;
                    
                    int length = 0;
                    for (int i = 0; i < weightFuncs.Length; i++)
                    {
                        length += weightFuncs[i].Length;
                    }

                    if (length == hiddenPopulation)
                    {
                        if (weightDepth == 2)
                        {
                            int count = 0;
                            int indexTracking = 0;
                            int currentLayer = 0;

                            for (int i = 0; i < weightFuncs.Length; i++)
                            {
                                for (int j = 0; j < weightFuncs.Length; j++)
                                {
                                    double initWeights = weightFuncs[i][j]();
                                    for (int k = 0; k < architecture[currentLayer + 1]; k++)
                                    {
                                        nodeWeights[count] = initWeights;
                                        count++;
                                    }

                                    indexTracking++;

                                    if (indexTracking == architecture[currentLayer])
                                    {
                                        indexTracking = 0;
                                        currentLayer++;
                                    }
                                }
                            }
                        }
                        else if (weightDepth == 3)
                        {
                            int count = 0;
                            int indexTracking = 0;
                            int currentLayer = 0;

                            for (int i = 0; i < weightFuncs.Length; i++)
                            {
                                for (int j = 0; j < weightFuncs.Length; j++)
                                {
                                    for (int k = 0; k < architecture[currentLayer + 1]; k++)
                                    {
                                        nodeWeights[count] = weightFuncs[i][j]();
                                        count++;
                                    }

                                    indexTracking++;

                                    if (indexTracking == architecture[currentLayer])
                                    {
                                        indexTracking = 0;
                                        currentLayer++;
                                    }
                                }
                            }
                        }
                        else
                        {
                            LogError.Weights.DepthArgument(weightFuncs, weightDepth);

                            return Weights(architecture, 1D);
                        }
                    }
                    else if (length == fullWeights)
                    {
                        if (weightDepth != 3)
                        {
                            LogError.Weights.DepthArgument(weightFuncs, weightDepth);

                            return Weights(architecture, 1D);
                        }

                        int count = 0;
                        for (int i = 0; i < weightFuncs.Length; i++)
                        {
                            for (int j = 0; j < weightFuncs.Length; j++)
                            {
                                nodeWeights[count] = weightFuncs[i][j]();
                                count++;
                            }
                        }
                    }
                    else
                    {
                        LogError.Weights.InitLength(architecture, weightInit);

                        return Weights(architecture, 1D);
                    }
                }
                else
                {
                    object[][] initWeights = (object[][])weightInit;

                    int length = 0;
                    for (int i = 0; i < initWeights.Length; i++)
                    {
                        length += initWeights[i].Length;
                    }

                    if (length == hiddenPopulation)
                    {
                        int count = 0;
                        int indexTracking = 0;
                        int currentLayer = 0;

                        for (int i = 0; i < initWeights.Length; i++)
                        {
                            for (int j = 0; j < initWeights.Length; j++)
                            {
                                for (int k = 0; k < architecture[currentLayer + 1]; k++)
                                {
                                    nodeWeights[count] = (double)initWeights[i][j];
                                    count++;
                                }

                                indexTracking++;

                                if (indexTracking == architecture[currentLayer])
                                {
                                    indexTracking = 0;
                                    currentLayer++;
                                }
                            }
                        }
                    }
                    else if (length == fullWeights)
                    {
                        int count = 0;
                        for (int i = 0; i < initWeights.Length; i++)
                        {
                            for (int j = 0; j < initWeights.Length; j++)
                            {
                                nodeWeights[count] = (double)initWeights[i][j];
                                count++;
                            }
                        }
                    }
                    else
                    {
                        LogError.Weights.InitLength(architecture, initWeights);
                    }
                }
            }
            else if (AcceptableSetupTypes.acceptableTypes3D.Contains(weightInit.GetType()))
            {
                if (weightInit.GetType() == typeof(Func<double>[][][]))
                {
                    if (weightDepth != 3)
                    {
                        LogError.Weights.DepthArgument(weightInit, weightDepth);

                        return Weights(architecture, 1D);
                    }

                    Func<double>[][][] weightFuncs = (Func<double>[][][])weightInit;

                    int length = 0;
                    for (int i = 0; i < weightFuncs.Length; i++)
                    {
                        for (int j = 0; j < weightFuncs[i].Length; j++)
                        {
                            length += weightFuncs[i][j].Length;
                        }
                    }

                    if (length != fullWeights)
                    {
                        LogError.Weights.InitLength(architecture, weightInit);

                        return Weights(architecture, 1D);
                    }

                    int count = 0;
                    for (int i = 0; i < weightFuncs.Length; i++)
                    {
                        for (int j = 0; j < weightFuncs[i].Length; j++)
                        {
                            for (int k = 0; k < weightFuncs[i][j].Length; k++)
                            {
                                nodeWeights[count] = weightFuncs[i][j][k]();
                                count++;
                            }
                        }
                    }
                }
                else
                {
                    object[][][] initWeights = (object[][][])weightInit;

                    int length = 0;
                    for (int i = 0; i < initWeights.Length; i++)
                    {
                        for (int j = 0; j < initWeights[i].Length; j++)
                        {
                            length += initWeights[i][j].Length;
                        }
                    }

                    if (length != fullWeights)
                    {
                        LogError.Weights.InitLength(architecture, initWeights);

                        return Weights(architecture, 1D);
                    }

                    int count = 0;
                    for (int i = 0; i < initWeights.Length; i++)
                    {
                        for (int j = 0; j < initWeights[i].Length; j++)
                        {
                            for (int k = 0; k < initWeights[i][j].Length; k++)
                            {
                                nodeWeights[count] = (double)initWeights[i][j][k];
                                count++;
                            }
                        }
                    }
                }
            }
            else if (AcceptableSetupTypes.acceptableTypes2D.Contains(weightInit.GetType()))
            {
                object[,] initWeights = (object[,])weightInit;
                if (initWeights.GetLength(1) != 3 || initWeights.GetLength(0) > fullWeights)
                {
                    LogError.Weights.Array2D(initWeights);

                    return Weights(architecture, 1D);
                }

                double[,] completeWeights = new double[initWeights.GetLength(0), 3];
                for (int i = 0; i < initWeights.GetLength(0); i++)
                {
                    completeWeights[i, 0] = (double)initWeights[i, 0];
                    completeWeights[i, 1] = (double)initWeights[i, 1];
                    completeWeights[i, 2] = (double)initWeights[i, 2];
                }

                return completeWeights;
            }
            else
            {
                LogError.Weights.InitType(weightInit);

                return Weights(architecture, 1D);
            }

            return InsertWeightCoords(architecture, nodeWeights);
        }


        // ~~~ // ~~~ // ~~~ // ~~~ // ~~~ // ~~~ //


        public static double[,] InsertWeightCoords(int[] architecture, double[] weights)
        {
            double[,] newWeights = new double[weights.Length, 3];

            int weightIndex = 0;
            int originIndex = 0;
            int targetIndex = 0;
            int oldIndex = 0;

            for (int L = 0; L < architecture.Length - 1; L++)
            {
                oldIndex += architecture[L];

                for (int j = 0; j < architecture[L]; j++)
                {
                    for (int i = 0; i < architecture[L + 1]; i++)
                    {
                        newWeights[weightIndex, 0] = originIndex;
                        newWeights[weightIndex, 1] = targetIndex;
                        newWeights[weightIndex, 2] = weights[weightIndex];

                        weightIndex++;
                        targetIndex++;
                    }

                    originIndex++;
                    targetIndex = oldIndex;
                }
            }

            return newWeights;
        }


        // ~~~ // ~~~ // ~~~ // ~~~ // ~~~ // ~~~ //


        public static double[,] Weights(int[] architecture, object weightInit, bool forward, double weightDepth = 3)
        {
            if (forward)
            {
                return Weights(architecture, weightInit, weightDepth);
            }

            int hiddenPopulation = 0;
            int fullWeights = 0;
            for (int L = 1; L < architecture.Length; L++)
            {
                hiddenPopulation += architecture[L];
                fullWeights += architecture[L - 1] * architecture[L];
            }

            double[] nodeWeights = new double[fullWeights];

            if (AcceptableSetupTypes.acceptableTypes1D.Contains(weightInit.GetType()))
            {
                object[] initWeights = (object[])weightInit;

                if (initWeights.Length != hiddenPopulation)
                {
                    return Weights(architecture, weightInit, weightDepth);
                }

                if (initWeights.GetType() == typeof(Func<double>[]))
                {
                    if (weightDepth == 2)
                    {

                    }

                    if (weightDepth == 3)
                    {

                    }

                    return Weights(architecture, weightInit, weightDepth);
                }


            }
            else if (AcceptableSetupTypes.acceptableTypesJagged2D.Contains(weightInit.GetType()))
            {
                object[][] initWeights = (object[][])weightInit;

                int length = 0;
                for (int i = 0; i < initWeights.Length; i++)
                {
                    length += initWeights[i].Length;
                }

                if (length != hiddenPopulation)
                {
                    return Weights(architecture, weightInit, weightDepth);
                }

                if (weightInit.GetType() == typeof(Func<double>[][]))
                {
                    Func<double>[][] weightFuncs = (Func<double>[][])weightInit;

                    if (weightDepth == 2)
                    {

                    }

                    if (weightDepth == 3)
                    {

                    }
                }


            }

            return Weights(architecture, weightInit, weightDepth);
        }
    }
}