namespace NeuralNetworks
{
    public class ProblemData
    {
        public double[] input; // The Problem / Input //
        public double[] ideal; // The Desired Output //

        public bool Supervised => ideal != null; // Whether or not the Problem Data Indicates a Supervised Machine Learning Algorithm //


        public ProblemData(double[] input, double[] ideal)
        {
            this.input = new double[input.Length];
            this.ideal = new double[ideal.Length];

            for (int i = 0; i < input.Length; i++)
            {
                this.input[i] = input[i];
            }

            for (int i = 0; i < ideal.Length; i++)
            {
                this.ideal[i] = ideal[i];
            }
        }

        public ProblemData(double[][] data)
            : this(data[0], data[1]) { }

        public ProblemData(double[] input)
        {
            ideal = null;

            this.input = new double[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                this.input[i] = input[i];
            }
        }


        public void FormatUnsupervised()
        {
            if (!Supervised)
            {
                ideal = input;
            }
        }


        public ProblemData Clone()
        {
            return new ProblemData(input, ideal);
        }
    }
}