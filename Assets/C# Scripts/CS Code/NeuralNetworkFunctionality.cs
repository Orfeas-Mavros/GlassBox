using System;

namespace NeuralNetworks
{
    public partial class NeuralNet
    {
            // - Running Methods - //

        public void Run(double[] inputs)
        {
            State = "Running";

            if (inputs.Length != Architecture[0])
            {
                GiveName();
                Console.WriteLine("Execution Error - Input does not correspond to the Network's Input Size.");
                return;
            }


            OrderWeights(0);

            State = "Running";

            // Setting the Biases
            for (int i = 0; i < hiddenPopulation; i++)
            {
                Nodes[Architecture[0] + i] = Biases[i];
            }

            //Running the Weights
            for (int i = 0; i < Weights.GetLength(0); i++)
            {
                Nodes[(int)Weights[i, 1]] += Nodes[(int)Weights[i, 0]] * Weights[i, 2];
            }

            FormatOutput();

            State = "Idle";
        }

        private void FormatOutput()
        {
            State = "Formatting Output";

            Output = new double[Architecture[^1]];

            for (int i = 1; i <= Architecture[^1]; i++)
            {
                Output[^i] = Nodes[^i];
            }

            State = "Idle";
        }


            // - Weight Methods - //

        private void OrderWeights(int order)
        {
            State = "Ordering Weights";

            // If ordering the Weights' values,
            // we assume there are little to no repetitions
            // and as a result run MergeSort.
            if (order == 2)
            {
                WeightMergeSort();
                return;
            }
            if (order != 0 && order != 1)
            {
                GiveName();
                Console.WriteLine("Sorting the Weights requires the vertical column of the  sorting values to be either 0, 1 or 2.");
                return;
            }

            // Otherwise, if ordering the Weights' origins or targets,
            // we assume there are many repetitions
            // and as a result run CountingSort.

            double[,] sortedArray = new double[Weights.GetLength(0), 3];

            // We find the largest value in the specified column:
            double max = Weights[0, order];
            for (int i = 0; i < Weights.GetLength(0); i++)
            {
                max = (Weights[i, order] > max) ? Weights[i, order] : max;
            }

            // We then create a separate Array of 0s.
            // We have as many zeros as the max value plus one.
            int[] countArray = new int[(int)max + 1];
            for (int i = 0; i <= max; i++)
            {
                countArray[i] = 0;
            }
            // Then for each value of the original array,
            // we increment the corresponding index of
            // the countArray by 1.
            for (int i = 0; i < Weights.GetLength(0); i++)
            {
                countArray[(int)Weights[i, order]]++;
            }

            // Now each index of the countArray corrseponds
            // to the number of times that value is encountered
            // in the original array.
            // We replace each index of the countArray with the
            // sum of all its previous values.
            for (int i = 1; i < countArray.Length; i++)
            {
                countArray[i] += countArray[i - 1];
            }
            // And lastly we shift all the values of the array
            // by one to the right (0 => 1, 1 => 2 etc.)
            for (int i = 1; i < countArray.Length; i++)
            {
                countArray[^i] = countArray[^(i + 1)];
            }
            // (The first index now should be 0)
            countArray[0] = 0;

            // And now we get a headache trying to read this.
            for (int i = 0; i < Weights.GetLength(0); i++)
            {
                sortedArray[countArray[(int)Weights[i, order]], 0] = Weights[i, 0];
                sortedArray[countArray[(int)Weights[i, order]], 1] = Weights[i, 1];
                sortedArray[countArray[(int)Weights[i, order]], 2] = Weights[i, 2];

                countArray[(int)Weights[i, order]]++;
            }

            Weights = sortedArray;

            State = "Idle";
        }
        private void WeightMergeSort()
        {
            double[,] auxiliaryArray = new double[Weights.GetLength(0), 3];
            
            for (int i = 0; i < Weights.GetLength(0); i++)
            {
                auxiliaryArray[i, 0] = Weights[i, 0];
                auxiliaryArray[i, 1] = Weights[i, 1];
                auxiliaryArray[i, 2] = Weights[i, 2];
            }


            for (int gLength = 1; gLength < Weights.GetLength(0); gLength *= 2)
            {
                int count = 0;

                if (Math.Log(gLength, 2) % 2 == 0)
                {
                    for (int gNum = 0; gNum < Math.Floor(Math.Ceiling(Weights.GetLength(0) / (double)gLength) / 2); gNum++)
                    {
                        int i = 0;
                        int j = 0;

                        int iIndex = gNum * gLength;
                        int jIndex = (gNum + 1) * gLength;

                        while (i != gLength || !(j == gLength || jIndex + j == Weights.GetLength(0)))
                        {
                            if (i == gLength)
                            {
                                auxiliaryArray[count, 0] = Weights[jIndex + j, 0];
                                auxiliaryArray[count, 1] = Weights[jIndex + j, 1];
                                auxiliaryArray[count, 2] = Weights[jIndex + j, 2];

                                j++;
                                count++;
                            }
                            else if (j == gLength || jIndex + j == Weights.GetLength(0))
                            {
                                auxiliaryArray[count, 0] = Weights[iIndex + i, 0];
                                auxiliaryArray[count, 1] = Weights[iIndex + i, 1];
                                auxiliaryArray[count, 2] = Weights[iIndex + i, 2];

                                i++;
                                count++;
                            }
                            else
                            {
                                if (Weights[iIndex + i, 2] >= Weights[jIndex + j, 2])
                                {
                                    auxiliaryArray[count, 0] = Weights[iIndex + i, 0];
                                    auxiliaryArray[count, 1] = Weights[iIndex + i, 1];
                                    auxiliaryArray[count, 2] = Weights[iIndex + i, 2];

                                    i++;
                                    count++;
                                }
                                else
                                {
                                    auxiliaryArray[count, 0] = Weights[jIndex + j, 0];
                                    auxiliaryArray[count, 1] = Weights[jIndex + j, 1];
                                    auxiliaryArray[count, 2] = Weights[jIndex + j, 2];

                                    j++;
                                    count++;
                                }
                            }
                        }
                    }
                }
                else
                {
                    for (int gNum = 0; gNum < Math.Floor(Math.Ceiling(Weights.GetLength(0) / (double)gLength) / 2); gNum++)
                    {
                        int i = 0;
                        int j = 0;

                        int iIndex = gNum * gLength;
                        int jIndex = (gNum + 1) * gLength;

                        while (i != gLength || !(j == gLength || jIndex + j == Weights.GetLength(0)))
                        {
                            if (i == gLength)
                            {
                                Weights[count, 0] = auxiliaryArray[jIndex + j, 0];
                                Weights[count, 1] = auxiliaryArray[jIndex + j, 1];
                                Weights[count, 2] = auxiliaryArray[jIndex + j, 2];

                                j++;
                                count++;
                            }
                            else if (j == gLength || jIndex + j == Weights.GetLength(0))
                            {
                                Weights[count, 0] = auxiliaryArray[iIndex + i, 0];
                                Weights[count, 1] = auxiliaryArray[iIndex + i, 1];
                                Weights[count, 2] = auxiliaryArray[iIndex + i, 2];

                                i++;
                                count++;
                            }
                            else
                            {
                                if (Weights[iIndex + i, 2] >= Weights[jIndex + j, 2])
                                {
                                    Weights[count, 0] = auxiliaryArray[iIndex + i, 0];
                                    Weights[count, 1] = auxiliaryArray[iIndex + i, 1];
                                    Weights[count, 2] = auxiliaryArray[iIndex + i, 2];

                                    i++;
                                    count++;
                                }
                                else
                                {
                                    Weights[count, 0] = auxiliaryArray[jIndex + j, 0];
                                    Weights[count, 1] = auxiliaryArray[jIndex + j, 1];
                                    Weights[count, 2] = auxiliaryArray[jIndex + j, 2];

                                    j++;
                                    count++;
                                }
                            }
                        }
                    }
                }
            }

            if (Math.Ceiling(Math.Log(Weights.GetLength(0), 2)) % 2 == 1)
            {
                for (int i = 0; i < Weights.GetLength(0); i++)
                {
                    Weights[i, 0] = auxiliaryArray[i, 0];
                    Weights[i, 1] = auxiliaryArray[i, 1];
                    Weights[i, 2] = auxiliaryArray[i, 2];
                }
            }

            State = "Idle";
        }


        /*private void ReverseWeights()
        {
            State = "Reversing Weights";

            double[] tempWeight = new double[3];
            int weightLength = Weights.GetLength(0);
            int endIndex = weightLength - 1;

            for (int i = 0; i < Math.Floor((decimal)(weightLength + 1) / 2); i++)
            {
                tempWeight[0] = Weights[i, 0];
                tempWeight[1] = Weights[i, 1];
                tempWeight[2] = Weights[i, 2];

                Weights[i, 0] = Weights[endIndex, 0];
                Weights[i, 1] = Weights[endIndex, 1];
                Weights[i, 2] = Weights[endIndex, 2];

                Weights[endIndex, 0] = tempWeight[0];
                Weights[endIndex, 1] = tempWeight[1];
                Weights[endIndex, 2] = tempWeight[2];

                endIndex--;
            }

            State = "Idle";
        }*/


            // - Learning Methods - //

        public void AdjustWeights(double[] weightChange)
        {
            State = "Adjusting Weights";

            if (weightChange.Length != Weights.GetLength(0))
            {
                GiveName();
                Console.WriteLine("Adjusting Weights Failed: Gradient Array not of same length as Weight Array.");
                return;
            }

            for (int i = 0; i < weightChange.Length; i++)
            {
                Weights[i, 2] += weightChange[i];
            }

            State = "Idle";
        }
        public void AdjustWeights(double[,] weightChange)
        {
            State = "Adjusting Weights";

            if (weightChange.GetLength(0) != Weights.GetLength(0))
            {
                GiveName();
                Console.WriteLine("Adjusting Weights Failed: Gradient Array not of same length as Weight Array.");
                return;
            }

            for (int i = 0; i < weightChange.Length; i++)
            {
                Weights[i, 2] += weightChange[i, 2];
            }

            State = "Idle";
        }

        public void AdjustBiases(double[] biasChange)
        {
            State = "Adjusting Biases";

            if (biasChange.Length != Biases.Length)
            {
                GiveName();
                Console.WriteLine("Adjusting Biases Failed: Gradient Array not of same length as Bias Array.");
                return;
            }

            for (int i = 0; i < biasChange.Length; i++)
            {
                Biases[i] += biasChange[i];
            }

            State = "Idle";
        }


        public void BackPropagate(double[] input, double[] ideal)
        {
            Run(input);
            CalcGradients(ideal);

            AdjustWeights(WeightGradients);
            AdjustBiases(BiasGradients);

            State = "Idle";
        }

        public void CalcGradients(double[] ideal)
        {
            State = "Calculating Gradients";

            BiasGradients = new double[Biases.Length];
            WeightGradients = new double[Weights.GetLength(0)];

            OrderWeights(1);

            int firstOutputIndex = Population - Architecture[^1];
            for (int i = 0; i < ideal.Length; i++)
            {
                BiasGradients[firstOutputIndex + i] = 2 * (Nodes[firstOutputIndex + i] - ideal[i]);
            }

            int prevTarget = 0;
            int currentTarget = 0;
            int currentOrigin = 0;

            for (int i = Weights.GetLength(0) - 1; i >= 0; i++)
            {
                currentOrigin = (int)Weights[i, 0];
                currentTarget = (int)Weights[i, 1];

                if (currentTarget != prevTarget)
                {
                    BiasGradients[currentTarget] *= ActivationFuncs[currentTarget].Derivative(Nodes[currentTarget]);

                    prevTarget = currentTarget;
                }

                WeightGradients[i] = BiasGradients[currentTarget] * Nodes[currentOrigin];

                BiasGradients[currentOrigin] += Weights[i, 2] * BiasGradients[currentTarget];
            } // There's no way this works first try


            State = "Idle";
        }


            // - Pruning Methods - //

        public void Prune()
        {
            // To be tested in the Optimization Methods phase.
        }


            // - Copy Methods - //

        public NeuralNet Clone(string newId) // Returns the exact same Network //
        {
            return new NeuralNet(newId, NetData);
        }
        public NeuralNet Clone()
        {
            return Clone(id + " Clone");
        }

        public NeuralNet Twin(string newId) // Returns a Network with the same Initialization Parameters //
        {
            return new NeuralNet(newId, SetupData);
        }


            // - Debug Methods - //

        public void GiveName()
        {
            Console.Write(id + " (" + State + "): ");
        }
    }
}